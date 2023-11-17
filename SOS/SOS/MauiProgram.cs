﻿using SOS.Models;
using SOS.Services;
using SOS.ViewModel;
using SQLite;
using Syncfusion.Maui.Core.Hosting;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using SOS.Pages;

//using Plugin.Firebase.Auth;
using Microsoft.Maui.LifecycleEvents;


namespace SOS;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureSyncfusionCore()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
#if DEBUG
        builder.Logging.AddDebug();
#endif
        builder.Services.AddTransient<IPopupService, PopupService>();
        builder.Services.AddSingleton<AppShellViewModel>();
        builder.Services.AddSingleton<SettingsViewModel>();
        builder.Services.AddSingleton<ILoginRepo, LoginService>();
        builder.Services.AddSingleton<IRegisterRepo, RegisterService>();
        builder.Services.AddSingleton<IUpdateRepo, UpdateService>();
        builder.Services.AddTransient<IUsersRepo, UsersService>();
        builder.Services.AddSingleton<StartGame>();
        builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddSingleton<HighScore>();
        builder.Services.AddSingleton<HighScoreViewModel>();
        builder.Services.AddSingleton<GridGameViewModel>();
        builder.Services.AddSingleton<StartGameViewModel>();
        builder.Services.AddSingleton<Settings>();
        builder.Services.AddSingleton<UserSettings>();
        builder.Services.AddSingleton<RegisterPage>();
        builder.Services.AddSingleton<Game>();
        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<RegisterViewModel>();
        builder.Services.AddTransient<UserSettingsViewModel>();

        return builder.Build();
    }
}
