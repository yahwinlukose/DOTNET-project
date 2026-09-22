namespace MusicPlayer;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.lstSongs = new System.Windows.Forms.ListBox();
        this.lblSongCount = new System.Windows.Forms.Label();
        this.btnAddMusic = new System.Windows.Forms.Button();
        this.btnDeleteMusic = new System.Windows.Forms.Button();
        this.btnFavorite = new System.Windows.Forms.Button();
        this.lblSearch = new System.Windows.Forms.Label();
        this.txtSearch = new System.Windows.Forms.TextBox();
        this.btnClearSearch = new System.Windows.Forms.Button();
        this.cmbFilter = new System.Windows.Forms.ComboBox();
        this.lblSongTitle = new System.Windows.Forms.Label();
        this.lblArtist = new System.Windows.Forms.Label();
        this.tbProgress = new System.Windows.Forms.TrackBar();
        this.lblCurrentTime = new System.Windows.Forms.Label();
        this.lblTotalTime = new System.Windows.Forms.Label();
        this.btnRepeat = new System.Windows.Forms.Button();
        this.btnShuffle = new System.Windows.Forms.Button();
        this.btnPlayPause = new System.Windows.Forms.Button();
        this.btnPrevious = new System.Windows.Forms.Button();
        this.btnNext = new System.Windows.Forms.Button();
        this.btnStop = new System.Windows.Forms.Button();
        this.btnFastForward = new System.Windows.Forms.Button();
        this.tbVolume = new System.Windows.Forms.TrackBar();
        this.lblVolume = new System.Windows.Forms.Label();
        this.pbArtwork = new System.Windows.Forms.PictureBox();
        ((System.ComponentModel.ISupportInitialize)(this.tbProgress)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.tbVolume)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.pbArtwork)).BeginInit();
        this.SuspendLayout();
        // 
        // lstSongs
        // 
        this.lstSongs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left)));
        this.lstSongs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
        this.lstSongs.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.lstSongs.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.lstSongs.ForeColor = System.Drawing.Color.White;
        this.lstSongs.FormattingEnabled = true;
        this.lstSongs.ItemHeight = 17;
        this.lstSongs.Location = new System.Drawing.Point(15, 135);
        this.lstSongs.Name = "lstSongs";
        this.lstSongs.Size = new System.Drawing.Size(250, 323);
        this.lstSongs.TabIndex = 0;
        this.lstSongs.SelectedIndexChanged += new System.EventHandler(this.LstSongs_SelectedIndexChanged);
        // 
        // btnAddMusic
        // 
        this.btnAddMusic.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
        this.btnAddMusic.FlatAppearance.BorderSize = 0;
        this.btnAddMusic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnAddMusic.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.btnAddMusic.ForeColor = System.Drawing.Color.White;
        this.btnAddMusic.Location = new System.Drawing.Point(15, 15);
        this.btnAddMusic.Name = "btnAddMusic";
        this.btnAddMusic.Size = new System.Drawing.Size(75, 30);
        this.btnAddMusic.TabIndex = 1;
        this.btnAddMusic.Text = "Add";
        this.btnAddMusic.UseVisualStyleBackColor = false;
        this.btnAddMusic.Click += new System.EventHandler(this.BtnAddMusic_Click);
        // 
        // btnDeleteMusic
        // 
        this.btnDeleteMusic.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
        this.btnDeleteMusic.FlatAppearance.BorderSize = 0;
        this.btnDeleteMusic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnDeleteMusic.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.btnDeleteMusic.ForeColor = System.Drawing.Color.White;
        this.btnDeleteMusic.Location = new System.Drawing.Point(95, 15);
        this.btnDeleteMusic.Name = "btnDeleteMusic";
        this.btnDeleteMusic.Size = new System.Drawing.Size(75, 30);
        this.btnDeleteMusic.TabIndex = 2;
        this.btnDeleteMusic.Text = "Delete";
        this.btnDeleteMusic.UseVisualStyleBackColor = false;
        this.btnDeleteMusic.Click += new System.EventHandler(this.BtnDeleteMusic_Click);
        // 
        // btnFavorite
        // 
        this.btnFavorite.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
        this.btnFavorite.FlatAppearance.BorderSize = 0;
        this.btnFavorite.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnFavorite.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.btnFavorite.ForeColor = System.Drawing.Color.White;
        this.btnFavorite.Location = new System.Drawing.Point(175, 15);
        this.btnFavorite.Name = "btnFavorite";
        this.btnFavorite.Size = new System.Drawing.Size(90, 30);
        this.btnFavorite.TabIndex = 20;
        this.btnFavorite.Text = "⭐ Favorite";
        this.btnFavorite.UseVisualStyleBackColor = false;
        this.btnFavorite.Click += new System.EventHandler(this.BtnFavorite_Click);
        // 
        // lblSearch
        // 
        this.lblSearch.AutoSize = true;
        this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.lblSearch.ForeColor = System.Drawing.Color.White;
        this.lblSearch.Location = new System.Drawing.Point(15, 60);
        this.lblSearch.Name = "lblSearch";
        this.lblSearch.Size = new System.Drawing.Size(50, 17);
        this.lblSearch.TabIndex = 3;
        this.lblSearch.Text = "Search:";
        // 
        // txtSearch
        // 
        this.txtSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
        this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.txtSearch.ForeColor = System.Drawing.Color.White;
        this.txtSearch.Location = new System.Drawing.Point(75, 57);
        this.txtSearch.Name = "txtSearch";
        this.txtSearch.PlaceholderText = "Search songs...";
        this.txtSearch.Size = new System.Drawing.Size(155, 25);
        this.txtSearch.TabIndex = 4;
        this.txtSearch.TextChanged += new System.EventHandler(this.TxtSearch_TextChanged);
        // 
        // btnClearSearch
        // 
        this.btnClearSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
        this.btnClearSearch.FlatAppearance.BorderSize = 0;
        this.btnClearSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnClearSearch.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.btnClearSearch.ForeColor = System.Drawing.Color.White;
        this.btnClearSearch.Location = new System.Drawing.Point(235, 56);
        this.btnClearSearch.Name = "btnClearSearch";
        this.btnClearSearch.Size = new System.Drawing.Size(30, 27);
        this.btnClearSearch.TabIndex = 5;
        this.btnClearSearch.Text = "✕";
        this.btnClearSearch.UseVisualStyleBackColor = false;
        this.btnClearSearch.Click += new System.EventHandler(this.BtnClearSearch_Click);
        // 
        // cmbFilter
        // 
        this.cmbFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
        this.cmbFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cmbFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.cmbFilter.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.cmbFilter.ForeColor = System.Drawing.Color.White;
        this.cmbFilter.FormattingEnabled = true;
        this.cmbFilter.Location = new System.Drawing.Point(15, 95);
        this.cmbFilter.Name = "cmbFilter";
        this.cmbFilter.Size = new System.Drawing.Size(250, 25);
        this.cmbFilter.TabIndex = 21;
        this.cmbFilter.SelectedIndexChanged += new System.EventHandler(this.CmbFilter_SelectedIndexChanged);
        // 
        // lblSongCount
        // 
        this.lblSongCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.lblSongCount.AutoSize = true;
        this.lblSongCount.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.lblSongCount.ForeColor = System.Drawing.Color.DarkGray;
        this.lblSongCount.Location = new System.Drawing.Point(15, 475);
        this.lblSongCount.Name = "lblSongCount";
        this.lblSongCount.Size = new System.Drawing.Size(56, 17);
        this.lblSongCount.TabIndex = 2;
        this.lblSongCount.Text = "0 Songs";
        // 
        // lblSongTitle
        // 
        this.lblSongTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
        this.lblSongTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        this.lblSongTitle.ForeColor = System.Drawing.Color.White;
        this.lblSongTitle.Location = new System.Drawing.Point(290, 100);
        this.lblSongTitle.Name = "lblSongTitle";
        this.lblSongTitle.Size = new System.Drawing.Size(500, 50);
        this.lblSongTitle.TabIndex = 3;
        this.lblSongTitle.Text = "Unknown Title";
        this.lblSongTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // lblArtist
        // 
        this.lblArtist.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
        this.lblArtist.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.lblArtist.ForeColor = System.Drawing.Color.DarkGray;
        this.lblArtist.Location = new System.Drawing.Point(290, 160);
        this.lblArtist.Name = "lblArtist";
        this.lblArtist.Size = new System.Drawing.Size(500, 30);
        this.lblArtist.TabIndex = 4;
        this.lblArtist.Text = "Unknown Artist • Unknown Album";
        this.lblArtist.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // tbProgress
        // 
        this.tbProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
        this.tbProgress.Location = new System.Drawing.Point(340, 365);
        this.tbProgress.Name = "tbProgress";
        this.tbProgress.Size = new System.Drawing.Size(400, 45);
        this.tbProgress.TabIndex = 5;
        this.tbProgress.TickStyle = System.Windows.Forms.TickStyle.None;
        this.tbProgress.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TbProgress_MouseDown);
        this.tbProgress.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TbProgress_MouseUp);
        // 
        // lblCurrentTime
        // 
        this.lblCurrentTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.lblCurrentTime.AutoSize = true;
        this.lblCurrentTime.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.lblCurrentTime.ForeColor = System.Drawing.Color.White;
        this.lblCurrentTime.Location = new System.Drawing.Point(290, 365);
        this.lblCurrentTime.Name = "lblCurrentTime";
        this.lblCurrentTime.Size = new System.Drawing.Size(32, 17);
        this.lblCurrentTime.TabIndex = 6;
        this.lblCurrentTime.Text = "0:00";
        // 
        // lblTotalTime
        // 
        this.lblTotalTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.lblTotalTime.AutoSize = true;
        this.lblTotalTime.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.lblTotalTime.ForeColor = System.Drawing.Color.White;
        this.lblTotalTime.Location = new System.Drawing.Point(750, 365);
        this.lblTotalTime.Name = "lblTotalTime";
        this.lblTotalTime.Size = new System.Drawing.Size(32, 17);
        this.lblTotalTime.TabIndex = 7;
        this.lblTotalTime.Text = "0:00";
        // 
        // btnRepeat
        // 
        this.btnRepeat.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
        this.btnRepeat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
        this.btnRepeat.FlatAppearance.BorderSize = 0;
        this.btnRepeat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnRepeat.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.btnRepeat.ForeColor = System.Drawing.Color.White;
        this.btnRepeat.Location = new System.Drawing.Point(290, 430);
        this.btnRepeat.Name = "btnRepeat";
        this.btnRepeat.Size = new System.Drawing.Size(100, 45);
        this.btnRepeat.TabIndex = 8;
        this.btnRepeat.Text = "🔁";
        this.btnRepeat.UseVisualStyleBackColor = false;
        this.btnRepeat.Click += new System.EventHandler(this.BtnRepeat_Click);
        // 
        // btnShuffle
        // 
        this.btnShuffle.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
        this.btnShuffle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
        this.btnShuffle.FlatAppearance.BorderSize = 0;
        this.btnShuffle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnShuffle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.btnShuffle.ForeColor = System.Drawing.Color.White;
        this.btnShuffle.Location = new System.Drawing.Point(395, 430);
        this.btnShuffle.Name = "btnShuffle";
        this.btnShuffle.Size = new System.Drawing.Size(100, 45);
        this.btnShuffle.TabIndex = 9;
        this.btnShuffle.Text = "🔀 Shuffle OFF";
        this.btnShuffle.UseVisualStyleBackColor = false;
        this.btnShuffle.Click += new System.EventHandler(this.BtnShuffle_Click);
        // 
        // btnPrevious
        // 
        this.btnPrevious.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
        this.btnPrevious.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
        this.btnPrevious.FlatAppearance.BorderSize = 0;
        this.btnPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnPrevious.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.btnPrevious.ForeColor = System.Drawing.Color.White;
        this.btnPrevious.Location = new System.Drawing.Point(500, 430);
        this.btnPrevious.Name = "btnPrevious";
        this.btnPrevious.Size = new System.Drawing.Size(32, 45);
        this.btnPrevious.TabIndex = 16;
        this.btnPrevious.Text = "◀◀";
        this.btnPrevious.UseVisualStyleBackColor = false;
        this.btnPrevious.Click += new System.EventHandler(this.BtnPrevious_Click);
        // 
        // btnPlayPause
        // 
        this.btnPlayPause.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
        this.btnPlayPause.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
        this.btnPlayPause.FlatAppearance.BorderSize = 0;
        this.btnPlayPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnPlayPause.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.btnPlayPause.ForeColor = System.Drawing.Color.White;
        this.btnPlayPause.Location = new System.Drawing.Point(534, 430);
        this.btnPlayPause.Name = "btnPlayPause";
        this.btnPlayPause.Size = new System.Drawing.Size(40, 45);
        this.btnPlayPause.TabIndex = 10;
        this.btnPlayPause.Text = "▶";
        this.btnPlayPause.UseVisualStyleBackColor = false;
        this.btnPlayPause.Click += new System.EventHandler(this.BtnPlayPause_Click);
        // 
        // btnNext
        // 
        this.btnNext.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
        this.btnNext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
        this.btnNext.FlatAppearance.BorderSize = 0;
        this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnNext.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.btnNext.ForeColor = System.Drawing.Color.White;
        this.btnNext.Location = new System.Drawing.Point(576, 430);
        this.btnNext.Name = "btnNext";
        this.btnNext.Size = new System.Drawing.Size(32, 45);
        this.btnNext.TabIndex = 17;
        this.btnNext.Text = "▶▶";
        this.btnNext.UseVisualStyleBackColor = false;
        this.btnNext.Click += new System.EventHandler(this.BtnNext_Click);
        // 
        // btnStop
        // 
        this.btnStop.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
        this.btnStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
        this.btnStop.FlatAppearance.BorderSize = 0;
        this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnStop.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.btnStop.ForeColor = System.Drawing.Color.White;
        this.btnStop.Location = new System.Drawing.Point(610, 430);
        this.btnStop.Name = "btnStop";
        this.btnStop.Size = new System.Drawing.Size(32, 45);
        this.btnStop.TabIndex = 11;
        this.btnStop.Text = "⏹";
        this.btnStop.UseVisualStyleBackColor = false;
        this.btnStop.Click += new System.EventHandler(this.BtnStop_Click);
        // 
        // btnFastForward
        // 
        this.btnFastForward.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
        this.btnFastForward.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
        this.btnFastForward.FlatAppearance.BorderSize = 0;
        this.btnFastForward.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnFastForward.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.btnFastForward.ForeColor = System.Drawing.Color.White;
        this.btnFastForward.Location = new System.Drawing.Point(644, 430);
        this.btnFastForward.Name = "btnFastForward";
        this.btnFastForward.Size = new System.Drawing.Size(32, 45);
        this.btnFastForward.TabIndex = 12;
        this.btnFastForward.Text = "⏩";
        this.btnFastForward.UseVisualStyleBackColor = false;
        this.btnFastForward.Click += new System.EventHandler(this.BtnFastForward_Click);
        // 
        // tbVolume
        // 
        this.tbVolume.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.tbVolume.Location = new System.Drawing.Point(680, 420);
        this.tbVolume.Maximum = 100;
        this.tbVolume.Name = "tbVolume";
        this.tbVolume.Size = new System.Drawing.Size(110, 45);
        this.tbVolume.TabIndex = 13;
        this.tbVolume.TickStyle = System.Windows.Forms.TickStyle.None;
        this.tbVolume.Value = 100;
        this.tbVolume.ValueChanged += new System.EventHandler(this.TbVolume_ValueChanged);
        // 
        // lblVolume
        // 
        this.lblVolume.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.lblVolume.AutoSize = true;
        this.lblVolume.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.lblVolume.ForeColor = System.Drawing.Color.DarkGray;
        this.lblVolume.Location = new System.Drawing.Point(680, 455);
        this.lblVolume.Name = "lblVolume";
        this.lblVolume.Size = new System.Drawing.Size(82, 15);
        this.lblVolume.TabIndex = 14;
        this.lblVolume.Text = "Volume: 100%";
        // 
        // pbArtwork
        // 
        this.pbArtwork.Anchor = System.Windows.Forms.AnchorStyles.Top;
        this.pbArtwork.Location = new System.Drawing.Point(465, 200);
        this.pbArtwork.Name = "pbArtwork";
        this.pbArtwork.Size = new System.Drawing.Size(150, 150);
        this.pbArtwork.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
        this.pbArtwork.TabIndex = 15;
        this.pbArtwork.TabStop = false;
        // 
        // Form1
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
        this.ClientSize = new System.Drawing.Size(820, 520);
        this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.ForeColor = System.Drawing.Color.White;
        this.Controls.Add(this.lblVolume);
        this.Controls.Add(this.tbVolume);
        this.Controls.Add(this.pbArtwork);
        this.Controls.Add(this.btnRepeat);
        this.Controls.Add(this.btnShuffle);
        this.Controls.Add(this.btnFastForward);
        this.Controls.Add(this.btnStop);
        this.Controls.Add(this.btnNext);
        this.Controls.Add(this.btnPlayPause);
        this.Controls.Add(this.btnPrevious);
        this.Controls.Add(this.lblTotalTime);
        this.Controls.Add(this.lblCurrentTime);
        this.Controls.Add(this.tbProgress);
        this.Controls.Add(this.lblArtist);
        this.Controls.Add(this.lblSongTitle);
        this.Controls.Add(this.lblSongCount);
        this.Controls.Add(this.cmbFilter);
        this.Controls.Add(this.btnClearSearch);
        this.Controls.Add(this.txtSearch);
        this.Controls.Add(this.lblSearch);
        this.Controls.Add(this.btnAddMusic);
        this.Controls.Add(this.btnDeleteMusic);
        this.Controls.Add(this.btnFavorite);
        this.Controls.Add(this.lstSongs);
        this.Name = "Form1";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "🎵 Music Player";
        ((System.ComponentModel.ISupportInitialize)(this.tbProgress)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.tbVolume)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.pbArtwork)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ListBox lstSongs;
    private System.Windows.Forms.Label lblSongCount;
    private System.Windows.Forms.Button btnAddMusic;
    private System.Windows.Forms.Button btnDeleteMusic;
    private System.Windows.Forms.Button btnFavorite;
    private System.Windows.Forms.ComboBox cmbFilter;
    private System.Windows.Forms.Label lblSearch;
    private System.Windows.Forms.TextBox txtSearch;
    private System.Windows.Forms.Button btnClearSearch;
    private System.Windows.Forms.Label lblSongTitle;
    private System.Windows.Forms.Label lblArtist;
    private System.Windows.Forms.TrackBar tbProgress;
    private System.Windows.Forms.Label lblCurrentTime;
    private System.Windows.Forms.Label lblTotalTime;
    private System.Windows.Forms.Button btnRepeat;
    private System.Windows.Forms.Button btnShuffle;
    private System.Windows.Forms.Button btnPrevious;
    private System.Windows.Forms.Button btnPlayPause;
    private System.Windows.Forms.Button btnNext;
    private System.Windows.Forms.Button btnStop;
    private System.Windows.Forms.Button btnFastForward;
    private System.Windows.Forms.TrackBar tbVolume;
    private System.Windows.Forms.Label lblVolume;
    private System.Windows.Forms.PictureBox pbArtwork;
}
