namespace MusicPlayer;

public partial class Form1 : Form
{
    private readonly DatabaseService _databaseService;
    private readonly AudioPlayerService _audioPlayerService;
    private readonly AudioMetadataService _audioMetadataService;
    private readonly System.Windows.Forms.Timer _playbackTimer;
    private bool _isDraggingProgress;
    private int? _currentLoadedSongId;
    private System.Collections.Generic.List<Song> _allSongs = new();
    private enum RepeatMode { Off, All, One }
    private RepeatMode _repeatMode = RepeatMode.Off;
    private bool _isShuffleOn = false;
    private Random _random = new Random();

    public Form1()
    {
        InitializeComponent();
        _databaseService = new DatabaseService();
        _databaseService.InitializeDatabase();
        
        _audioPlayerService = new AudioPlayerService();
        _audioPlayerService.PlaybackFinished += AudioPlayerService_PlaybackFinished;
        _audioMetadataService = new AudioMetadataService();
        this.FormClosing += Form1_FormClosing;
        
        _playbackTimer = new System.Windows.Forms.Timer();
        _playbackTimer.Interval = 100;
        _playbackTimer.Tick += PlaybackTimer_Tick;
        _playbackTimer.Start();

        tbVolume.Value = 100;
        lblVolume.Text = "Volume: 100%";
        _audioPlayerService.Volume = 1.0f;

        cmbFilter.Items.Add("All Songs");
        cmbFilter.Items.Add("Favorites");
        cmbFilter.SelectedIndex = 0;

        LoadSongs();
    }

    private void TbVolume_ValueChanged(object? sender, EventArgs e)
    {
        lblVolume.Text = $"Volume: {tbVolume.Value}%";
        _audioPlayerService.Volume = tbVolume.Value / 100f;
    }

    private void PlaybackTimer_Tick(object? sender, EventArgs e)
    {
        if (_audioPlayerService.IsLoaded && !_isDraggingProgress)
        {
            var current = _audioPlayerService.CurrentPosition;
            var total = _audioPlayerService.TotalDuration;
            
            lblCurrentTime.Text = current.ToString(@"mm\:ss");
            if (total.TotalSeconds > 0)
            {
                lblTotalTime.Text = total.ToString(@"mm\:ss");
                tbProgress.Maximum = (int)total.TotalSeconds;
                int currentSeconds = (int)current.TotalSeconds;
                if (currentSeconds >= 0 && currentSeconds <= tbProgress.Maximum)
                {
                    tbProgress.Value = currentSeconds;
                }

                if (!_audioPlayerService.IsPlaying && current >= total && total > TimeSpan.Zero)
                {
                    btnPlayPause.Text = "▶";
                    lblCurrentTime.Text = total.ToString(@"mm\:ss");
                }
            }
        }
    }

    private void TbProgress_MouseDown(object? sender, MouseEventArgs e)
    {
        _isDraggingProgress = true;
    }

    private void TbProgress_MouseUp(object? sender, MouseEventArgs e)
    {
        if (_audioPlayerService.IsLoaded)
        {
            _audioPlayerService.Seek(TimeSpan.FromSeconds(tbProgress.Value));
        }
        _isDraggingProgress = false;
    }

    private void Form1_FormClosing(object? sender, FormClosingEventArgs e)
    {
        _playbackTimer.Stop();
        _playbackTimer.Dispose();
        _audioPlayerService.Dispose();
    }

    private void LoadSongs()
    {
        _allSongs = _databaseService.GetSongs();
        ApplySearchFilter();
    }

    private void ApplySearchFilter()
    {
        var searchText = txtSearch.Text?.Trim().ToLowerInvariant() ?? string.Empty;
        var showFavoritesOnly = cmbFilter.SelectedIndex == 1;
        
        var filteredSongs = _allSongs.Where(s => 
            (!showFavoritesOnly || s.IsFavorite) &&
            (string.IsNullOrEmpty(searchText) || 
             (s.Title != null && s.Title.ToLowerInvariant().Contains(searchText)) ||
             (s.Artist != null && s.Artist.ToLowerInvariant().Contains(searchText)) ||
             (s.Album != null && s.Album.ToLowerInvariant().Contains(searchText)))
        ).ToList();

        int? currentSelectionId = (lstSongs.SelectedItem as Song)?.Id;

        lstSongs.BeginUpdate();
        lstSongs.Items.Clear();
        Song? itemToSelect = null;
        foreach (var song in filteredSongs)
        {
            lstSongs.Items.Add(song);
            if (currentSelectionId.HasValue && song.Id == currentSelectionId.Value)
            {
                itemToSelect = song;
            }
        }
        lstSongs.EndUpdate();
        
        if (itemToSelect != null)
        {
            lstSongs.SelectedItem = itemToSelect;
        }
        
        lblSongCount.Text = $"{filteredSongs.Count} Songs";
    }

    private void TxtSearch_TextChanged(object? sender, EventArgs e)
    {
        ApplySearchFilter();
    }

    private void BtnClearSearch_Click(object? sender, EventArgs e)
    {
        txtSearch.Text = string.Empty;
    }

    private void CmbFilter_SelectedIndexChanged(object? sender, EventArgs e)
    {
        ApplySearchFilter();
    }

    private void LstSongs_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (lstSongs.SelectedItem is Song selectedSong)
        {
            UpdateFavoriteButtonText(selectedSong);

            if (_currentLoadedSongId == selectedSong.Id)
            {
                // Already loaded; no need to restart audio
                return;
            }

            try
            {
                _currentLoadedSongId = selectedSong.Id;
                _audioPlayerService.Load(selectedSong.FilePath);
                lblSongTitle.Text = string.IsNullOrWhiteSpace(selectedSong.Title) ? "Unknown Title" : selectedSong.Title;
                
                var artistStr = string.IsNullOrWhiteSpace(selectedSong.Artist) ? "Unknown Artist" : selectedSong.Artist;
                var albumStr = string.IsNullOrWhiteSpace(selectedSong.Album) ? "Unknown Album" : selectedSong.Album;
                lblArtist.Text = $"{artistStr} • {albumStr}";
                
                lblTotalTime.Text = _audioPlayerService.TotalDuration.ToString(@"mm\:ss");
                lblCurrentTime.Text = "00:00";
                tbProgress.Value = 0;
                btnPlayPause.Text = "▶";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading audio file:\n{ex.Message}", "Playback Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private void BtnPlayPause_Click(object? sender, EventArgs e)
    {
        if (!_audioPlayerService.IsLoaded)
        {
            MessageBox.Show("Please select a song to play.", "No Song Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        try
        {
            if (_audioPlayerService.IsPlaying)
            {
                _audioPlayerService.Pause();
                btnPlayPause.Text = "▶";
            }
            else
            {
                _audioPlayerService.Play();
                btnPlayPause.Text = "⏸";
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error playing audio:\n{ex.Message}", "Playback Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnStop_Click(object? sender, EventArgs e)
    {
        if (_audioPlayerService.IsLoaded)
        {
            _audioPlayerService.Stop();
            tbProgress.Value = 0;
            lblCurrentTime.Text = "00:00";
            btnPlayPause.Text = "▶";
        }
    }

    private void AudioPlayerService_PlaybackFinished(object? sender, EventArgs e)
    {
        if (this.InvokeRequired)
        {
            this.Invoke(new Action(() => AudioPlayerService_PlaybackFinished(sender, e)));
            return;
        }

        if (lstSongs.Items.Count == 0 || lstSongs.SelectedItem == null)
        {
            BtnStop_Click(null, EventArgs.Empty);
            return;
        }

        int currentIndex = lstSongs.SelectedIndex;
        
        if (_repeatMode == RepeatMode.One)
        {
            _audioPlayerService.Play();
            btnPlayPause.Text = "⏸";
        }
        else if (_isShuffleOn && lstSongs.Items.Count > 1)
        {
            int nextIndex;
            do
            {
                nextIndex = _random.Next(lstSongs.Items.Count);
            } while (nextIndex == currentIndex);

            lstSongs.SelectedIndex = nextIndex;
            _audioPlayerService.Play();
            btnPlayPause.Text = "⏸";
        }
        else if (!_isShuffleOn)
        {
            if (currentIndex < lstSongs.Items.Count - 1)
            {
                lstSongs.SelectedIndex = currentIndex + 1;
                _audioPlayerService.Play();
                btnPlayPause.Text = "⏸";
            }
            else if (_repeatMode == RepeatMode.All)
            {
                lstSongs.SelectedIndex = 0;
                _audioPlayerService.Play();
                btnPlayPause.Text = "⏸";
            }
            else
            {
                BtnStop_Click(null, EventArgs.Empty);
            }
        }
        else
        {
            if (_repeatMode == RepeatMode.All)
            {
                _audioPlayerService.Play();
                btnPlayPause.Text = "⏸";
            }
            else
            {
                BtnStop_Click(null, EventArgs.Empty);
            }
        }
    }

    private void BtnRepeat_Click(object? sender, EventArgs e)
    {
        _repeatMode = _repeatMode switch
        {
            RepeatMode.Off => RepeatMode.All,
            RepeatMode.All => RepeatMode.One,
            RepeatMode.One => RepeatMode.Off,
            _ => RepeatMode.Off
        };

        btnRepeat.Text = _repeatMode switch
        {
            RepeatMode.Off => "🔁 Repeat OFF",
            RepeatMode.All => "🔁 Repeat ALL",
            RepeatMode.One => "🔂 Repeat ONE",
            _ => "🔁 Repeat OFF"
        };
    }

    private void BtnShuffle_Click(object? sender, EventArgs e)
    {
        _isShuffleOn = !_isShuffleOn;
        btnShuffle.Text = _isShuffleOn ? "🔀 Shuffle ON" : "🔀 Shuffle OFF";
    }

    private void BtnFastForward_Click(object? sender, EventArgs e)
    {
        if (!_audioPlayerService.IsLoaded) return;
        _audioPlayerService.FastForward(TimeSpan.FromSeconds(10));
    }

    private void BtnAddMusic_Click(object? sender, EventArgs e)
    {
        using var openFileDialog = new OpenFileDialog();
        openFileDialog.Multiselect = true;
        openFileDialog.Filter = "Audio Files|*.mp3;*.wav;*.wma;*.aac;*.m4a";
        openFileDialog.Title = "Select Music Files";

        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            foreach (var filePath in openFileDialog.FileNames)
            {
                try
                {
                    if (!_databaseService.SongExists(filePath))
                    {
                        var song = _audioMetadataService.ExtractMetadata(filePath);
                        _databaseService.AddSong(song);
                    }
                }
                catch (Exception ex)
                {
                    // Ignore individual file errors and continue
                    System.Diagnostics.Debug.WriteLine($"Error adding file {filePath}: {ex.Message}");
                }
            }
            
            LoadSongs();
        }
    }

    private void BtnDeleteMusic_Click(object? sender, EventArgs e)
    {
        if (lstSongs.SelectedItem is not Song selectedSong)
        {
            MessageBox.Show("Please select a song to delete.", "No Song Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var result = MessageBox.Show($"Are you sure you want to remove '{selectedSong.Title}' from your library?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        if (result == DialogResult.Yes)
        {
            try
            {
                if (selectedSong.Id == _currentLoadedSongId)
                {
                    _audioPlayerService.Stop();
                    tbProgress.Value = 0;
                    lblCurrentTime.Text = "00:00";
                    btnPlayPause.Text = "▶";
                    _currentLoadedSongId = null;
                }

                _databaseService.DeleteSong(selectedSong.Id);
                LoadSongs();

                if (lstSongs.Items.Count == 0)
                {
                    lblSongTitle.Text = "Unknown Title";
                    lblArtist.Text = "Unknown Artist • Unknown Album";
                    lblTotalTime.Text = "00:00";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting song:\n{ex.Message}", "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private void UpdateFavoriteButtonText(Song song)
    {
        btnFavorite.Text = song.IsFavorite ? "★ Favorited" : "⭐ Favorite";
    }

    private void BtnFavorite_Click(object? sender, EventArgs e)
    {
        if (lstSongs.SelectedItem is not Song selectedSong)
        {
            MessageBox.Show("Please select a song first.", "No Song Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        try
        {
            bool newFavoriteState = !selectedSong.IsFavorite;
            _databaseService.SetFavorite(selectedSong.Id, newFavoriteState);
            LoadSongs();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error updating favorite:\n{ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
