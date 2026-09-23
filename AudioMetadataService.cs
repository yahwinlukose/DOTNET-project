// Import the System namespace for basic types and exceptions
using System;
// Import the System.IO namespace for file operations
using System.IO;
// Import the NAudio.Wave namespace for audio playback and analysis
using NAudio.Wave;

// Define the namespace for the application
namespace MusicPlayer;

// Declare a public class for extracting metadata from audio files
public class AudioMetadataService
{
    // Define a method to extract metadata from a given file path and return a Song object
    public Song ExtractMetadata(string filePath)
    {
        // Initialize a new Song object with basic information
        var song = new Song
        {
            // Set the FilePath property of the song to the provided file path
            FilePath = filePath,
            // Set the DateAdded property to the current date and time formatted as a string
            DateAdded = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        };

        // Start a try block to catch potential exceptions during metadata extraction
        try
        {
            // Attempt to read metadata using TagLib
            // Create a TagLib file object from the given file path, ensuring it is disposed
            using var file = TagLib.File.Create(filePath);

            // Assign the title from TagLib, or use the file name if the title is empty
            song.Title = string.IsNullOrWhiteSpace(file.Tag.Title) 
                // Extract the file name without extension to use as a fallback title
                ? Path.GetFileNameWithoutExtension(filePath) 
                // Use the title found in the tags
                : file.Tag.Title;

            // Join multiple performers into a single comma-separated string for the Artist property
            song.Artist = string.Join(", ", file.Tag.Performers);
            // Check if the joined Artist string is empty or whitespace
            if (string.IsNullOrWhiteSpace(song.Artist))
            {
                // Fallback to the first performer, or an empty string if null
                song.Artist = file.Tag.FirstPerformer ?? string.Empty;
            }

            // Assign the album from TagLib, falling back to an empty string if null
            song.Album = file.Tag.Album ?? string.Empty;

            // Check if the file properties are available
            if (file.Properties != null)
            {
                // Format the duration as minutes and seconds and assign it to the song
                song.Duration = file.Properties.Duration.ToString(@"mm\:ss");
            }
        }
        // Catch any exception that occurs during TagLib metadata extraction
        catch (Exception ex)
        {
            // Log the exception message to the debug console
            System.Diagnostics.Debug.WriteLine($"TagLib failed for {filePath}: {ex.Message}");
            // Fallback for metadata
            // Use the file name without extension as the title
            song.Title = Path.GetFileNameWithoutExtension(filePath);
            // Set the artist to an empty string
            song.Artist = string.Empty;
            // Set the album to an empty string
            song.Album = string.Empty;
        }

        // If duration is still empty or missing, fallback to NAudio
        // Check if the duration is missing or just "00:00"
        if (string.IsNullOrWhiteSpace(song.Duration) || song.Duration == "00:00")
        {
            // Start a try block for the NAudio fallback approach
            try
            {
                // Create an AudioFileReader for the file path, ensuring it is disposed
                using var audioFileReader = new AudioFileReader(filePath);
                // Assign the total time from NAudio, formatted as minutes and seconds
                song.Duration = audioFileReader.TotalTime.ToString(@"mm\:ss");
            }
            // Catch any exception that occurs during NAudio duration extraction
            catch (Exception ex)
            {
                // Log the exception message to the debug console
                System.Diagnostics.Debug.WriteLine($"NAudio fallback failed for {filePath}: {ex.Message}");
                // Set the duration to an empty string on failure
                song.Duration = string.Empty;
            }
        }

        // Return the populated Song object
        return song;
    }

    // Define a method to extract the artwork image from an audio file
    public System.Drawing.Image? GetArtwork(string filePath)
    {
        // Start a try block to handle potential errors while reading the image
        try
        {
            // Create a TagLib file object from the given file path, ensuring it is disposed
            using var file = TagLib.File.Create(filePath);
            // Check if the audio file contains any pictures in its tags
            if (file.Tag.Pictures.Length > 0)
            {
                // Get the first picture available in the tags
                var picture = file.Tag.Pictures[0];
                // Create a memory stream from the picture data bytes, ensuring it is disposed
                using var ms = new MemoryStream(picture.Data.Data);
                // Create an Image object from the memory stream, ensuring the original is disposed
                using var tempImage = System.Drawing.Image.FromStream(ms);
                // Return a new Bitmap instance created from the temporary image
                return new System.Drawing.Bitmap(tempImage);
            }
        }
        // Catch any exception that occurs during artwork extraction
        catch (Exception ex)
        {
            // Log the exception message to the debug console
            System.Diagnostics.Debug.WriteLine($"Failed to load artwork for {filePath}: {ex.Message}");
        }

        // Return null if no artwork was found or if an exception occurred
        return null;
    }
}
