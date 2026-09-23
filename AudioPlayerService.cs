// Import the System namespace for core functionality and events
using System;
// Import NAudio.Wave for audio playback and handling
using NAudio.Wave;

// Define the namespace for the application
namespace MusicPlayer;

// Declare a public class for playing audio, implementing IDisposable to manage resources
public class AudioPlayerService : IDisposable
{
    // A private field to hold the audio file reader instance
    private AudioFileReader? _audioFileReader;
    // A private field to hold the wave output device for playback
    private WaveOut? _waveOut;
    // A private field to store the current volume level, defaulting to 1.0 (max)
    private float _volume = 1.0f;
    // A private flag indicating if playback was stopped manually by the user
    private bool _isManualStop = false;

    // Declare a public event triggered when playback finishes naturally
    public event EventHandler? PlaybackFinished;

    // A public property to check if audio is currently playing
    public bool IsPlaying => _waveOut?.PlaybackState == PlaybackState.Playing;
    // A public property to check if an audio file is currently loaded
    public bool IsLoaded => _audioFileReader != null;

    // A public property to get or set the playback volume
    public float Volume
    {
        // Get the current volume level
        get => _volume;
        // Set a new volume level
        set
        {
            // Clamp the volume value between 0.0 and 1.0
            _volume = Math.Clamp(value, 0f, 1f);
            // If the wave output device is initialized
            if (_waveOut != null)
            {
                // Apply the clamped volume to the wave output device
                _waveOut.Volume = _volume;
            }
        }
    }

    // A public property to get the current playback state, defaulting to Stopped
    public PlaybackState State => _waveOut?.PlaybackState ?? PlaybackState.Stopped;

    // A public property to get the current playback position as a TimeSpan
    public TimeSpan CurrentPosition => _audioFileReader?.CurrentTime ?? TimeSpan.Zero;
    // A public property to get the total duration of the loaded audio file
    public TimeSpan TotalDuration => _audioFileReader?.TotalTime ?? TimeSpan.Zero;

    // A public method to load an audio file for playback
    public void Load(string filePath)
    {
        // Set the manual stop flag to true to prevent triggering the PlaybackFinished event
        _isManualStop = true;
        // Dispose any currently playing or loaded resources
        DisposeCurrent();

        // Initialize the audio file reader with the specified file path
        _audioFileReader = new AudioFileReader(filePath);
        // Initialize the wave output device
        _waveOut = new WaveOut();
        // Set the volume of the wave output device
        _waveOut.Volume = _volume;
        // Subscribe to the PlaybackStopped event of the wave output device
        _waveOut.PlaybackStopped += OnPlaybackStopped;
        // Initialize the wave output device with the audio file reader
        _waveOut.Init(_audioFileReader);
    }

    // Event handler for when playback stops
    private void OnPlaybackStopped(object? sender, StoppedEventArgs e)
    {
        // Check if the stop was not triggered manually (e.g., reaching the end of the file)
        if (!_isManualStop)
        {
            // Invoke the PlaybackFinished event if there are subscribers
            PlaybackFinished?.Invoke(this, EventArgs.Empty);
        }
    }

    // A public method to start or resume playback
    public void Play()
    {
        // Check if the wave output device is initialized and not already playing
        if (_waveOut != null && _waveOut.PlaybackState != PlaybackState.Playing)
        {
            // Reset the manual stop flag
            _isManualStop = false;
            // Check if we reached the end of the audio file
            if (_audioFileReader != null && _audioFileReader.Position >= _audioFileReader.Length)
            {
                // Reset the playback position to the beginning
                _audioFileReader.Position = 0;
            }
            // Start playing the audio
            _waveOut.Play();
        }
    }

    // A public method to pause playback
    public void Pause()
    {
        // Check if the wave output device is initialized and currently playing
        if (_waveOut != null && _waveOut.PlaybackState == PlaybackState.Playing)
        {
            // Set the manual stop flag to true
            _isManualStop = true;
            // Pause the audio playback
            _waveOut.Pause();
        }
    }

    // A public method to stop playback
    public void Stop()
    {
        // Check if the wave output device is initialized
        if (_waveOut != null)
        {
            // Set the manual stop flag to true
            _isManualStop = true;
            // Using Pause instead of Stop to avoid closing the device.
            // WaveOut.Stop() can require Re-Init in some cases, so Pause + Reset Position is safer.
            _waveOut.Pause();
            // Check if the audio file reader is initialized
            if (_audioFileReader != null)
            {
                // Reset the playback position to the beginning
                _audioFileReader.Position = 0;
            }
        }
    }

    // A public method to seek to a specific position in the audio file
    public void Seek(TimeSpan position)
    {
        // Check if the audio file reader is initialized
        if (_audioFileReader != null)
        {
            // Ensure the requested position is not less than zero
            if (position < TimeSpan.Zero) position = TimeSpan.Zero;
            // Ensure the requested position is not greater than the total duration
            if (position > _audioFileReader.TotalTime) position = _audioFileReader.TotalTime;
            // Set the current time of the audio file reader to the requested position
            _audioFileReader.CurrentTime = position;
        }
    }

    // A public method to fast forward playback by a specified amount
    public void FastForward(TimeSpan amount)
    {
        // Check if the audio file reader is initialized
        if (_audioFileReader != null)
        {
            // Calculate the new playback position
            var newPosition = _audioFileReader.CurrentTime + amount;
            // Check if the new position exceeds the total duration
            if (newPosition > _audioFileReader.TotalTime)
            {
                // Cap the new position at the total duration
                newPosition = _audioFileReader.TotalTime;
            }
            // Set the current time of the audio file reader to the new position
            _audioFileReader.CurrentTime = newPosition;
        }
    }

    // A private method to dispose of the current wave output and audio file reader
    private void DisposeCurrent()
    {
        // Check if the wave output device is initialized
        if (_waveOut != null)
        {
            // Stop playback on the wave output device
            _waveOut.Stop();
            // Dispose the wave output device
            _waveOut.Dispose();
            // Set the wave output device reference to null
            _waveOut = null;
        }

        // Check if the audio file reader is initialized
        if (_audioFileReader != null)
        {
            // Dispose the audio file reader
            _audioFileReader.Dispose();
            // Set the audio file reader reference to null
            _audioFileReader = null;
        }
    }

    // A public method to dispose of all resources used by the service
    public void Dispose()
    {
        // Call the private DisposeCurrent method to clean up
        DisposeCurrent();
    }
}
