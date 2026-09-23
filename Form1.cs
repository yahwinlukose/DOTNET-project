// Import the System.Drawing namespace for graphics and image manipulation
using System.Drawing;
// Import the System.Drawing.Drawing2D namespace for advanced 2D graphics classes
using System.Drawing.Drawing2D;

// Define the namespace for the application
namespace MusicPlayer;

// Declare a partial class for the main form of the application, inheriting from Form
public partial class Form1 : Form
{
    // A private readonly field for the database service instance
    private readonly DatabaseService _databaseService;
    // A private readonly field for the audio player service instance
    private readonly AudioPlayerService _audioPlayerService;
    // A private readonly field for the audio metadata service instance
    private readonly AudioMetadataService _audioMetadataService;
    // A private readonly field for the timer used to update playback progress
    private readonly System.Windows.Forms.Timer _playbackTimer;
    // A private flag indicating if the user is currently dragging the progress bar
    private bool _isDraggingProgress;
    // A private field to store the ID of the currently loaded song, if any
    private int? _currentLoadedSongId;
    // A private list to store all songs fetched from the database
    private System.Collections.Generic.List<Song> _allSongs = new();
    // A private enumeration to represent the repeat mode state
    private enum RepeatMode { Off, All, One }
    // A private field to store the current repeat mode, defaulting to Off
    private RepeatMode _repeatMode = RepeatMode.Off;
    // A private flag indicating if shuffle mode is currently enabled
    private bool _isShuffleOn = false;
    // A private random number generator instance for shuffle functionality
    private Random _random = new Random();
    // A private field to store the default artwork image
    private System.Drawing.Image _defaultArtwork;

    // Define the constructor for the Form1 class
    public Form1()
    {
        // Initialize the components created by the Windows Forms designer
        InitializeComponent();
        // Instantiate the database service
        _databaseService = new DatabaseService();
        // Initialize the database schema and tables
        _databaseService.InitializeDatabase();
        
        // Instantiate the audio player service
        _audioPlayerService = new AudioPlayerService();
        // Subscribe to the PlaybackFinished event of the audio player service
        _audioPlayerService.PlaybackFinished += AudioPlayerService_PlaybackFinished;
        // Instantiate the audio metadata service
        _audioMetadataService = new AudioMetadataService();
        // Subscribe to the FormClosing event of the form to handle cleanup
        this.FormClosing += Form1_FormClosing;
        
        // Instantiate a new timer for updating playback UI
        _playbackTimer = new System.Windows.Forms.Timer();
        // Set the timer interval to 100 milliseconds
        _playbackTimer.Interval = 100;
        // Subscribe to the Tick event of the timer
        _playbackTimer.Tick += PlaybackTimer_Tick;
        // Start the playback timer
        _playbackTimer.Start();

        // Set the initial value of the volume trackbar to 100
        tbVolume.Value = 100;
        // Update the volume label text to reflect the initial value
        lblVolume.Text = "Volume: 100%";
        // Set the initial volume of the audio player service to maximum (1.0)
        _audioPlayerService.Volume = 1.0f;

        // Add the "All Songs" option to the filter combo box
        cmbFilter.Items.Add("All Songs");
        // Add the "Favorites" option to the filter combo box
        cmbFilter.Items.Add("Favorites");
        // Set the default selected index of the filter combo box to "All Songs"
        cmbFilter.SelectedIndex = 0;

        // Call the method to generate and set the default artwork
        InitializeDefaultArtwork();
        // Load the initial list of songs from the database
        LoadSongs();
    }

    // A private method to initialize the default placeholder artwork
    private void InitializeDefaultArtwork()
    {
        // Create a new Bitmap with dimensions 150x150 pixels
        var bmp = new Bitmap(150, 150);
        // Create a Graphics object from the bitmap for drawing
        using (var g = Graphics.FromImage(bmp))
        {
            // Enable anti-aliasing for smoother drawing
            g.SmoothingMode = SmoothingMode.AntiAlias;
            // Clear the bitmap with a dark gray background color
            g.Clear(Color.FromArgb(30, 30, 30));
            
            // Create a solid brush with a lighter gray color for the icon
            using var brush = new SolidBrush(Color.FromArgb(80, 80, 80));
            // Create a bold Segoe UI font for the musical note character
            using var font = new Font("Segoe UI", 48, FontStyle.Bold);
            // Create a string format object to center the text
            var stringFormat = new StringFormat
            {
                // Center the text horizontally
                Alignment = StringAlignment.Center,
                // Center the text vertically
                LineAlignment = StringAlignment.Center
            };
            // Draw a musical note character in the center of the bitmap
            g.DrawString("🎵", font, brush, new RectangleF(0, 0, 150, 150), stringFormat);
        }
        // Assign the generated bitmap to the default artwork field
        _defaultArtwork = bmp;
        // Set the picture box image to the default artwork
        pbArtwork.Image = _defaultArtwork;
    }

    // A private method to clear the currently displayed artwork
    private void ClearArtwork()
    {
        // Store the current image reference
        var oldImage = pbArtwork.Image;
        // Set the picture box image to the default artwork
        pbArtwork.Image = _defaultArtwork;
        // Check if there was an old image and it's not the default artwork
        if (oldImage != null && oldImage != _defaultArtwork)
        {
            // Dispose the old image to free memory
            oldImage.Dispose();
        }
    }

    // A private method to update the artwork based on the given file path
    private void UpdateArtwork(string filePath)
    {
        // Attempt to extract the artwork image from the audio file metadata
        var newImage = _audioMetadataService.GetArtwork(filePath);
        // Store the current image reference
        var oldImage = pbArtwork.Image;
        
        // Set the picture box image to the new image, or the default if null
        pbArtwork.Image = newImage ?? _defaultArtwork;
        
        // Check if there was an old image and it's not the default artwork
        if (oldImage != null && oldImage != _defaultArtwork)
        {
            // Dispose the old image to free memory
            oldImage.Dispose();
        }
    }

    // Event handler for when the volume trackbar value changes
    private void TbVolume_ValueChanged(object? sender, EventArgs e)
    {
        // Update the volume label text with the new percentage
        lblVolume.Text = $"Volume: {tbVolume.Value}%";
        // Set the volume of the audio player service, converting 0-100 to 0.0-1.0
        _audioPlayerService.Volume = tbVolume.Value / 100f;
    }

    // Event handler for the playback timer tick
    private void PlaybackTimer_Tick(object? sender, EventArgs e)
    {
        // Check if an audio file is loaded and the user is not dragging the progress bar
        if (_audioPlayerService.IsLoaded && !_isDraggingProgress)
        {
            // Get the current playback position
            var current = _audioPlayerService.CurrentPosition;
            // Get the total duration of the audio file
            var total = _audioPlayerService.TotalDuration;
            
            // Update the current time label formatting it as minutes and seconds
            lblCurrentTime.Text = current.ToString(@"mm\:ss");
            // Check if the total duration is greater than zero
            if (total.TotalSeconds > 0)
            {
                // Update the total time label formatting it as minutes and seconds
                lblTotalTime.Text = total.ToString(@"mm\:ss");
                // Set the maximum value of the progress bar to the total seconds
                tbProgress.Maximum = (int)total.TotalSeconds;
                // Calculate the current progress in seconds as an integer
                int currentSeconds = (int)current.TotalSeconds;
                // Ensure the current seconds are within the valid range of the progress bar
                if (currentSeconds >= 0 && currentSeconds <= tbProgress.Maximum)
                {
                    // Update the progress bar value
                    tbProgress.Value = currentSeconds;
                }

                // Check if playback is stopped and the current position has reached the end
                if (!_audioPlayerService.IsPlaying && current >= total && total > TimeSpan.Zero)
                {
                    // Reset the play/pause button text to play
                    btnPlayPause.Text = "▶";
                    // Ensure the current time label shows the total duration precisely
                    lblCurrentTime.Text = total.ToString(@"mm\:ss");
                }
            }
        }
    }

    // Event handler for when the user presses the mouse down on the progress bar
    private void TbProgress_MouseDown(object? sender, MouseEventArgs e)
    {
        // Set the dragging flag to true to prevent timer updates
        _isDraggingProgress = true;
    }

    // Event handler for when the user releases the mouse on the progress bar
    private void TbProgress_MouseUp(object? sender, MouseEventArgs e)
    {
        // Check if an audio file is currently loaded
        if (_audioPlayerService.IsLoaded)
        {
            // Seek the audio playback to the time corresponding to the progress bar value
            _audioPlayerService.Seek(TimeSpan.FromSeconds(tbProgress.Value));
        }
        // Set the dragging flag back to false
        _isDraggingProgress = false;
    }

    // Event handler for when the form is closing
    private void Form1_FormClosing(object? sender, FormClosingEventArgs e)
    {
        // Stop the playback timer
        _playbackTimer.Stop();
        // Dispose the playback timer to release resources
        _playbackTimer.Dispose();
        // Dispose the audio player service to release audio resources
        _audioPlayerService.Dispose();

        // Store the current artwork image reference
        var img = pbArtwork.Image;
        // Clear the picture box image reference
        pbArtwork.Image = null;
        // Dispose the artwork image if it's not the default
        if (img != null && img != _defaultArtwork) img.Dispose();
        // Dispose the default artwork image
        _defaultArtwork?.Dispose();
    }

    // A private method to load songs from the database and refresh the list
    private void LoadSongs()
    {
        // Retrieve all songs from the database
        _allSongs = _databaseService.GetSongs();
        // Apply the current search filter and update the UI list
        ApplySearchFilter();
    }

    // A private method to apply search and favorite filters to the song list
    private void ApplySearchFilter()
    {
        // Get the search text, trimmed and converted to lowercase
        var searchText = txtSearch.Text?.Trim().ToLowerInvariant() ?? string.Empty;
        // Determine if only favorite songs should be shown based on the combo box selection
        var showFavoritesOnly = cmbFilter.SelectedIndex == 1;
        
        // Filter the all songs list based on the criteria
        var filteredSongs = _allSongs.Where(s => 
            // Check if favorites filter is inactive, or if the song is a favorite
            (!showFavoritesOnly || s.IsFavorite) &&
            // Check if search text is empty, or if the title, artist, or album matches
            (string.IsNullOrEmpty(searchText) || 
             (s.Title != null && s.Title.ToLowerInvariant().Contains(searchText)) ||
             (s.Artist != null && s.Artist.ToLowerInvariant().Contains(searchText)) ||
             (s.Album != null && s.Album.ToLowerInvariant().Contains(searchText)))
        ).ToList();

        // Store the ID of the currently selected song in the list box, if any
        int? currentSelectionId = (lstSongs.SelectedItem as Song)?.Id;

        // Suspend list box layout updates for performance
        lstSongs.BeginUpdate();
        // Clear the current items in the list box
        lstSongs.Items.Clear();
        // Variable to hold the song that should be re-selected after updating
        Song? itemToSelect = null;
        // Iterate through each filtered song
        foreach (var song in filteredSongs)
        {
            // Add the song to the list box
            lstSongs.Items.Add(song);
            // Check if this song matches the previously selected ID
            if (currentSelectionId.HasValue && song.Id == currentSelectionId.Value)
            {
                // Store this song as the one to re-select
                itemToSelect = song;
            }
        }
        // Resume list box layout updates
        lstSongs.EndUpdate();
        
        // If a previously selected song was found in the new list
        if (itemToSelect != null)
        {
            // Reselect the item in the list box
            lstSongs.SelectedItem = itemToSelect;
        }
        
        // Update the label displaying the total number of visible songs
        lblSongCount.Text = $"{filteredSongs.Count} Songs";
    }

    // Event handler for when the text in the search box changes
    private void TxtSearch_TextChanged(object? sender, EventArgs e)
    {
        // Re-apply the search filter to update the list immediately
        ApplySearchFilter();
    }

    // Event handler for when the clear search button is clicked
    private void BtnClearSearch_Click(object? sender, EventArgs e)
    {
        // Clear the text in the search box
        txtSearch.Text = string.Empty;
    }

    // Event handler for when the selected item in the filter combo box changes
    private void CmbFilter_SelectedIndexChanged(object? sender, EventArgs e)
    {
        // Re-apply the filter to update the list immediately
        ApplySearchFilter();
    }

    // A private method to load a selected song into the audio player and update UI
    private bool LoadSelectedSong(Song selectedSong)
    {
        // If the song is already loaded, do nothing and return true
        if (_currentLoadedSongId == selectedSong.Id && _audioPlayerService.IsLoaded)
        {
            // Return true to indicate successful load
            return true;
        }

        // Start a try block to handle potential loading errors
        try
        {
            // Load the audio file path into the player service
            _audioPlayerService.Load(selectedSong.FilePath);
            // Store the ID of the newly loaded song
            _currentLoadedSongId = selectedSong.Id;

            // Update the title label, using a fallback if empty
            lblSongTitle.Text = string.IsNullOrWhiteSpace(selectedSong.Title) ? "Unknown Title" : selectedSong.Title;
            
            // Prepare the artist string with a fallback if empty
            var artistStr = string.IsNullOrWhiteSpace(selectedSong.Artist) ? "Unknown Artist" : selectedSong.Artist;
            // Prepare the album string with a fallback if empty
            var albumStr = string.IsNullOrWhiteSpace(selectedSong.Album) ? "Unknown Album" : selectedSong.Album;
            // Update the artist and album label text
            lblArtist.Text = $"{artistStr} • {albumStr}";
            
            // Update the total time label with the new duration
            lblTotalTime.Text = _audioPlayerService.TotalDuration.ToString(@"mm\:ss");
            // Reset the current time label
            lblCurrentTime.Text = "00:00";
            // Reset the progress bar value
            tbProgress.Value = 0;
            // Reset the play/pause button text to play
            btnPlayPause.Text = "▶";

            // Update the album artwork for the newly loaded song
            UpdateArtwork(selectedSong.FilePath);
            // Return true indicating successful load
            return true;
        }
        // Catch any exception that occurs during loading
        catch (Exception ex)
        {
            // Reset the loaded song ID since it failed
            _currentLoadedSongId = null;
            // Show an error message box with the exception details
            MessageBox.Show($"Error loading audio file:\n{ex.Message}", "Playback Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            // Return false indicating load failure
            return false;
        }
    }

    // Event handler for when the selected item in the song list changes
    private void LstSongs_SelectedIndexChanged(object? sender, EventArgs e)
    {
        // Check if the selected item is a valid Song object
        if (lstSongs.SelectedItem is Song selectedSong)
        {
            // Update the favorite button state for the selected song
            UpdateFavoriteButtonText(selectedSong);
            // Attempt to load the selected song
            LoadSelectedSong(selectedSong);
        }
    }

    // A private method to select, load, and play a song at a specific index
    private void LoadAndPlaySong(int index)
    {
        // Ensure the index is within the valid bounds of the list
        if (index < 0 || index >= lstSongs.Items.Count) return;
        // Ensure the item at the index is a Song object
        if (lstSongs.Items[index] is not Song song) return;

        // Setting SelectedIndex updates the UI list visually.
        // It may fire SelectedIndexChanged synchronously, but we don't rely on that side-effect.
        lstSongs.SelectedIndex = index;
        
        // Explicitly load the song to guarantee it is ready before playing.
        // If it's already loaded, this safely returns true without re-loading.
        if (LoadSelectedSong(song))
        {
            // Stop any current playback
            _audioPlayerService.Stop();
            // Reset current time label
            lblCurrentTime.Text = "00:00";
            // Reset progress bar value
            tbProgress.Value = 0;
            // Start playing the newly loaded song
            _audioPlayerService.Play();
            // Update the play/pause button text to pause
            btnPlayPause.Text = "⏸";
        }
    }

    // Event handler for the Previous button click
    private void BtnPrevious_Click(object? sender, EventArgs e)
    {
        // Ensure there are songs in the list and one is selected
        if (lstSongs.Items.Count == 0 || lstSongs.SelectedItem == null) return;

        // Get the index of the currently selected song
        int currentIndex = lstSongs.SelectedIndex;
        // If it's not the first song
        if (currentIndex > 0)
        {
            // Load and play the previous song in the list
            LoadAndPlaySong(currentIndex - 1);
        }
        else
        {
            // If it is the first song, restart the current song
            LoadAndPlaySong(currentIndex); // Restarts current song
        }
    }

    // Event handler for the Next button click
    private void BtnNext_Click(object? sender, EventArgs e)
    {
        // Ensure there are songs in the list and one is selected
        if (lstSongs.Items.Count == 0 || lstSongs.SelectedItem == null) return;

        // Get the index of the currently selected song
        int currentIndex = lstSongs.SelectedIndex;
        
        // Check if shuffle is enabled and there are multiple songs
        if (_isShuffleOn && lstSongs.Items.Count > 1)
        {
            // Variable to hold the next random index
            int nextIndex;
            // Generate a random index until it's different from the current one
            do
            {
                // Pick a random number between 0 and the number of items
                nextIndex = _random.Next(lstSongs.Items.Count);
            } while (nextIndex == currentIndex);
            
            // Load and play the randomly selected song
            LoadAndPlaySong(nextIndex);
        }
        else
        {
            // Calculate the index of the next song sequentially
            int nextIndex = currentIndex + 1;
            // If we reached the end of the list
            if (nextIndex >= lstSongs.Items.Count)
            {
                // Wrap around to the first song
                nextIndex = 0;
            }
            // Load and play the calculated next song
            LoadAndPlaySong(nextIndex);
        }
    }

    // Event handler for the Play/Pause button click
    private void BtnPlayPause_Click(object? sender, EventArgs e)
    {
        // Check if an audio file is currently loaded
        if (!_audioPlayerService.IsLoaded)
        {
            // Show a message asking the user to select a song
            MessageBox.Show("Please select a song to play.", "No Song Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // Exit the method
            return;
        }

        // Start a try block to catch playback errors
        try
        {
            // If the audio is currently playing
            if (_audioPlayerService.IsPlaying)
            {
                // Pause the playback
                _audioPlayerService.Pause();
                // Update the button text to play
                btnPlayPause.Text = "▶";
            }
            else
            {
                // Otherwise, resume or start playback
                _audioPlayerService.Play();
                // Update the button text to pause
                btnPlayPause.Text = "⏸";
            }
        }
        // Catch any exceptions during playback toggling
        catch (Exception ex)
        {
            // Show an error message box with exception details
            MessageBox.Show($"Error playing audio:\n{ex.Message}", "Playback Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    // Event handler for the Stop button click
    private void BtnStop_Click(object? sender, EventArgs e)
    {
        // Check if an audio file is currently loaded
        if (_audioPlayerService.IsLoaded)
        {
            // Stop the playback
            _audioPlayerService.Stop();
            // Reset the progress bar value
            tbProgress.Value = 0;
            // Reset the current time label
            lblCurrentTime.Text = "00:00";
            // Update the play/pause button text to play
            btnPlayPause.Text = "▶";
        }
    }

    // Event handler triggered when playback finishes naturally
    private void AudioPlayerService_PlaybackFinished(object? sender, EventArgs e)
    {
        // Ensure this method runs on the UI thread
        if (this.InvokeRequired)
        {
            // Invoke the method recursively on the UI thread
            this.Invoke(new Action(() => AudioPlayerService_PlaybackFinished(sender, e)));
            // Exit the current background thread execution
            return;
        }

        // If the list is empty or nothing is selected, just stop
        if (lstSongs.Items.Count == 0 || lstSongs.SelectedItem == null)
        {
            // Trigger the stop action
            BtnStop_Click(null, EventArgs.Empty);
            // Exit the method
            return;
        }

        // Get the current selected index
        int currentIndex = lstSongs.SelectedIndex;
        
        // Check if the repeat mode is set to repeat the same song
        if (_repeatMode == RepeatMode.One)
        {
            // Restart the playback of the current song
            _audioPlayerService.Play();
            // Update button text to pause
            btnPlayPause.Text = "⏸";
        }
        // Check if shuffle is enabled and there are multiple songs
        else if (_isShuffleOn && lstSongs.Items.Count > 1)
        {
            // Variable to hold the next random index
            int nextIndex;
            // Generate a random index until it's different from the current one
            do
            {
                // Pick a random number between 0 and the number of items
                nextIndex = _random.Next(lstSongs.Items.Count);
            } while (nextIndex == currentIndex);

            // Change the selected item to the random song (which auto-loads it via the selection event)
            lstSongs.SelectedIndex = nextIndex;
            // Ensure playback starts for the newly selected song
            _audioPlayerService.Play();
            // Update button text to pause
            btnPlayPause.Text = "⏸";
        }
        // If shuffle is off
        else if (!_isShuffleOn)
        {
            // If we are not at the end of the list
            if (currentIndex < lstSongs.Items.Count - 1)
            {
                // Move to the next sequential song (auto-loads via selection event)
                lstSongs.SelectedIndex = currentIndex + 1;
                // Start playing the newly selected song
                _audioPlayerService.Play();
                // Update button text to pause
                btnPlayPause.Text = "⏸";
            }
            // If we are at the end, but repeat all is enabled
            else if (_repeatMode == RepeatMode.All)
            {
                // Wrap around to the first song in the list (auto-loads)
                lstSongs.SelectedIndex = 0;
                // Start playing the first song
                _audioPlayerService.Play();
                // Update button text to pause
                btnPlayPause.Text = "⏸";
            }
            // If we are at the end and repeat is off
            else
            {
                // Stop playback completely
                BtnStop_Click(null, EventArgs.Empty);
            }
        }
        // Fallback for any other state
        else
        {
            // If repeat all is on, but shuffle was somewhat involved? (Edge case)
            if (_repeatMode == RepeatMode.All)
            {
                // Just restart play
                _audioPlayerService.Play();
                // Update button text
                btnPlayPause.Text = "⏸";
            }
            else
            {
                // Otherwise stop
                BtnStop_Click(null, EventArgs.Empty);
            }
        }
    }

    // Event handler for the Repeat button click
    private void BtnRepeat_Click(object? sender, EventArgs e)
    {
        // Cycle the repeat mode using a switch expression
        _repeatMode = _repeatMode switch
        {
            // If off, switch to all
            RepeatMode.Off => RepeatMode.All,
            // If all, switch to one
            RepeatMode.All => RepeatMode.One,
            // If one, switch to off
            RepeatMode.One => RepeatMode.Off,
            // Default fallback to off
            _ => RepeatMode.Off
        };

        // Update the button text to show the correct icon based on mode
        btnRepeat.Text = _repeatMode == RepeatMode.One ? "🔂" : "🔁";
        // Update the button background color to indicate if repeat is active
        btnRepeat.BackColor = _repeatMode == RepeatMode.Off 
            // Dark gray if off
            ? System.Drawing.Color.FromArgb(45, 45, 45) 
            // Steel blue if active
            : System.Drawing.Color.SteelBlue;
    }

    // Event handler for the Shuffle button click
    private void BtnShuffle_Click(object? sender, EventArgs e)
    {
        // Toggle the boolean shuffle state
        _isShuffleOn = !_isShuffleOn;
        // Update the button text to reflect the new state
        btnShuffle.Text = _isShuffleOn ? "🔀 Shuffle ON" : "🔀 Shuffle OFF";
    }

    // Event handler for the Fast Forward button click
    private void BtnFastForward_Click(object? sender, EventArgs e)
    {
        // Do nothing if no audio is loaded
        if (!_audioPlayerService.IsLoaded) return;
        // Fast forward the playback by 10 seconds
        _audioPlayerService.FastForward(TimeSpan.FromSeconds(10));
    }

    // Event handler for the Add Music button click
    private void BtnAddMusic_Click(object? sender, EventArgs e)
    {
        // Create an open file dialog, ensuring it gets disposed
        using var openFileDialog = new OpenFileDialog();
        // Allow the user to select multiple files at once
        openFileDialog.Multiselect = true;
        // Set the filter to show only common audio formats
        openFileDialog.Filter = "Audio Files|*.mp3;*.wav;*.wma;*.aac;*.m4a";
        // Set the dialog window title
        openFileDialog.Title = "Select Music Files";

        // Show the dialog and check if the user clicked OK
        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            // Iterate through all selected file paths
            foreach (var filePath in openFileDialog.FileNames)
            {
                // Start a try block for adding each individual file
                try
                {
                    // Check if the song is not already in the database
                    if (!_databaseService.SongExists(filePath))
                    {
                        // Extract metadata from the audio file
                        var song = _audioMetadataService.ExtractMetadata(filePath);
                        // Add the new song record to the database
                        _databaseService.AddSong(song);
                    }
                }
                // Catch any exception during file addition
                catch (Exception ex)
                {
                    // Ignore individual file errors and continue
                    // Log the error to the debug console
                    System.Diagnostics.Debug.WriteLine($"Error adding file {filePath}: {ex.Message}");
                }
            }
            
            // Reload the song list to display the newly added items
            LoadSongs();
        }
    }

    // Event handler for the Delete Music button click
    private void BtnDeleteMusic_Click(object? sender, EventArgs e)
    {
        // Ensure a song is selected in the list box
        if (lstSongs.SelectedItem is not Song selectedSong)
        {
            // Show a message if no song is selected
            MessageBox.Show("Please select a song to delete.", "No Song Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // Exit the method
            return;
        }

        // Ask the user to confirm the deletion action
        var result = MessageBox.Show($"Are you sure you want to remove '{selectedSong.Title}' from your library?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        // Check if the user confirmed by clicking Yes
        if (result == DialogResult.Yes)
        {
            // Start a try block for the deletion process
            try
            {
                // If the song being deleted is the one currently loaded/playing
                if (selectedSong.Id == _currentLoadedSongId)
                {
                    // Stop the playback immediately
                    _audioPlayerService.Stop();
                    // Reset the progress bar
                    tbProgress.Value = 0;
                    // Reset the current time label
                    lblCurrentTime.Text = "00:00";
                    // Update the play button text
                    btnPlayPause.Text = "▶";
                    // Clear the current loaded song ID reference
                    _currentLoadedSongId = null;
                }

                // Delete the song record from the database
                _databaseService.DeleteSong(selectedSong.Id);
                // Reload the song list to reflect the deletion
                LoadSongs();

                // Check if the list is now completely empty
                if (lstSongs.Items.Count == 0)
                {
                    // Reset the title label to unknown
                    lblSongTitle.Text = "Unknown Title";
                    // Reset the artist label to unknown
                    lblArtist.Text = "Unknown Artist • Unknown Album";
                    // Reset the total time label
                    lblTotalTime.Text = "00:00";
                    // Clear the artwork picture box
                    ClearArtwork();
                }
            }
            // Catch any exception during the deletion process
            catch (Exception ex)
            {
                // Show an error message box with exception details
                MessageBox.Show($"Error deleting song:\n{ex.Message}", "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    // A private method to update the text of the favorite button based on song state
    private void UpdateFavoriteButtonText(Song song)
    {
        // Set text to indicate if it is a favorite or an option to make it one
        btnFavorite.Text = song.IsFavorite ? "★ Favorited" : "⭐ Favorite";
    }

    // Event handler for the Favorite button click
    private void BtnFavorite_Click(object? sender, EventArgs e)
    {
        // Ensure a song is selected in the list box
        if (lstSongs.SelectedItem is not Song selectedSong)
        {
            // Show a message if no song is selected
            MessageBox.Show("Please select a song first.", "No Song Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // Exit the method
            return;
        }

        // Start a try block for the favorite status update
        try
        {
            // Toggle the current favorite status
            bool newFavoriteState = !selectedSong.IsFavorite;
            // Update the favorite status in the database
            _databaseService.SetFavorite(selectedSong.Id, newFavoriteState);
            // Reload the song list to reflect the updated status
            LoadSongs();
        }
        // Catch any exception during the database update
        catch (Exception ex)
        {
            // Show an error message box with exception details
            MessageBox.Show($"Error updating favorite:\n{ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
