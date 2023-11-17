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
        //Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTUzMzc4QDMxMzkyZTM0MmUzMGp1L29pL2E3MVZQZVdmTmNBOElDai9QUS95dlV2OStuQi9qRHdmeDVaQzg9==");
        //Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mjc1MjE4NkAzMjMzMmUzMDJlMzBLUjZSRGNIb3o4K1pjemk5aGg4L0VnN29peVExS0NIV0JDSjhyUTN3QjNrPQ==");
        InitializeComponent();
        Application.Current.UserAppTheme = AppTheme.Unspecified;
        MainPage = new AppShell(appShellViewModel);
    }
}