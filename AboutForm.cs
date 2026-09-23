using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace MusicPlayer;

public partial class AboutForm : Form
{
    public AboutForm()
    {
        InitializeComponent();
    }

    private void btnGitHub_Click(object? sender, EventArgs e)
    {
        try
        {
            Process.Start(new ProcessStartInfo("https://github.com/yahwinlukose/DOTNET-project") { UseShellExecute = true });
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Failed to open GitHub link: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnClose_Click(object? sender, EventArgs e)
    {
        this.Close();
    }
}
