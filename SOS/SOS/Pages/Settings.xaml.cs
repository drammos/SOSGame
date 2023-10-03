using SOS.ViewModel;

namespace SOS;


public partial class Settings : ContentPage
{

    public Settings(SettingsViewModel settingsViewModel)
    {
        InitializeComponent();
        this.BindingContext = settingsViewModel;
    }
}