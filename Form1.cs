using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MusicPlayer;

public partial class Form1 : Form
{
    private readonly DatabaseService _databaseService;
    private readonly AudioPlayerService _audioPlayerService;
    private readonly AudioMetadataService _audioMetadataService;
    private readonly SettingsService _settingsService;
    private readonly System.Windows.Forms.Timer _playbackTimer;
    private bool _isDraggingProgress;
    private int? _currentLoadedSongId;
    private System.Collections.Generic.List<Song> _allSongs = new();
    private enum RepeatMode { Off, All, One }
    private RepeatMode _repeatMode = RepeatMode.Off;
    private bool _isShuffleOn = false;
    private Random _random = new Random();
    private System.Drawing.Image _defaultArtwork;

    public Form1()
    {
        InitializeComponent();
        _databaseService = new DatabaseService();
        _databaseService.InitializeDatabase();

        _audioPlayerService = new AudioPlayerService();
        _audioPlayerService.PlaybackFinished += AudioPlayerService_PlaybackFinished;
        _audioMetadataService = new AudioMetadataService();
        _settingsService = new SettingsService();
        this.FormClosing += Form1_FormClosing;
        this.Shown += Form1_Shown;

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

        InitializeDefaultArtwork();
        LoadSongs();
    }

    private void InitializeDefaultArtwork()
    {
        var bmp = new Bitmap(150, 150);
        using (var g = Graphics.FromImage(bmp))
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.FromArgb(30, 30, 30));

            using var brush = new SolidBrush(Color.FromArgb(80, 80, 80));
            using var font = new Font("Segoe UI", 48, FontStyle.Bold);
            var stringFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            g.DrawString("🎵", font, brush, new RectangleF(0, 0, 150, 150), stringFormat);
        }
        _defaultArtwork = bmp;
        pbArtwork.Image = _defaultArtwork;
    }

    private void ClearArtwork()
    {
        var oldImage = pbArtwork.Image;
        pbArtwork.Image = _defaultArtwork;
        if (oldImage != null && oldImage != _defaultArtwork)
        {
            oldImage.Dispose();
        }
    }

    private void UpdateArtwork(string filePath)
    {
        var newImage = _audioMetadataService.GetArtwork(filePath);
        var oldImage = pbArtwork.Image;

        pbArtwork.Image = newImage ?? _defaultArtwork;

        if (oldImage != null && oldImage != _defaultArtwork)
        {
            oldImage.Dispose();
        }
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

        var img = pbArtwork.Image;
        pbArtwork.Image = null;
        if (img != null && img != _defaultArtwork) img.Dispose();
        _defaultArtwork?.Dispose();
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

    private bool LoadSelectedSong(Song selectedSong, bool silentFail = false)
    {
        if (_currentLoadedSongId == selectedSong.Id && _audioPlayerService.IsLoaded)
        {
            return true;
        }

        try
        {
            if (string.IsNullOrWhiteSpace(selectedSong.Duration) || selectedSong.Duration == "00:00")
            {
                var realMetadata = _audioMetadataService.ExtractMetadata(selectedSong.FilePath);
                selectedSong.Title = realMetadata.Title;
                selectedSong.Artist = realMetadata.Artist;
                selectedSong.Album = realMetadata.Album;
                selectedSong.Duration = realMetadata.Duration;
                _databaseService.UpdateSongMetadata(selectedSong);
            }

            _audioPlayerService.Load(selectedSong.FilePath);
            _currentLoadedSongId = selectedSong.Id;

            lblSongTitle.Text = string.IsNullOrWhiteSpace(selectedSong.Title) ? "Unknown Title" : selectedSong.Title;

            var artistStr = string.IsNullOrWhiteSpace(selectedSong.Artist) ? "Unknown Artist" : selectedSong.Artist;
            var albumStr = string.IsNullOrWhiteSpace(selectedSong.Album) ? "Unknown Album" : selectedSong.Album;
            lblArtist.Text = $"{artistStr} • {albumStr}";

            lblTotalTime.Text = _audioPlayerService.TotalDuration.ToString(@"mm\:ss");
            lblCurrentTime.Text = "00:00";
            tbProgress.Value = 0;
            btnPlayPause.Text = "▶";

            UpdateArtwork(selectedSong.FilePath);
            return true;
        }
        catch (Exception ex)
        {
            _currentLoadedSongId = null;
            if (!silentFail)
            {
                MessageBox.Show($"Error loading audio file:\n{ex.Message}", "Playback Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }
    }

    private bool _isProgrammaticSelection = false;

    private void LstSongs_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (lstSongs.SelectedItem is Song selectedSong)
        {
            UpdateFavoriteButtonText(selectedSong);
            if (!_isProgrammaticSelection)
            {
                LoadSelectedSong(selectedSong, silentFail: false);
            }
        }
    }

    private void LoadAndPlaySong(int index)
    {
        if (index < 0 || index >= lstSongs.Items.Count) return;
        if (lstSongs.Items[index] is not Song song) return;

        try
        {
            _isProgrammaticSelection = true;
            lstSongs.SelectedIndex = index;
        }
        finally
        {
            _isProgrammaticSelection = false;
        }

        if (LoadSelectedSong(song, silentFail: false))
        {
            _audioPlayerService.Stop();
            lblCurrentTime.Text = "00:00";
            tbProgress.Value = 0;
            _audioPlayerService.Play();
            btnPlayPause.Text = "⏸";
        }
    }

    private void BtnPrevious_Click(object? sender, EventArgs e)
    {
        if (lstSongs.Items.Count == 0 || lstSongs.SelectedItem == null) return;

        int currentIndex = lstSongs.SelectedIndex;
        if (currentIndex == 0)
        {
            if (_audioPlayerService.IsLoaded)
            {
                _audioPlayerService.Seek(TimeSpan.Zero);
                _audioPlayerService.Play();
                btnPlayPause.Text = "⏸";
            }
            return;
        }

        for (int i = currentIndex - 1; i >= 0; i--)
        {
            try
            {
                _isProgrammaticSelection = true;
                lstSongs.SelectedIndex = i;
            }
            finally
            {
                _isProgrammaticSelection = false;
            }
            if (lstSongs.Items[i] is Song song && LoadSelectedSong(song, silentFail: true))
            {
                _audioPlayerService.Play();
                btnPlayPause.Text = "⏸";
                return;
            }
        }

        BtnStop_Click(null, EventArgs.Empty);
    }

    private void BtnNext_Click(object? sender, EventArgs e)
    {
        PlayNextTrack();
    }

    private void PlayNextTrack()
    {
        if (lstSongs.Items.Count == 0 || lstSongs.SelectedItem == null) return;

        int currentIndex = lstSongs.SelectedIndex;

        if (_isShuffleOn && lstSongs.Items.Count > 1)
        {
            var candidates = System.Linq.Enumerable.Range(0, lstSongs.Items.Count)
                                .Where(i => i != currentIndex)
                                .OrderBy(x => _random.Next())
                                .ToList();

            foreach (var idx in candidates)
            {
                try
                {
                    _isProgrammaticSelection = true;
                    lstSongs.SelectedIndex = idx;
                }
                finally
                {
                    _isProgrammaticSelection = false;
                }
                if (lstSongs.Items[idx] is Song song && LoadSelectedSong(song, silentFail: true))
                {
                    _audioPlayerService.Play();
                    btnPlayPause.Text = "⏸";
                    return;
                }
            }
        }
        else
        {
            for (int attempts = 0; attempts < lstSongs.Items.Count; attempts++)
            {
                currentIndex++;
                if (currentIndex >= lstSongs.Items.Count) currentIndex = 0;

                try
                {
                    _isProgrammaticSelection = true;
                    lstSongs.SelectedIndex = currentIndex;
                }
                finally
                {
                    _isProgrammaticSelection = false;
                }
                if (lstSongs.Items[currentIndex] is Song song && LoadSelectedSong(song, silentFail: true))
                {
                    _audioPlayerService.Play();
                    btnPlayPause.Text = "⏸";
                    return;
                }
            }
        }

        BtnStop_Click(null, EventArgs.Empty);
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

        if (_repeatMode == RepeatMode.One)
        {
            _audioPlayerService.Seek(TimeSpan.Zero);
            _audioPlayerService.Play();
            btnPlayPause.Text = "⏸";
            return;
        }

        int currentIndex = lstSongs.SelectedIndex;
        if (!_isShuffleOn && currentIndex >= lstSongs.Items.Count - 1 && _repeatMode != RepeatMode.All)
        {
            BtnStop_Click(null, EventArgs.Empty);
            return;
        }

        PlayNextTrack();
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

        btnRepeat.Text = _repeatMode == RepeatMode.One ? "🔂" : "🔁";
        btnRepeat.BackColor = _repeatMode == RepeatMode.Off
            ? System.Drawing.Color.FromArgb(45, 45, 45)
            : System.Drawing.Color.SteelBlue;
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
                    ClearArtwork();
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

    private void Form1_Shown(object? sender, EventArgs e)
    {
        CleanPollutedDatabase();
        CheckFirstRunAndScan();
    }

    private bool IsKnownBadOldEntry(string filePath)
    {
        string fileName = Path.GetFileNameWithoutExtension(filePath).ToLowerInvariant();
        if (fileName.StartsWith("ptt-") && fileName.Contains("-wa")) return true;
        if (fileName == "invalid_keypress" || fileName == "silence") return true;
        if (System.Text.RegularExpressions.Regex.IsMatch(fileName, @"test-\d{4,5}hz")) return true;
        if (System.Text.RegularExpressions.Regex.IsMatch(fileName, @"\d+hz-\d+ch")) return true;
        if (fileName.Contains("test-") || fileName.Contains("-test") || fileName.StartsWith("test_") || fileName.EndsWith("_test"))
        {
            if (fileName.Contains("hz") || fileName.Contains("bit") || fileName.Contains("ch-") || fileName.Contains("float") || fileName.Contains("le") || fileName.Contains("bytes") || fileName.Contains("s-"))
                return true;
        }
        if (fileName.Contains("24bit") || fileName.Contains("32bit") || fileName.Contains("16bit"))
        {
            if (fileName.Contains("float") || fileName.Contains("le") || fileName.Contains("be") || fileName.Contains("test"))
                return true;
        }
        return false;
    }

    private void CleanPollutedDatabase()
    {
        var songs = _databaseService.GetSongs();
        bool changed = false;

        foreach (var song in songs)
        {
            if (song.IsFavorite) continue; // Do not delete favorites

            if (song.IsAutoDiscovered)
            {
                // New logic: delete if it fails relevance
                if (IsKnownBadOldEntry(song.FilePath))
                {
                    _databaseService.DeleteSong(song.Id);
                    changed = true;
                }
            }
            else
            {
                // Old logic: only delete explicit proven bad legacy entries
                if (IsKnownBadOldEntry(song.FilePath))
                {
                    _databaseService.DeleteSong(song.Id);
                    changed = true;
                }
            }
        }

        if (changed)
        {
            LoadSongs();
        }
    }

    private async void CheckFirstRunAndScan()
    {
        var settings = _settingsService.LoadSettings();

        if (settings.ScanChoice == ScanComputerChoice.NotSet)
        {
            var result = ShowFirstRunPrompt();
            settings.ScanChoice = result;
            _settingsService.SaveSettings(settings);
        }

        if (settings.ScanChoice == ScanComputerChoice.Scan)
        {
            await RunBackgroundScanAsync();
        }
    }

    private ScanComputerChoice ShowFirstRunPrompt()
    {
        using var prompt = new Form()
        {
            Width = 400,
            Height = 200,
            FormBorderStyle = FormBorderStyle.FixedDialog,
            Text = "Find music automatically?",
            StartPosition = FormStartPosition.CenterParent,
            MaximizeBox = false,
            MinimizeBox = false,
            BackColor = Color.FromArgb(40, 40, 40),
            ForeColor = Color.White
        };

        var lblMsg = new Label()
        {
            Left = 20,
            Top = 20,
            Width = 340,
            Height = 60,
            Text = "Music Player can scan your computer for supported audio files. This may take some time on large drives.",
            Font = new Font("Segoe UI", 10)
        };

        var btnScan = new Button()
        {
            Text = "Scan Computer",
            Left = 50,
            Width = 120,
            Top = 100,
            DialogResult = DialogResult.Yes,
            BackColor = Color.SteelBlue,
            FlatStyle = FlatStyle.Flat,
            ForeColor = Color.White
        };
        btnScan.FlatAppearance.BorderSize = 0;

        var btnNotNow = new Button()
        {
            Text = "Not Now",
            Left = 200,
            Width = 120,
            Top = 100,
            DialogResult = DialogResult.No,
            BackColor = Color.FromArgb(60, 60, 60),
            FlatStyle = FlatStyle.Flat,
            ForeColor = Color.White
        };
        btnNotNow.FlatAppearance.BorderSize = 0;

        prompt.Controls.Add(lblMsg);
        prompt.Controls.Add(btnScan);
        prompt.Controls.Add(btnNotNow);
        prompt.AcceptButton = btnScan;
        prompt.CancelButton = btnNotNow;

        return prompt.ShowDialog(this) == DialogResult.Yes
            ? ScanComputerChoice.Scan
            : ScanComputerChoice.NotNow;
    }

    private async Task RunBackgroundScanAsync()
    {
        lblSongCount.Text = "Scanning...";
        int foundCount = 0;

        await Task.Run(() =>
        {
            var drives = DriveInfo.GetDrives().Where(d => d.DriveType == DriveType.Fixed);
            var excludedFolders = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                Environment.GetFolderPath(Environment.SpecialFolder.Windows),
                Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
                Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86),
                Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
            };

            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            foreach (var drive in drives)
            {
                if (!drive.IsReady) continue;

                string root = drive.RootDirectory.FullName;
                excludedFolders.Add(Path.Combine(root, "$Recycle.Bin"));
                excludedFolders.Add(Path.Combine(root, "System Volume Information"));

                ScanDirectoryRecursive(root, excludedFolders, ref foundCount, stopwatch);
            }
        });

        LoadSongs();
    }

    private static readonly HashSet<string> SmartDirectoryExclusions = new(StringComparer.OrdinalIgnoreCase)
    {
        "node_modules", "bin", "obj", "packages", "test", "tests", "testing", "sample", "samples", "fixtures", "benchmark", "benchmarks", ".venv", "__pycache__"
    };

    private void ScanDirectoryRecursive(string path, HashSet<string> excludedFolders, ref int foundCount, System.Diagnostics.Stopwatch stopwatch)
    {
        try
        {
            if (excludedFolders.Contains(path)) return;

            string dirName = Path.GetFileName(path);
            if (dirName.StartsWith(".")) return;
            if (SmartDirectoryExclusions.Contains(dirName)) return;

            var files = Directory.GetFiles(path);
            var supportedExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { ".mp3", ".wav", ".wma", ".aac", ".m4a" };

            foreach (var file in files)
            {
                if (supportedExtensions.Contains(Path.GetExtension(file)))
                {
                    if (!_databaseService.SongExists(file))
                    {
                        try
                        {
                            if (!IsKnownBadOldEntry(file))
                            {
                                var song = new Song
                                {
                                    FilePath = file,
                                    Title = Path.GetFileNameWithoutExtension(file),
                                    Artist = "Unknown Artist",
                                    Album = "Unknown Album",
                                    DateAdded = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                    IsAutoDiscovered = true
                                };

                                _databaseService.AddSong(song);
                                foundCount++;

                                if (stopwatch.ElapsedMilliseconds > 500)
                                {
                                    int currentCount = foundCount;
                                    this.Invoke((Action)(() => lblSongCount.Text = $"Scanning... Music files found: {currentCount}"));
                                    stopwatch.Restart();
                                }
                            }
                        }
                        catch
                        {
                            // Ignore bad files
                        }
                    }
                }
            }

            var directories = Directory.GetDirectories(path);
            foreach (var directory in directories)
            {
                ScanDirectoryRecursive(directory, excludedFolders, ref foundCount, stopwatch);
            }
        }
        catch (UnauthorizedAccessException)
        {
            // Skip folders we don't have access to
        }
        catch (Exception ex)
        {
            // Ignore other file system errors to let scan continue
            System.Diagnostics.Debug.WriteLine($"Error scanning {path}: {ex.Message}");
        }
    }
}
