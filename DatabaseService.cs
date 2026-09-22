using System;
using System.IO;
using Microsoft.Data.Sqlite;

namespace MusicPlayer;

public class DatabaseService
{
    private readonly string _dbFilePath;
    private readonly string _connectionString;

    public DatabaseService()
    {
        _dbFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "musicplayer.db");
        _connectionString = $"Data Source={_dbFilePath}";
    }

    public string DbFilePath => _dbFilePath;

    public void InitializeDatabase()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Songs (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Title TEXT,
                Artist TEXT,
                Album TEXT,
                FilePath TEXT,
                Duration TEXT,
                DateAdded TEXT
            );
        ";
        command.ExecuteNonQuery();

        // Migration for IsFavorite
        var checkCmd = connection.CreateCommand();
        checkCmd.CommandText = "PRAGMA table_info(Songs)";
        using var reader = checkCmd.ExecuteReader();
        bool hasIsFavorite = false;
        while (reader.Read())
        {
            if (reader.GetString(1) == "IsFavorite")
            {
                hasIsFavorite = true;
                break;
            }
        }
        reader.Close();

        if (!hasIsFavorite)
        {
            var alterCmd = connection.CreateCommand();
            alterCmd.CommandText = "ALTER TABLE Songs ADD COLUMN IsFavorite INTEGER NOT NULL DEFAULT 0";
            alterCmd.ExecuteNonQuery();
        }

        // Migration for IsAutoDiscovered
        var checkCmdAuto = connection.CreateCommand();
        checkCmdAuto.CommandText = "PRAGMA table_info(Songs)";
        using var readerAuto = checkCmdAuto.ExecuteReader();
        bool hasIsAutoDiscovered = false;
        while (readerAuto.Read())
        {
            if (readerAuto.GetString(1) == "IsAutoDiscovered")
            {
                hasIsAutoDiscovered = true;
                break;
            }
        }
        readerAuto.Close();

        if (!hasIsAutoDiscovered)
        {
            var alterCmdAuto = connection.CreateCommand();
            alterCmdAuto.CommandText = "ALTER TABLE Songs ADD COLUMN IsAutoDiscovered INTEGER NOT NULL DEFAULT 0";
            alterCmdAuto.ExecuteNonQuery();
        }
    }

    public System.Collections.Generic.List<Song> GetSongs()
    {
        var songs = new System.Collections.Generic.List<Song>();
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT Id, Title, Artist, Album, FilePath, Duration, DateAdded, IsFavorite, IsAutoDiscovered FROM Songs";

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            songs.Add(new Song
            {
                Id = reader.GetInt32(0),
                Title = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                Artist = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                Album = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                FilePath = reader.IsDBNull(4) ? string.Empty : reader.GetString(4),
                Duration = reader.IsDBNull(5) ? string.Empty : reader.GetString(5),
                DateAdded = reader.IsDBNull(6) ? string.Empty : reader.GetString(6),
                IsFavorite = !reader.IsDBNull(7) && reader.GetInt32(7) == 1,
                IsAutoDiscovered = !reader.IsDBNull(8) && reader.GetInt32(8) == 1
            });
        }
        return songs;
    }

    public bool SongExists(string filePath)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT COUNT(1) FROM Songs WHERE FilePath = @FilePath";
        command.Parameters.AddWithValue("@FilePath", filePath);

        var result = command.ExecuteScalar();
        return Convert.ToInt32(result) > 0;
    }

    public void AddSong(Song song)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"
            INSERT INTO Songs (Title, Artist, Album, FilePath, Duration, DateAdded, IsAutoDiscovered)
            VALUES (@Title, @Artist, @Album, @FilePath, @Duration, @DateAdded, @IsAutoDiscovered)
        ";
        command.Parameters.AddWithValue("@Title", song.Title ?? string.Empty);
        command.Parameters.AddWithValue("@Artist", song.Artist ?? string.Empty);
        command.Parameters.AddWithValue("@Album", song.Album ?? string.Empty);
        command.Parameters.AddWithValue("@FilePath", song.FilePath ?? string.Empty);
        command.Parameters.AddWithValue("@Duration", song.Duration ?? string.Empty);
        command.Parameters.AddWithValue("@DateAdded", song.DateAdded ?? string.Empty);
        command.Parameters.AddWithValue("@IsAutoDiscovered", song.IsAutoDiscovered ? 1 : 0);

        command.ExecuteNonQuery();
    }

    public void UpdateSongMetadata(Song song)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"
            UPDATE Songs
            SET Title = @Title, Artist = @Artist, Album = @Album, Duration = @Duration
            WHERE Id = @Id
        ";
        command.Parameters.AddWithValue("@Title", song.Title ?? string.Empty);
        command.Parameters.AddWithValue("@Artist", song.Artist ?? string.Empty);
        command.Parameters.AddWithValue("@Album", song.Album ?? string.Empty);
        command.Parameters.AddWithValue("@Duration", song.Duration ?? string.Empty);
        command.Parameters.AddWithValue("@Id", song.Id);

        command.ExecuteNonQuery();
    }

    public void DeleteSong(int id)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM Songs WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", id);

        command.ExecuteNonQuery();
    }

    public void SetFavorite(int id, bool isFavorite)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "UPDATE Songs SET IsFavorite = @IsFavorite WHERE Id = @Id";
        command.Parameters.AddWithValue("@IsFavorite", isFavorite ? 1 : 0);
        command.Parameters.AddWithValue("@Id", id);

        command.ExecuteNonQuery();
    }
}
