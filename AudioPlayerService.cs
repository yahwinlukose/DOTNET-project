using System;
using NAudio.Wave;

namespace MusicPlayer;

public class AudioPlayerService : IDisposable
{
    private AudioFileReader? _audioFileReader;
    private WaveOut? _waveOut;
    private float _volume = 1.0f;
    private bool _isManualStop = false;

    public event EventHandler? PlaybackFinished;

    public bool IsPlaying => _waveOut?.PlaybackState == PlaybackState.Playing;
    public bool IsLoaded => _audioFileReader != null;

    public float Volume
    {
        get => _volume;
        set
        {
            _volume = Math.Clamp(value, 0f, 1f);
            if (_waveOut != null)
            {
                _waveOut.Volume = _volume;
            }
        }
    }

    public PlaybackState State => _waveOut?.PlaybackState ?? PlaybackState.Stopped;

    public TimeSpan CurrentPosition => _audioFileReader?.CurrentTime ?? TimeSpan.Zero;
    public TimeSpan TotalDuration => _audioFileReader?.TotalTime ?? TimeSpan.Zero;

    public void Load(string filePath)
    {
        _isManualStop = true;
        DisposeCurrent();

        _audioFileReader = new AudioFileReader(filePath);
        _waveOut = new WaveOut();
        _waveOut.Volume = _volume;
        _waveOut.PlaybackStopped += OnPlaybackStopped;
        _waveOut.Init(_audioFileReader);
    }

    private void OnPlaybackStopped(object? sender, StoppedEventArgs e)
    {
        bool isCurrentWaveOut = ReferenceEquals(sender, _waveOut);
        bool isError = e.Exception != null;
        long position = 0;
        long length = 0;
        bool isEof = false;

        System.Diagnostics.Debug.WriteLine($"[AUDIO] PlaybackStopped fired. isCurrent={isCurrentWaveOut}, manual={_isManualStop}, error={isError}");

        if (!isCurrentWaveOut)
        {
            System.Diagnostics.Debug.WriteLine("[AUDIO] Ignoring PlaybackStopped from an old/disposed WaveOut instance.");
            return;
        }

        if (_audioFileReader != null)
        {
            try
            {
                position = _audioFileReader.Position;
                length = _audioFileReader.Length;
                
                if (length > 0)
                {
                    // Allow 1 second tolerance for EOF due to potential MP3 frame rounding
                    long tolerance = _audioFileReader.WaveFormat.AverageBytesPerSecond;
                    isEof = position >= (length - tolerance);
                }
            }
            catch { }
        }

        System.Diagnostics.Debug.WriteLine($"[AUDIO] Playback details: pos={position}, len={length}, isEof={isEof}");

        if (!_isManualStop && !isError && isEof)
        {
            System.Diagnostics.Debug.WriteLine("[AUDIO] Raising PlaybackFinished (Genuine EOF reached).");
            PlaybackFinished?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            System.Diagnostics.Debug.WriteLine("[AUDIO] PlaybackFinished NOT raised (Premature stop or manual).");
        }
    }

    public void Play()
    {
        if (_waveOut != null && _waveOut.PlaybackState != PlaybackState.Playing)
        {
            _isManualStop = false;
            // Reset position if we reached the end
            if (_audioFileReader != null && _audioFileReader.Position >= _audioFileReader.Length)
            {
                _audioFileReader.Position = 0;
            }
            _waveOut.Play();
        }
    }

    public void Pause()
    {
        if (_waveOut != null && _waveOut.PlaybackState == PlaybackState.Playing)
        {
            _isManualStop = true;
            _waveOut.Pause();
        }
    }

    public void Stop()
    {
        if (_waveOut != null)
        {
            _isManualStop = true;
            // Using Pause instead of Stop to avoid closing the device.
            // WaveOut.Stop() can require Re-Init in some cases, so Pause + Reset Position is safer.
            _waveOut.Pause();
            if (_audioFileReader != null)
            {
                _audioFileReader.Position = 0;
            }
        }
    }

    public void Seek(TimeSpan position)
    {
        if (_audioFileReader != null)
        {
            if (position < TimeSpan.Zero) position = TimeSpan.Zero;
            if (position > _audioFileReader.TotalTime) position = _audioFileReader.TotalTime;
            _audioFileReader.CurrentTime = position;
        }
    }

    public void FastForward(TimeSpan amount)
    {
        if (_audioFileReader != null)
        {
            var newPosition = _audioFileReader.CurrentTime + amount;
            if (newPosition > _audioFileReader.TotalTime)
            {
                newPosition = _audioFileReader.TotalTime;
            }
            _audioFileReader.CurrentTime = newPosition;
        }
    }

    private void DisposeCurrent()
    {
        if (_waveOut != null)
        {
            _waveOut.Stop();
            _waveOut.Dispose();
            _waveOut = null;
        }

        if (_audioFileReader != null)
        {
            _audioFileReader.Dispose();
            _audioFileReader = null;
        }
    }

    public void Dispose()
    {
        DisposeCurrent();
    }
}
