using CommunityToolkit.Mvvm.ComponentModel;
using SOS.Models;
using SOS.ViewModel;
using SQLite;
using System.ComponentModel;

namespace SOS;

public partial class App : Application, INotifyPropertyChanged
{

    public static User User;
   
    public App(AppShellViewModel appShellViewModel)
    {
        InitializeComponent();
        MainPage = new AppShell(appShellViewModel);
    }
}
