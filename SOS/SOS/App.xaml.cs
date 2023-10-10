﻿using CommunityToolkit.Mvvm.ComponentModel;
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
        //Mjc1MjE4NkAzMjMzMmUzMDJlMzBLUjZSRGNIb3o4K1pjemk5aGg4L0VnN29peVExS0NIV0JDSjhyUTN3QjNrPQ==
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mjc1MjE4NkAzMjMzMmUzMDJlMzBLUjZSRGNIb3o4K1pjemk5aGg4L0VnN29peVExS0NIV0JDSjhyUTN3QjNrPQ==");
        InitializeComponent();
        MainPage = new AppShell(appShellViewModel);
    }
}
