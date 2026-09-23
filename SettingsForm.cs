using System;
using System.Windows.Forms;

namespace MusicPlayer;

public partial class SettingsForm : Form
{
    private readonly SettingsService _settingsService;

    public SettingsForm(SettingsService settingsService)
    {
        _settingsService = settingsService;
        InitializeComponent();
        LoadSettings();
    }

    private void LoadSettings()
    {
        tbDefaultVolume.Value = _settingsService.DefaultVolume;
        lblVolumeValue.Text = $"{_settingsService.DefaultVolume}%";
        chkStartPlayback.Checked = _settingsService.StartPlaybackAutomatically;
        chkConfirmDelete.Checked = _settingsService.ConfirmBeforeDeletingSongs;
    }

    private void tbDefaultVolume_ValueChanged(object? sender, EventArgs e)
    {
        lblVolumeValue.Text = $"{tbDefaultVolume.Value}%";
    }

    private void btnSave_Click(object? sender, EventArgs e)
    {
        _settingsService.DefaultVolume = tbDefaultVolume.Value;
        _settingsService.StartPlaybackAutomatically = chkStartPlayback.Checked;
        _settingsService.ConfirmBeforeDeletingSongs = chkConfirmDelete.Checked;
        _settingsService.SaveSettings();

        this.DialogResult = DialogResult.OK;
        this.Close();
    }

    private void btnCancel_Click(object? sender, EventArgs e)
    {
        this.DialogResult = DialogResult.Cancel;
        this.Close();
    }
}
