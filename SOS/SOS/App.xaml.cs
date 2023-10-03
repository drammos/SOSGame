using SOS.Models;
using SOS.ViewModel;
using SQLite;

namespace SOS;

public partial class App : Application
{

    public static User User;

    public App(AppShellViewModel appShellViewModel)
    {
        InitializeComponent();
        MainPage = new AppShell(appShellViewModel);
    }
}
