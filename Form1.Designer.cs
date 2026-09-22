namespace MusicPlayer;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        this.lstSongs = new MusicPlayer.Controls.ModernListBox();
        this.lblSongCount = new System.Windows.Forms.Label();
        this.lblStatus = new System.Windows.Forms.Label();
        this.btnAddMusic = new MusicPlayer.Controls.ModernButton();
        this.btnDeleteMusic = new MusicPlayer.Controls.ModernButton();
        this.btnFavorite = new MusicPlayer.Controls.ModernButton();
        this.lblSearch = new System.Windows.Forms.Label();
        this.txtSearch = new System.Windows.Forms.TextBox();
        this.btnClearSearch = new MusicPlayer.Controls.ModernButton();
        this.cmbFilter = new System.Windows.Forms.ComboBox();
        this.lblSongTitle = new System.Windows.Forms.Label();
        this.lblArtist = new System.Windows.Forms.Label();
        this.tbProgress = new System.Windows.Forms.TrackBar();
        this.lblCurrentTime = new System.Windows.Forms.Label();
        this.lblTotalTime = new System.Windows.Forms.Label();
        this.btnRepeat = new MusicPlayer.Controls.ModernButton();
        this.btnShuffle = new MusicPlayer.Controls.ModernButton();
        this.btnPlayPause = new MusicPlayer.Controls.ModernButton();
        this.btnPrevious = new MusicPlayer.Controls.ModernButton();
        this.btnNext = new MusicPlayer.Controls.ModernButton();
        this.btnStop = new MusicPlayer.Controls.ModernButton();
        this.btnFastForward = new MusicPlayer.Controls.ModernButton();
        this.tbVolume = new System.Windows.Forms.TrackBar();
        this.lblVolume = new System.Windows.Forms.Label();
        this.lblVolumeIcon = new System.Windows.Forms.Label();
        this.pbArtwork = new System.Windows.Forms.PictureBox();
        this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
        
        this.pnlHeader = new System.Windows.Forms.Panel();
        this.lblHeader = new System.Windows.Forms.Label();
        this.pnlSidebar = new System.Windows.Forms.Panel();
        this.pnlMain = new System.Windows.Forms.Panel();
        this.pnlBottomArea = new System.Windows.Forms.Panel();
        this.pnlProgress = new System.Windows.Forms.Panel();
        this.pnlPlayback = new System.Windows.Forms.Panel();
        this.tlpPlaybackCenter = new System.Windows.Forms.TableLayoutPanel();
        this.tlpVolumeRow = new System.Windows.Forms.TableLayoutPanel();
        this.flpVolume = new System.Windows.Forms.FlowLayoutPanel();
        this.pnlFooter = new System.Windows.Forms.Panel();
        this.pnlFooterTopBorder = new System.Windows.Forms.Panel();

        ((System.ComponentModel.ISupportInitialize)(this.tbProgress)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.tbVolume)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.pbArtwork)).BeginInit();
        this.pnlHeader.SuspendLayout();
        this.pnlSidebar.SuspendLayout();
        this.pnlMain.SuspendLayout();
        this.pnlBottomArea.SuspendLayout();
        this.pnlProgress.SuspendLayout();
        this.pnlPlayback.SuspendLayout();
        this.tlpPlaybackCenter.SuspendLayout();
        this.tlpVolumeRow.SuspendLayout();
        this.flpVolume.SuspendLayout();
        this.pnlFooter.SuspendLayout();
        this.SuspendLayout();
        
        // pnlHeader
        this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
        this.pnlHeader.Controls.Add(this.lblHeader);
        this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
        this.pnlHeader.Location = new System.Drawing.Point(0, 0);
        this.pnlHeader.Name = "pnlHeader";
        this.pnlHeader.Size = new System.Drawing.Size(1050, 50);
        this.pnlHeader.TabIndex = 0;
        
        // lblHeader
        this.lblHeader.AutoSize = true;
        this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        this.lblHeader.ForeColor = System.Drawing.Color.White;
        this.lblHeader.Location = new System.Drawing.Point(20, 10);
        this.lblHeader.Name = "lblHeader";
        this.lblHeader.Size = new System.Drawing.Size(175, 30);
        this.lblHeader.TabIndex = 0;
        this.lblHeader.Text = "🎵 Music Player";
        
        // pnlFooter
        this.pnlFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
        this.pnlFooter.Controls.Add(this.lblSongCount);
        this.pnlFooter.Controls.Add(this.lblStatus);
        this.pnlFooter.Controls.Add(this.pnlFooterTopBorder);
        this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
        this.pnlFooter.Location = new System.Drawing.Point(0, 680);
        this.pnlFooter.Name = "pnlFooter";
        this.pnlFooter.Size = new System.Drawing.Size(1050, 40);
        this.pnlFooter.TabIndex = 1;

        // pnlFooterTopBorder
        this.pnlFooterTopBorder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
        this.pnlFooterTopBorder.Dock = System.Windows.Forms.DockStyle.Top;
        this.pnlFooterTopBorder.Height = 1;

        // lblStatus
        this.lblStatus.Dock = System.Windows.Forms.DockStyle.Left;
        this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.lblStatus.ForeColor = System.Drawing.Color.DarkGray;
        this.lblStatus.Location = new System.Drawing.Point(0, 1);
        this.lblStatus.Name = "lblStatus";
        this.lblStatus.Size = new System.Drawing.Size(200, 39);
        this.lblStatus.TabIndex = 1;
        this.lblStatus.Text = "Ready to play";
        this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.lblStatus.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
        
        // lblSongCount
        this.lblSongCount.Dock = System.Windows.Forms.DockStyle.Right;
        this.lblSongCount.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.lblSongCount.ForeColor = System.Drawing.Color.DarkGray;
        this.lblSongCount.Location = new System.Drawing.Point(850, 1);
        this.lblSongCount.Name = "lblSongCount";
        this.lblSongCount.Size = new System.Drawing.Size(200, 39);
        this.lblSongCount.TabIndex = 0;
        this.lblSongCount.Text = "0 Songs";
        this.lblSongCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        this.lblSongCount.Padding = new System.Windows.Forms.Padding(0, 0, 20, 0);
        
        // pnlSidebar
        this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
        this.pnlSidebar.Controls.Add(this.lstSongs);
        this.pnlSidebar.Controls.Add(this.cmbFilter);
        this.pnlSidebar.Controls.Add(this.btnClearSearch);
        this.pnlSidebar.Controls.Add(this.txtSearch);
        this.pnlSidebar.Controls.Add(this.lblSearch);
        this.pnlSidebar.Controls.Add(this.btnFavorite);
        this.pnlSidebar.Controls.Add(this.btnDeleteMusic);
        this.pnlSidebar.Controls.Add(this.btnAddMusic);
        this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
        this.pnlSidebar.Location = new System.Drawing.Point(0, 50);
        this.pnlSidebar.Name = "pnlSidebar";
        this.pnlSidebar.Size = new System.Drawing.Size(320, 630);
        this.pnlSidebar.TabIndex = 2;
        
        // btnAddMusic
        this.btnAddMusic.BorderRadius = 6;
        this.btnAddMusic.Location = new System.Drawing.Point(20, 20);
        this.btnAddMusic.Name = "btnAddMusic";
        this.btnAddMusic.Size = new System.Drawing.Size(90, 44);
        this.btnAddMusic.TabIndex = 0;
        this.btnAddMusic.Text = "＋ Add";
        this.btnAddMusic.Click += new System.EventHandler(this.BtnAddMusic_Click);
        
        // btnDeleteMusic
        this.btnDeleteMusic.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
        this.btnDeleteMusic.BorderRadius = 6;
        this.btnDeleteMusic.Location = new System.Drawing.Point(120, 20);
        this.btnDeleteMusic.Name = "btnDeleteMusic";
        this.btnDeleteMusic.Size = new System.Drawing.Size(90, 44);
        this.btnDeleteMusic.TabIndex = 1;
        this.btnDeleteMusic.Text = "🗑 Delete";
        this.btnDeleteMusic.Click += new System.EventHandler(this.BtnDeleteMusic_Click);
        
        // btnFavorite
        this.btnFavorite.BorderRadius = 6;
        this.btnFavorite.Location = new System.Drawing.Point(220, 20);
        this.btnFavorite.Name = "btnFavorite";
        this.btnFavorite.Size = new System.Drawing.Size(80, 44);
        this.btnFavorite.TabIndex = 2;
        this.btnFavorite.Text = "★ Fav";
        this.toolTip1.SetToolTip(this.btnFavorite, "Favorite — Add or remove this song from favorites");
        this.btnFavorite.Click += new System.EventHandler(this.BtnFavorite_Click);
        
        // lblSearch
        this.lblSearch.AutoSize = true;
        this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.lblSearch.ForeColor = System.Drawing.Color.DarkGray;
        this.lblSearch.Location = new System.Drawing.Point(16, 85);
        this.lblSearch.Name = "lblSearch";
        this.lblSearch.Size = new System.Drawing.Size(52, 19);
        this.lblSearch.TabIndex = 3;
        this.lblSearch.Text = "Search:";
        
        // txtSearch
        this.txtSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
        this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.txtSearch.ForeColor = System.Drawing.Color.White;
        this.txtSearch.Location = new System.Drawing.Point(20, 110);
        this.txtSearch.Name = "txtSearch";
        this.txtSearch.PlaceholderText = "Search songs...";
        this.txtSearch.Size = new System.Drawing.Size(235, 29);
        this.txtSearch.TabIndex = 4;
        this.txtSearch.TextChanged += new System.EventHandler(this.TxtSearch_TextChanged);
        
        // btnClearSearch
        this.btnClearSearch.BorderRadius = 4;
        this.btnClearSearch.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        this.btnClearSearch.Location = new System.Drawing.Point(265, 110);
        this.btnClearSearch.Name = "btnClearSearch";
        this.btnClearSearch.Size = new System.Drawing.Size(35, 30);
        this.btnClearSearch.TabIndex = 5;
        this.btnClearSearch.Text = "✕";
        this.toolTip1.SetToolTip(this.btnClearSearch, "Clear Search — Remove the current search");
        this.btnClearSearch.Click += new System.EventHandler(this.BtnClearSearch_Click);
        
        // cmbFilter
        this.cmbFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
        this.cmbFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cmbFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.cmbFilter.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.cmbFilter.ForeColor = System.Drawing.Color.White;
        this.cmbFilter.FormattingEnabled = true;
        this.cmbFilter.Location = new System.Drawing.Point(20, 150);
        this.cmbFilter.Name = "cmbFilter";
        this.cmbFilter.Size = new System.Drawing.Size(280, 28);
        this.cmbFilter.TabIndex = 6;
        this.cmbFilter.SelectedIndexChanged += new System.EventHandler(this.CmbFilter_SelectedIndexChanged);
        
        // lstSongs
        this.lstSongs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
        | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right)));
        this.lstSongs.AccentColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(131)))), ((int)(((byte)(255))))); // #1683FF
        this.lstSongs.Location = new System.Drawing.Point(20, 195);
        this.lstSongs.Name = "lstSongs";
        this.lstSongs.Size = new System.Drawing.Size(280, 410);
        this.lstSongs.TabIndex = 7;
        this.lstSongs.SelectedIndexChanged += new System.EventHandler(this.LstSongs_SelectedIndexChanged);
        
        // pnlMain
        this.pnlMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
        this.pnlMain.Controls.Add(this.pbArtwork);
        this.pnlMain.Controls.Add(this.lblSongTitle);
        this.pnlMain.Controls.Add(this.lblArtist);
        this.pnlMain.Controls.Add(this.pnlBottomArea);
        this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
        this.pnlMain.Location = new System.Drawing.Point(320, 50);
        this.pnlMain.Name = "pnlMain";
        this.pnlMain.Size = new System.Drawing.Size(730, 630);
        this.pnlMain.TabIndex = 3;
        
        // pnlBottomArea
        this.pnlBottomArea.Dock = System.Windows.Forms.DockStyle.Bottom;
        this.pnlBottomArea.Height = 185;
        this.pnlBottomArea.Controls.Add(this.pnlProgress);
        this.pnlBottomArea.Controls.Add(this.tlpVolumeRow);
        this.pnlBottomArea.Controls.Add(this.pnlPlayback);

        // pnlProgress
        this.pnlProgress.Dock = System.Windows.Forms.DockStyle.Top;
        this.pnlProgress.Height = 50;
        this.pnlProgress.Controls.Add(this.lblCurrentTime);
        this.pnlProgress.Controls.Add(this.tbProgress);
        this.pnlProgress.Controls.Add(this.lblTotalTime);

        // tlpVolumeRow
        this.tlpVolumeRow.Dock = System.Windows.Forms.DockStyle.Bottom;
        this.tlpVolumeRow.Height = 55;
        this.tlpVolumeRow.ColumnCount = 3;
        this.tlpVolumeRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
        this.tlpVolumeRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.AutoSize));
        this.tlpVolumeRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
        this.tlpVolumeRow.RowCount = 1;
        this.tlpVolumeRow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
        this.tlpVolumeRow.Controls.Add(this.flpVolume, 1, 0);

        // flpVolume
        this.flpVolume.AutoSize = true;
        this.flpVolume.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        this.flpVolume.WrapContents = false;
        this.flpVolume.Anchor = System.Windows.Forms.AnchorStyles.None;
        this.flpVolume.Margin = new System.Windows.Forms.Padding(0);
        this.flpVolume.Controls.Add(this.lblVolumeIcon);
        this.flpVolume.Controls.Add(this.tbVolume);
        this.flpVolume.Controls.Add(this.lblVolume);

        // pnlPlayback
        this.pnlPlayback.Dock = System.Windows.Forms.DockStyle.Fill;
        this.pnlPlayback.Controls.Add(this.tlpPlaybackCenter);
        this.pnlPlayback.Controls.Add(this.btnFastForward);

        // tlpPlaybackCenter
        this.tlpPlaybackCenter.ColumnCount = 8;
        this.tlpPlaybackCenter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
        this.tlpPlaybackCenter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.AutoSize));
        this.tlpPlaybackCenter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.AutoSize));
        this.tlpPlaybackCenter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.AutoSize));
        this.tlpPlaybackCenter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.AutoSize));
        this.tlpPlaybackCenter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.AutoSize));
        this.tlpPlaybackCenter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.AutoSize));
        this.tlpPlaybackCenter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
        this.tlpPlaybackCenter.RowCount = 1;
        this.tlpPlaybackCenter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
        this.tlpPlaybackCenter.Dock = System.Windows.Forms.DockStyle.Fill;
        this.tlpPlaybackCenter.Controls.Add(this.btnShuffle, 1, 0);
        this.tlpPlaybackCenter.Controls.Add(this.btnPrevious, 2, 0);
        this.tlpPlaybackCenter.Controls.Add(this.btnPlayPause, 3, 0);
        this.tlpPlaybackCenter.Controls.Add(this.btnNext, 4, 0);
        this.tlpPlaybackCenter.Controls.Add(this.btnRepeat, 5, 0);
        this.tlpPlaybackCenter.Controls.Add(this.btnStop, 6, 0);

        // pbArtwork
        this.pbArtwork.Anchor = System.Windows.Forms.AnchorStyles.Top;
        this.pbArtwork.Location = new System.Drawing.Point(190, 40);
        this.pbArtwork.Name = "pbArtwork";
        this.pbArtwork.Size = new System.Drawing.Size(350, 350);
        this.pbArtwork.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
        this.pbArtwork.TabIndex = 0;
        this.pbArtwork.TabStop = false;
        this.toolTip1.SetToolTip(this.pbArtwork, "Album Artwork");
        
        // lblSongTitle
        this.lblSongTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
        this.lblSongTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        this.lblSongTitle.ForeColor = System.Drawing.Color.White;
        this.lblSongTitle.Location = new System.Drawing.Point(40, 410);
        this.lblSongTitle.Name = "lblSongTitle";
        this.lblSongTitle.Size = new System.Drawing.Size(650, 45);
        this.lblSongTitle.TabIndex = 1;
        this.lblSongTitle.Text = "Unknown Title";
        this.lblSongTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        this.lblSongTitle.AutoEllipsis = true;
        
        // lblArtist
        this.lblArtist.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
        this.lblArtist.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.lblArtist.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
        this.lblArtist.Location = new System.Drawing.Point(40, 455);
        this.lblArtist.Name = "lblArtist";
        this.lblArtist.Size = new System.Drawing.Size(650, 30);
        this.lblArtist.TabIndex = 2;
        this.lblArtist.Text = "Unknown Artist • Unknown Album";
        this.lblArtist.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        this.lblArtist.AutoEllipsis = true;
        
        // lblCurrentTime
        this.lblCurrentTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
        this.lblCurrentTime.Location = new System.Drawing.Point(30, 20);
        this.lblCurrentTime.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.lblCurrentTime.ForeColor = System.Drawing.Color.White;
        this.lblCurrentTime.Name = "lblCurrentTime";
        this.lblCurrentTime.Size = new System.Drawing.Size(50, 19);
        this.lblCurrentTime.TabIndex = 3;
        this.lblCurrentTime.Text = "0:00";
        this.lblCurrentTime.TextAlign = System.Drawing.ContentAlignment.TopLeft;
        
        // tbProgress
        this.tbProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
        this.tbProgress.Location = new System.Drawing.Point(80, 20);
        this.tbProgress.Name = "tbProgress";
        this.tbProgress.Size = new System.Drawing.Size(570, 45);
        this.tbProgress.TabIndex = 4;
        this.tbProgress.TickStyle = System.Windows.Forms.TickStyle.None;
        this.tbProgress.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TbProgress_MouseDown);
        this.tbProgress.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TbProgress_MouseUp);
        
        // lblTotalTime
        this.lblTotalTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.lblTotalTime.Location = new System.Drawing.Point(650, 20);
        this.lblTotalTime.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.lblTotalTime.ForeColor = System.Drawing.Color.White;
        this.lblTotalTime.Name = "lblTotalTime";
        this.lblTotalTime.Size = new System.Drawing.Size(50, 19);
        this.lblTotalTime.TabIndex = 5;
        this.lblTotalTime.Text = "0:00";
        this.lblTotalTime.TextAlign = System.Drawing.ContentAlignment.TopRight;
        
        // btnShuffle
        this.btnShuffle.Anchor = System.Windows.Forms.AnchorStyles.None;
        this.btnShuffle.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
        this.btnShuffle.BorderRadius = 24;
        this.btnShuffle.Name = "btnShuffle";
        this.btnShuffle.Size = new System.Drawing.Size(48, 48);
        this.btnShuffle.TabIndex = 0;
        this.btnShuffle.Text = "🔀";
        this.toolTip1.SetToolTip(this.btnShuffle, "Shuffle — Play songs in random order");
        this.btnShuffle.Click += new System.EventHandler(this.BtnShuffle_Click);
        
        // btnPrevious
        this.btnPrevious.Anchor = System.Windows.Forms.AnchorStyles.None;
        this.btnPrevious.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
        this.btnPrevious.BorderRadius = 28;
        this.btnPrevious.Name = "btnPrevious";
        this.btnPrevious.Size = new System.Drawing.Size(56, 56);
        this.btnPrevious.TabIndex = 1;
        this.btnPrevious.Text = "◀◀";
        this.toolTip1.SetToolTip(this.btnPrevious, "Previous Song — Play the previous song");
        this.btnPrevious.Click += new System.EventHandler(this.BtnPrevious_Click);
        
        // btnPlayPause
        this.btnPlayPause.Anchor = System.Windows.Forms.AnchorStyles.None;
        this.btnPlayPause.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
        this.btnPlayPause.BorderRadius = 36;
        this.btnPlayPause.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.btnPlayPause.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(131)))), ((int)(((byte)(255))))); // #1683FF
        this.btnPlayPause.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(145)))), ((int)(((byte)(255)))));
        this.btnPlayPause.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(100)))), ((int)(((byte)(210)))));
        this.btnPlayPause.Name = "btnPlayPause";
        this.btnPlayPause.Size = new System.Drawing.Size(72, 72);
        this.btnPlayPause.TabIndex = 2;
        this.btnPlayPause.Text = "▶";
        this.toolTip1.SetToolTip(this.btnPlayPause, "Play — Start playback");
        this.btnPlayPause.Click += new System.EventHandler(this.BtnPlayPause_Click);
        
        // btnNext
        this.btnNext.Anchor = System.Windows.Forms.AnchorStyles.None;
        this.btnNext.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
        this.btnNext.BorderRadius = 28;
        this.btnNext.Name = "btnNext";
        this.btnNext.Size = new System.Drawing.Size(56, 56);
        this.btnNext.TabIndex = 3;
        this.btnNext.Text = "▶▶";
        this.toolTip1.SetToolTip(this.btnNext, "Next Song — Play the next song");
        this.btnNext.Click += new System.EventHandler(this.BtnNext_Click);
        
        // btnRepeat
        this.btnRepeat.Anchor = System.Windows.Forms.AnchorStyles.None;
        this.btnRepeat.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
        this.btnRepeat.BorderRadius = 24;
        this.btnRepeat.Name = "btnRepeat";
        this.btnRepeat.Size = new System.Drawing.Size(48, 48);
        this.btnRepeat.TabIndex = 4;
        this.btnRepeat.Text = "🔁";
        this.toolTip1.SetToolTip(this.btnRepeat, "Repeat — Change repeat mode");
        this.btnRepeat.Click += new System.EventHandler(this.BtnRepeat_Click);
        
        // btnStop
        this.btnStop.Anchor = System.Windows.Forms.AnchorStyles.None;
        this.btnStop.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
        this.btnStop.BorderRadius = 15;
        this.btnStop.Name = "btnStop";
        this.btnStop.Size = new System.Drawing.Size(30, 30);
        this.btnStop.TabIndex = 5;
        this.btnStop.Text = "⏹";
        this.toolTip1.SetToolTip(this.btnStop, "Stop — Stop playback");
        this.btnStop.Click += new System.EventHandler(this.BtnStop_Click);

        // btnFastForward
        this.btnFastForward.Visible = false;

        // lblVolumeIcon
        this.lblVolumeIcon.Anchor = System.Windows.Forms.AnchorStyles.None;
        this.lblVolumeIcon.Margin = new System.Windows.Forms.Padding(10, 10, 10, 0);
        this.lblVolumeIcon.AutoSize = true;
        this.lblVolumeIcon.Font = new System.Drawing.Font("Segoe MDL2 Assets", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.lblVolumeIcon.ForeColor = System.Drawing.Color.DarkGray;
        this.lblVolumeIcon.TabIndex = 6;
        this.lblVolumeIcon.Text = "\uE767";

        // tbVolume
        this.tbVolume.Anchor = System.Windows.Forms.AnchorStyles.None;
        this.tbVolume.Margin = new System.Windows.Forms.Padding(0, 8, 0, 0);
        this.tbVolume.AutoSize = false;
        this.tbVolume.Size = new System.Drawing.Size(160, 30);
        this.tbVolume.Maximum = 100;
        this.tbVolume.Name = "tbVolume";
        this.tbVolume.TabIndex = 7;
        this.tbVolume.TickStyle = System.Windows.Forms.TickStyle.None;
        this.tbVolume.Value = 100;
        this.toolTip1.SetToolTip(this.tbVolume, "Volume — Drag to adjust playback volume");
        this.tbVolume.ValueChanged += new System.EventHandler(this.TbVolume_ValueChanged);

        // lblVolume
        this.lblVolume.Anchor = System.Windows.Forms.AnchorStyles.None;
        this.lblVolume.Margin = new System.Windows.Forms.Padding(10, 10, 10, 0);
        this.lblVolume.AutoSize = true;
        this.lblVolume.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.lblVolume.ForeColor = System.Drawing.Color.DarkGray;
        this.lblVolume.Name = "lblVolume";
        this.lblVolume.Size = new System.Drawing.Size(50, 19);
        this.lblVolume.TabIndex = 8;
        this.lblVolume.Text = "100%";
        
        // Form1
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
        this.ClientSize = new System.Drawing.Size(1050, 720);
        this.Controls.Add(this.pnlMain);
        this.Controls.Add(this.pnlSidebar);
        this.Controls.Add(this.pnlFooter);
        this.Controls.Add(this.pnlHeader);
        this.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.ForeColor = System.Drawing.Color.White;
        this.Name = "Form1";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Music Player";
        
        ((System.ComponentModel.ISupportInitialize)(this.tbProgress)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.tbVolume)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.pbArtwork)).EndInit();
        this.pnlHeader.ResumeLayout(false);
        this.pnlHeader.PerformLayout();
        this.pnlSidebar.ResumeLayout(false);
        this.pnlSidebar.PerformLayout();
        this.pnlMain.ResumeLayout(false);
        this.pnlBottomArea.ResumeLayout(false);
        this.pnlProgress.ResumeLayout(false);
        this.pnlProgress.PerformLayout();
        this.pnlPlayback.ResumeLayout(false);
        this.tlpPlaybackCenter.ResumeLayout(false);
        this.tlpVolumeRow.ResumeLayout(false);
        this.flpVolume.ResumeLayout(false);
        this.flpVolume.PerformLayout();
        this.pnlFooter.ResumeLayout(false);
        this.ResumeLayout(false);
    }

    private MusicPlayer.Controls.ModernListBox lstSongs;
    private System.Windows.Forms.Label lblSongCount;
    private System.Windows.Forms.Label lblStatus;
    private MusicPlayer.Controls.ModernButton btnAddMusic;
    private MusicPlayer.Controls.ModernButton btnDeleteMusic;
    private MusicPlayer.Controls.ModernButton btnFavorite;
    private System.Windows.Forms.ComboBox cmbFilter;
    private System.Windows.Forms.Label lblSearch;
    private System.Windows.Forms.TextBox txtSearch;
    private MusicPlayer.Controls.ModernButton btnClearSearch;
    private System.Windows.Forms.Label lblSongTitle;
    private System.Windows.Forms.Label lblArtist;
    private System.Windows.Forms.TrackBar tbProgress;
    private System.Windows.Forms.Label lblCurrentTime;
    private System.Windows.Forms.Label lblTotalTime;
    private MusicPlayer.Controls.ModernButton btnRepeat;
    private MusicPlayer.Controls.ModernButton btnShuffle;
    private MusicPlayer.Controls.ModernButton btnPrevious;
    private MusicPlayer.Controls.ModernButton btnPlayPause;
    private MusicPlayer.Controls.ModernButton btnNext;
    private MusicPlayer.Controls.ModernButton btnStop;
    private MusicPlayer.Controls.ModernButton btnFastForward;
    private System.Windows.Forms.TrackBar tbVolume;
    private System.Windows.Forms.Label lblVolume;
    private System.Windows.Forms.Label lblVolumeIcon;
    private System.Windows.Forms.PictureBox pbArtwork;
    private System.Windows.Forms.ToolTip toolTip1;
    
    // Layout Panels
    private System.Windows.Forms.Panel pnlHeader;
    private System.Windows.Forms.Label lblHeader;
    private System.Windows.Forms.Panel pnlSidebar;
    private System.Windows.Forms.Panel pnlMain;
    private System.Windows.Forms.Panel pnlBottomArea;
    private System.Windows.Forms.Panel pnlProgress;
    private System.Windows.Forms.Panel pnlPlayback;
    private System.Windows.Forms.TableLayoutPanel tlpPlaybackCenter;
    private System.Windows.Forms.TableLayoutPanel tlpVolumeRow;
    private System.Windows.Forms.FlowLayoutPanel flpVolume;
    private System.Windows.Forms.Panel pnlFooter;
    private System.Windows.Forms.Panel pnlFooterTopBorder;
}
