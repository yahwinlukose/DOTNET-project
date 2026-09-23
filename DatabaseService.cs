// Import the System namespace for basic types and exceptions
using System;
// Import the System.IO namespace for file and path operations
using System.IO;
// Import the Microsoft.Data.Sqlite namespace for SQLite database interactions
using Microsoft.Data.Sqlite;

// Define the namespace for the application
namespace MusicPlayer;

// Declare a public class for database operations
public class DatabaseService
{
    // Declare a private readonly field to store the database file path
    private readonly string _dbFilePath;
    // Declare a private readonly field to store the database connection string
    private readonly string _connectionString;

    // Define the constructor for the DatabaseService class
    public DatabaseService()
    {
        // Construct the full path to the database file in the application's base directory
        _dbFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "musicplayer.db");
        // Create the SQLite connection string using the database file path
        _connectionString = $"Data Source={_dbFilePath}";
    }

    // Expose the database file path via a public property
    public string DbFilePath => _dbFilePath;

    // Define a method to initialize the database and its schema
    public void InitializeDatabase()
    {
        // Create a new SQLite connection using the connection string and ensure it is disposed
        using var connection = new SqliteConnection(_connectionString);
        // Open the database connection
        connection.Open();

        // Create a new SQLite command associated with the connection
        var command = connection.CreateCommand();
        // Set the SQL command text to create the Songs table if it doesn't exist
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
        // Execute the command to create the table
        command.ExecuteNonQuery();

        // Migration for IsFavorite - create a new command to check existing columns
        var checkCmd = connection.CreateCommand();
        // Set the SQL command text to retrieve table information for Songs
        checkCmd.CommandText = "PRAGMA table_info(Songs)";
        // Execute the command and get a data reader, ensuring it is disposed
        using var reader = checkCmd.ExecuteReader();
        // Initialize a boolean flag to track if the IsFavorite column exists
        bool hasIsFavorite = false;
        // Loop through the results returned by the reader
        while (reader.Read())
        {
            // Check if the column name (at index 1) is 'IsFavorite'
            if (reader.GetString(1) == "IsFavorite")
            {
                // Set the flag to true since the column exists
                hasIsFavorite = true;
                // Exit the loop as we found the column
                break;
            }
        }
        // Close the data reader explicitly
        reader.Close();

        // Check if the IsFavorite column is missing
        if (!hasIsFavorite)
        {
            // Create a new command to alter the table structure
            var alterCmd = connection.CreateCommand();
            // Set the SQL command text to add the IsFavorite column with a default value of 0
            alterCmd.CommandText = "ALTER TABLE Songs ADD COLUMN IsFavorite INTEGER NOT NULL DEFAULT 0";
            // Execute the command to alter the table
            alterCmd.ExecuteNonQuery();
        }
    }

    // Define a method to retrieve all songs from the database
    public System.Collections.Generic.List<Song> GetSongs()
    {
        // Initialize a new list to hold the retrieved songs
        var songs = new System.Collections.Generic.List<Song>();
        // Create a new SQLite connection using the connection string and ensure it is disposed
        using var connection = new SqliteConnection(_connectionString);
        // Open the database connection
        connection.Open();

        // Create a new SQLite command associated with the connection
        var command = connection.CreateCommand();
        // Set the SQL command text to select all relevant columns from the Songs table
        command.CommandText = "SELECT Id, Title, Artist, Album, FilePath, Duration, DateAdded, IsFavorite FROM Songs";
        
        // Execute the command and get a data reader, ensuring it is disposed
        using var reader = command.ExecuteReader();
        // Loop through the results returned by the reader
        while (reader.Read())
        {
            // Add a new Song object to the list for each row in the result set
            songs.Add(new Song
            {
                // Retrieve the Id as an integer from the first column (index 0)
                Id = reader.GetInt32(0),
                // Retrieve the Title as a string, handling null values (index 1)
                Title = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                // Retrieve the Artist as a string, handling null values (index 2)
                Artist = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                // Retrieve the Album as a string, handling null values (index 3)
                Album = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                // Retrieve the FilePath as a string, handling null values (index 4)
                FilePath = reader.IsDBNull(4) ? string.Empty : reader.GetString(4),
                // Retrieve the Duration as a string, handling null values (index 5)
                Duration = reader.IsDBNull(5) ? string.Empty : reader.GetString(5),
                // Retrieve the DateAdded as a string, handling null values (index 6)
                DateAdded = reader.IsDBNull(6) ? string.Empty : reader.GetString(6),
                // Retrieve IsFavorite as a boolean, checking for null and integer value of 1 (index 7)
                IsFavorite = !reader.IsDBNull(7) && reader.GetInt32(7) == 1
            });
        }
        // Return the populated list of songs
        return songs;
    }

    // Define a method to check if a song with the specified file path already exists in the database
    public bool SongExists(string filePath)
    {
        // Create a new SQLite connection using the connection string and ensure it is disposed
        using var connection = new SqliteConnection(_connectionString);
        // Open the database connection
        connection.Open();

        // Create a new SQLite command associated with the connection
        var command = connection.CreateCommand();
        // Set the SQL command text to count rows matching the provided file path
        command.CommandText = "SELECT COUNT(1) FROM Songs WHERE FilePath = @FilePath";
        // Add the FilePath parameter to the command with its value
        command.Parameters.AddWithValue("@FilePath", filePath);

        // Execute the command and retrieve the scalar result (the count)
        var result = command.ExecuteScalar();
        // Return true if the count is greater than 0, indicating the song exists
        return Convert.ToInt32(result) > 0;
    }

    // Define a method to insert a new song into the database
    public void AddSong(Song song)
    {
        // Create a new SQLite connection using the connection string and ensure it is disposed
        using var connection = new SqliteConnection(_connectionString);
        // Open the database connection
        connection.Open();

        // Create a new SQLite command associated with the connection
        var command = connection.CreateCommand();
        // Set the SQL command text to insert a new row into the Songs table
        command.CommandText = @"
            INSERT INTO Songs (Title, Artist, Album, FilePath, Duration, DateAdded)
            VALUES (@Title, @Artist, @Album, @FilePath, @Duration, @DateAdded)
        ";
        // Add the Title parameter, falling back to an empty string if null
        command.Parameters.AddWithValue("@Title", song.Title ?? string.Empty);
        // Add the Artist parameter, falling back to an empty string if null
        command.Parameters.AddWithValue("@Artist", song.Artist ?? string.Empty);
        // Add the Album parameter, falling back to an empty string if null
        command.Parameters.AddWithValue("@Album", song.Album ?? string.Empty);
        // Add the FilePath parameter, falling back to an empty string if null
        command.Parameters.AddWithValue("@FilePath", song.FilePath ?? string.Empty);
        // Add the Duration parameter, falling back to an empty string if null
        command.Parameters.AddWithValue("@Duration", song.Duration ?? string.Empty);
        // Add the DateAdded parameter, falling back to an empty string if null
        command.Parameters.AddWithValue("@DateAdded", song.DateAdded ?? string.Empty);

        // Execute the command to perform the insert operation
        command.ExecuteNonQuery();
    }

    // Define a method to delete a song from the database by its ID
    public void DeleteSong(int id)
    {
        // Create a new SQLite connection using the connection string and ensure it is disposed
        using var connection = new SqliteConnection(_connectionString);
        // Open the database connection
        connection.Open();

        // Create a new SQLite command associated with the connection
        var command = connection.CreateCommand();
        // Set the SQL command text to delete a row from the Songs table matching the provided ID
        command.CommandText = "DELETE FROM Songs WHERE Id = @Id";
        // Add the Id parameter to the command with its value
        command.Parameters.AddWithValue("@Id", id);

        // Execute the command to perform the delete operation
        command.ExecuteNonQuery();
    }

    // Define a method to update the favorite status of a song by its ID
    public void SetFavorite(int id, bool isFavorite)
    {
        // Create a new SQLite connection using the connection string and ensure it is disposed
        using var connection = new SqliteConnection(_connectionString);
        // Open the database connection
        connection.Open();

        // Create a new SQLite command associated with the connection
        var command = connection.CreateCommand();
        // Set the SQL command text to update the IsFavorite column for the matching ID
        command.CommandText = "UPDATE Songs SET IsFavorite = @IsFavorite WHERE Id = @Id";
        // Add the IsFavorite parameter, converting boolean to integer (1 for true, 0 for false)
        command.Parameters.AddWithValue("@IsFavorite", isFavorite ? 1 : 0);
        // Add the Id parameter to the command with its value
        command.Parameters.AddWithValue("@Id", id);

        // Execute the command to perform the update operation
        command.ExecuteNonQuery();
    }
}
