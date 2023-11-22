using SOS.Models;
using SOS.ViewModel;
using System.ComponentModel;

namespace SOS;

public partial class App : Application, INotifyPropertyChanged
{

    public static User User;
    public static SettingsData UserSettings;

    public App(AppShellViewModel appShellViewModel)
    {
        InitializeComponent();
        Application.Current.UserAppTheme = AppTheme.Unspecified;
        MainPage = new AppShell(appShellViewModel);
    }
}