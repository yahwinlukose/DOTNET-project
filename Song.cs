// Define the namespace for the application
namespace MusicPlayer;

// Declare a public class that represents a Song entity
public class Song
{
    // A unique identifier for the song in the database
    public int Id { get; set; }
    // The title of the song, defaults to an empty string
    public string Title { get; set; } = string.Empty;
    // The name of the artist performing the song, defaults to an empty string
    public string Artist { get; set; } = string.Empty;
    // The name of the album the song belongs to, defaults to an empty string
    public string Album { get; set; } = string.Empty;
    // The absolute file path where the audio file is located on disk
    public string FilePath { get; set; } = string.Empty;
    // A formatted string representing the duration of the song (e.g., "3:45")
    public string Duration { get; set; } = string.Empty;
    // A formatted string representing when the song was added to the library
    public string DateAdded { get; set; } = string.Empty;
    // A boolean flag indicating whether the user has marked this song as a favorite
    public bool IsFavorite { get; set; }
    
    // Override the default ToString method to provide a custom string representation
    public override string ToString()
    {
        // If both the artist and title are empty or whitespace
        if (string.IsNullOrWhiteSpace(Artist) && string.IsNullOrWhiteSpace(Title))
            // Return just the file path as a fallback
            return FilePath;
            
        // If the artist is empty or whitespace but a title exists
        if (string.IsNullOrWhiteSpace(Artist))
            // Return only the title
            return Title;
            
        // If both artist and title are present, return them combined
        return $"{Artist} - {Title}";
    }
}
