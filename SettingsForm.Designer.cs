namespace MusicPlayer;

partial class SettingsForm
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
        this.lblPlayback = new System.Windows.Forms.Label();
        this.lblDefaultVolume = new System.Windows.Forms.Label();
        this.tbDefaultVolume = new System.Windows.Forms.TrackBar();
        this.lblVolumeValue = new System.Windows.Forms.Label();
        this.chkStartPlayback = new System.Windows.Forms.CheckBox();
        
        this.lblLibrary = new System.Windows.Forms.Label();
        this.chkConfirmDelete = new System.Windows.Forms.CheckBox();

        this.lblAppearance = new System.Windows.Forms.Label();
        this.chkDarkTheme = new System.Windows.Forms.CheckBox();

        this.btnSave = new System.Windows.Forms.Button();
        this.btnCancel = new System.Windows.Forms.Button();

        ((System.ComponentModel.ISupportInitialize)(this.tbDefaultVolume)).BeginInit();
        this.SuspendLayout();
        // 
        // lblPlayback
        // 
        this.lblPlayback.AutoSize = true;
        this.lblPlayback.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        this.lblPlayback.Location = new System.Drawing.Point(20, 20);
        this.lblPlayback.Name = "lblPlayback";
        this.lblPlayback.Size = new System.Drawing.Size(78, 21);
        this.lblPlayback.TabIndex = 0;
        this.lblPlayback.Text = "Playback";
        // 
        // lblDefaultVolume
        // 
        this.lblDefaultVolume.AutoSize = true;
        this.lblDefaultVolume.Location = new System.Drawing.Point(20, 60);
        this.lblDefaultVolume.Name = "lblDefaultVolume";
        this.lblDefaultVolume.Size = new System.Drawing.Size(96, 17);
        this.lblDefaultVolume.TabIndex = 1;
        this.lblDefaultVolume.Text = "Default Volume";
        // 
        // tbDefaultVolume
        // 
        this.tbDefaultVolume.Location = new System.Drawing.Point(130, 60);
        this.tbDefaultVolume.Maximum = 100;
        this.tbDefaultVolume.Name = "tbDefaultVolume";
        this.tbDefaultVolume.Size = new System.Drawing.Size(150, 45);
        this.tbDefaultVolume.TabIndex = 2;
        this.tbDefaultVolume.TickStyle = System.Windows.Forms.TickStyle.None;
        this.tbDefaultVolume.ValueChanged += new System.EventHandler(this.tbDefaultVolume_ValueChanged);
        // 
        // lblVolumeValue
        // 
        this.lblVolumeValue.AutoSize = true;
        this.lblVolumeValue.Location = new System.Drawing.Point(290, 60);
        this.lblVolumeValue.Name = "lblVolumeValue";
        this.lblVolumeValue.Size = new System.Drawing.Size(40, 17);
        this.lblVolumeValue.TabIndex = 3;
        this.lblVolumeValue.Text = "100%";
        // 
        // chkStartPlayback
        // 
        this.chkStartPlayback.AutoSize = true;
        this.chkStartPlayback.Location = new System.Drawing.Point(20, 100);
        this.chkStartPlayback.Name = "chkStartPlayback";
        this.chkStartPlayback.Size = new System.Drawing.Size(202, 21);
        this.chkStartPlayback.TabIndex = 4;
        this.chkStartPlayback.Text = "Start Playback Automatically";
        this.chkStartPlayback.UseVisualStyleBackColor = true;
        // 
        // lblLibrary
        // 
        this.lblLibrary.AutoSize = true;
        this.lblLibrary.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        this.lblLibrary.Location = new System.Drawing.Point(20, 140);
        this.lblLibrary.Name = "lblLibrary";
        this.lblLibrary.Size = new System.Drawing.Size(65, 21);
        this.lblLibrary.TabIndex = 5;
        this.lblLibrary.Text = "Library";
        // 
        // chkConfirmDelete
        // 
        this.chkConfirmDelete.AutoSize = true;
        this.chkConfirmDelete.Location = new System.Drawing.Point(20, 170);
        this.chkConfirmDelete.Name = "chkConfirmDelete";
        this.chkConfirmDelete.Size = new System.Drawing.Size(211, 21);
        this.chkConfirmDelete.TabIndex = 6;
        this.chkConfirmDelete.Text = "Confirm Before Deleting Songs";
        this.chkConfirmDelete.UseVisualStyleBackColor = true;
        // 
        // lblAppearance
        // 
        this.lblAppearance.AutoSize = true;
        this.lblAppearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        this.lblAppearance.Location = new System.Drawing.Point(20, 210);
        this.lblAppearance.Name = "lblAppearance";
        this.lblAppearance.Size = new System.Drawing.Size(100, 21);
        this.lblAppearance.TabIndex = 7;
        this.lblAppearance.Text = "Appearance";
        // 
        // chkDarkTheme
        // 
        this.chkDarkTheme.AutoSize = true;
        this.chkDarkTheme.Checked = true;
        this.chkDarkTheme.CheckState = System.Windows.Forms.CheckState.Checked;
        this.chkDarkTheme.Enabled = false;
        this.chkDarkTheme.Location = new System.Drawing.Point(20, 240);
        this.chkDarkTheme.Name = "chkDarkTheme";
        this.chkDarkTheme.Size = new System.Drawing.Size(98, 21);
        this.chkDarkTheme.TabIndex = 8;
        this.chkDarkTheme.Text = "Dark Theme";
        this.chkDarkTheme.UseVisualStyleBackColor = true;
        // 
        // btnSave
        // 
        this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
        this.btnSave.FlatAppearance.BorderSize = 0;
        this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnSave.Location = new System.Drawing.Point(120, 290);
        this.btnSave.Name = "btnSave";
        this.btnSave.Size = new System.Drawing.Size(80, 30);
        this.btnSave.TabIndex = 9;
        this.btnSave.Text = "Save";
        this.btnSave.UseVisualStyleBackColor = false;
        this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
        // 
        // btnCancel
        // 
        this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
        this.btnCancel.FlatAppearance.BorderSize = 0;
        this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnCancel.Location = new System.Drawing.Point(210, 290);
        this.btnCancel.Name = "btnCancel";
        this.btnCancel.Size = new System.Drawing.Size(80, 30);
        this.btnCancel.TabIndex = 10;
        this.btnCancel.Text = "Cancel";
        this.btnCancel.UseVisualStyleBackColor = false;
        this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
        // 
        // SettingsForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
        this.ClientSize = new System.Drawing.Size(350, 340);
        this.Controls.Add(this.btnCancel);
        this.Controls.Add(this.btnSave);
        this.Controls.Add(this.chkDarkTheme);
        this.Controls.Add(this.lblAppearance);
        this.Controls.Add(this.chkConfirmDelete);
        this.Controls.Add(this.lblLibrary);
        this.Controls.Add(this.chkStartPlayback);
        this.Controls.Add(this.lblVolumeValue);
        this.Controls.Add(this.tbDefaultVolume);
        this.Controls.Add(this.lblDefaultVolume);
        this.Controls.Add(this.lblPlayback);
        this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.ForeColor = System.Drawing.Color.White;
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "SettingsForm";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        this.Text = "Player Settings";
        ((System.ComponentModel.ISupportInitialize)(this.tbDefaultVolume)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private System.Windows.Forms.Label lblPlayback;
    private System.Windows.Forms.Label lblDefaultVolume;
    private System.Windows.Forms.TrackBar tbDefaultVolume;
    private System.Windows.Forms.Label lblVolumeValue;
    private System.Windows.Forms.CheckBox chkStartPlayback;
    private System.Windows.Forms.Label lblLibrary;
    private System.Windows.Forms.CheckBox chkConfirmDelete;
    private System.Windows.Forms.Label lblAppearance;
    private System.Windows.Forms.CheckBox chkDarkTheme;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.Button btnCancel;
}
