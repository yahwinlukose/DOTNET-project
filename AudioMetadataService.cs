using System;
using System.IO;
using NAudio.Wave;

namespace MusicPlayer;

public class AudioMetadataService
{
    public Song ExtractMetadata(string filePath)
    {
        var song = new Song
        {
            FilePath = filePath,
            DateAdded = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        };

        try
        {
            // Attempt to read metadata using TagLib
            using var file = TagLib.File.Create(filePath);

            song.Title = string.IsNullOrWhiteSpace(file.Tag.Title) 
                ? Path.GetFileNameWithoutExtension(filePath) 
                : file.Tag.Title;

            song.Artist = string.Join(", ", file.Tag.Performers);
            if (string.IsNullOrWhiteSpace(song.Artist))
            {
                song.Artist = file.Tag.FirstPerformer ?? string.Empty;
            }

            song.Album = file.Tag.Album ?? string.Empty;

            if (file.Properties != null)
            {
                song.Duration = file.Properties.Duration.ToString(@"mm\:ss");
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"TagLib failed for {filePath}: {ex.Message}");
            // Fallback for metadata
            song.Title = Path.GetFileNameWithoutExtension(filePath);
            song.Artist = string.Empty;
            song.Album = string.Empty;
        }

        // If duration is still empty or missing, fallback to NAudio
        if (string.IsNullOrWhiteSpace(song.Duration) || song.Duration == "00:00")
        {
            try
            {
                using var audioFileReader = new AudioFileReader(filePath);
                song.Duration = audioFileReader.TotalTime.ToString(@"mm\:ss");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"NAudio fallback failed for {filePath}: {ex.Message}");
                song.Duration = string.Empty;
            }
        }

        return song;
    }

    public System.Drawing.Image? GetArtwork(string filePath)
    {
        try
        {
            using var file = TagLib.File.Create(filePath);
            if (file.Tag.Pictures.Length > 0)
            {
                var picture = file.Tag.Pictures[0];
                using var ms = new MemoryStream(picture.Data.Data);
                using var tempImage = System.Drawing.Image.FromStream(ms);
                return new System.Drawing.Bitmap(tempImage);
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Failed to load artwork for {filePath}: {ex.Message}");
        }

        return null;
    }
}
