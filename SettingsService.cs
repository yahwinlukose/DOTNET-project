using System;
using System.IO;
using System.Text.Json;

namespace MusicPlayer;

public enum ScanComputerChoice
{
    NotSet,
    Scan,
    NotNow
}

public class AppSettings
{
    public ScanComputerChoice ScanChoice { get; set; } = ScanComputerChoice.NotSet;
}

public class SettingsService
{
    private readonly string _settingsFilePath;

    public SettingsService()
    {
        string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        string musicPlayerFolder = Path.Combine(appDataFolder, "MusicPlayer");
        Directory.CreateDirectory(musicPlayerFolder);
        _settingsFilePath = Path.Combine(musicPlayerFolder, "settings.json");
    }

    public AppSettings LoadSettings()
    {
        if (!File.Exists(_settingsFilePath))
        {
            return new AppSettings();
        }

        try
        {
            string json = File.ReadAllText(_settingsFilePath);
            return JsonSerializer.Deserialize<AppSettings>(json) ?? new AppSettings();
        }
        catch
        {
            return new AppSettings();
        }
    }

    public void SaveSettings(AppSettings settings)
    {
        try
        {
            string json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_settingsFilePath, json);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Failed to save settings: {ex.Message}");
        }
    }
}
