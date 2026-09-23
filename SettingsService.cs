using System;
using System.IO;
using System.Text.Json;

namespace MusicPlayer;

public class SettingsService
{
    private const string SettingsFileName = "settings.json";
    
    public int DefaultVolume { get; set; } = 100;
    public bool StartPlaybackAutomatically { get; set; } = false;
    public bool ConfirmBeforeDeletingSongs { get; set; } = true;

    public void LoadSettings()
    {
        try
        {
            if (File.Exists(SettingsFileName))
            {
                string json = File.ReadAllText(SettingsFileName);
                var settings = JsonSerializer.Deserialize<SettingsService>(json);
                if (settings != null)
                {
                    DefaultVolume = settings.DefaultVolume;
                    StartPlaybackAutomatically = settings.StartPlaybackAutomatically;
                    ConfirmBeforeDeletingSongs = settings.ConfirmBeforeDeletingSongs;
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Failed to load settings: {ex.Message}");
        }
    }

    public void SaveSettings()
    {
        try
        {
            string json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(SettingsFileName, json);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Failed to save settings: {ex.Message}");
        }
    }
}
