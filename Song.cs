namespace MusicPlayer;

public class Song
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Artist { get; set; } = string.Empty;
    public string Album { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public string Duration { get; set; } = string.Empty;
    public string DateAdded { get; set; } = string.Empty;
    public bool IsFavorite { get; set; }
    
    public override string ToString()
    {
        if (string.IsNullOrWhiteSpace(Artist) && string.IsNullOrWhiteSpace(Title))
            return FilePath;
            
        if (string.IsNullOrWhiteSpace(Artist))
            return Title;
            
        return $"{Artist} - {Title}";
    }
}
