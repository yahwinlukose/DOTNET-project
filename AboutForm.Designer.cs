namespace MusicPlayer;

partial class AboutForm
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
        this.lblTitle = new System.Windows.Forms.Label();
        this.lblSubtitle = new System.Windows.Forms.Label();
        this.lblFeatures = new System.Windows.Forms.Label();
        this.lblDeveloper = new System.Windows.Forms.Label();
        this.btnGitHub = new System.Windows.Forms.Button();
        this.btnClose = new System.Windows.Forms.Button();
        this.SuspendLayout();
        // 
        // lblTitle
        // 
        this.lblTitle.AutoSize = true;
        this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        this.lblTitle.Location = new System.Drawing.Point(50, 20);
        this.lblTitle.Name = "lblTitle";
        this.lblTitle.Size = new System.Drawing.Size(262, 45);
        this.lblTitle.TabIndex = 0;
        this.lblTitle.Text = "🎵 MusicPlayer";
        this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // lblSubtitle
        // 
        this.lblSubtitle.AutoSize = true;
        this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.lblSubtitle.Location = new System.Drawing.Point(40, 75);
        this.lblSubtitle.Name = "lblSubtitle";
        this.lblSubtitle.Size = new System.Drawing.Size(268, 42);
        this.lblSubtitle.TabIndex = 1;
        this.lblSubtitle.Text = "A modern desktop music player\r\nBuilt using C# and .NET 10";
        this.lblSubtitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // lblFeatures
        // 
        this.lblFeatures.AutoSize = true;
        this.lblFeatures.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.lblFeatures.Location = new System.Drawing.Point(80, 135);
        this.lblFeatures.Name = "lblFeatures";
        this.lblFeatures.Size = new System.Drawing.Size(201, 133);
        this.lblFeatures.TabIndex = 2;
        this.lblFeatures.Text = "Features:\r\n• Music library management\r\n• Search and favorites\r\n• Shuffle and repeat\r\n• Album artwork\r\n• Folder scanning\r\n• Audio playback";
        // 
        // lblDeveloper
        // 
        this.lblDeveloper.AutoSize = true;
        this.lblDeveloper.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.lblDeveloper.Location = new System.Drawing.Point(120, 280);
        this.lblDeveloper.Name = "lblDeveloper";
        this.lblDeveloper.Size = new System.Drawing.Size(126, 57);
        this.lblDeveloper.TabIndex = 3;
        this.lblDeveloper.Text = "Developed by\r\nYahwin\r\nVersion 1.0.0";
        this.lblDeveloper.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // btnGitHub
        // 
        this.btnGitHub.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
        this.btnGitHub.FlatAppearance.BorderSize = 0;
        this.btnGitHub.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnGitHub.Location = new System.Drawing.Point(50, 350);
        this.btnGitHub.Name = "btnGitHub";
        this.btnGitHub.Size = new System.Drawing.Size(100, 35);
        this.btnGitHub.TabIndex = 4;
        this.btnGitHub.Text = "GitHub";
        this.btnGitHub.UseVisualStyleBackColor = false;
        this.btnGitHub.Click += new System.EventHandler(this.btnGitHub_Click);
        // 
        // btnClose
        // 
        this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
        this.btnClose.FlatAppearance.BorderSize = 0;
        this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnClose.Location = new System.Drawing.Point(220, 350);
        this.btnClose.Name = "btnClose";
        this.btnClose.Size = new System.Drawing.Size(100, 35);
        this.btnClose.TabIndex = 5;
        this.btnClose.Text = "Close";
        this.btnClose.UseVisualStyleBackColor = false;
        this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
        // 
        // AboutForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
        this.ClientSize = new System.Drawing.Size(370, 410);
        this.Controls.Add(this.btnClose);
        this.Controls.Add(this.btnGitHub);
        this.Controls.Add(this.lblDeveloper);
        this.Controls.Add(this.lblFeatures);
        this.Controls.Add(this.lblSubtitle);
        this.Controls.Add(this.lblTitle);
        this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.ForeColor = System.Drawing.Color.White;
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "AboutForm";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        this.Text = "About MusicPlayer";
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private System.Windows.Forms.Label lblTitle;
    private System.Windows.Forms.Label lblSubtitle;
    private System.Windows.Forms.Label lblFeatures;
    private System.Windows.Forms.Label lblDeveloper;
    private System.Windows.Forms.Button btnGitHub;
    private System.Windows.Forms.Button btnClose;
}
