using SOS.Models;
using SOS.ViewModel;
using SQLite;

namespace SOS;

public partial class App : Application
{

    public App(AppShellViewModel appShellViewModel)
    {
        InitializeComponent();
        MainPage = new AppShell(appShellViewModel);
    }
}
