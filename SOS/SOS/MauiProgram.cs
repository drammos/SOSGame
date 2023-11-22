using SOS.Services;
using SOS.ViewModel;
using Syncfusion.Maui.Core.Hosting;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using SOS.Pages;
using SOS.Firebase;

namespace SOS;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(Entry), (handler, view) =>
        {
#if ANDROID

            handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);

#elif IOS
            handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;
            handler.PlatformView.Layer.BorderWidth = 0;
            handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#endif
        });

        Microsoft.Maui.Handlers.PickerHandler.Mapper.AppendToMapping(nameof(Entry), (handler, view) =>
        {
#if ANDROID

            handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);

#elif IOS
            handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;
            handler.PlatformView.Layer.BorderWidth = 0;
            handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;

#endif
        });
#if DEBUG
        builder.Logging.AddDebug();
#endif
        
        // SERVICES
        builder.Services.AddSingleton<IFirebaseClient, FirebaseClient>();
        builder.Services.AddSingleton<AuthenticationService, AuthenticationService>();
        builder.Services.AddTransient<IPopupService, PopupService>();

        // VIEW MODELS
        builder.Services.AddSingleton<AppShellViewModel>();
        builder.Services.AddSingleton<SettingsViewModel>();
        builder.Services.AddSingleton<HighScoreViewModel>();
        builder.Services.AddSingleton<GridGameViewModel>();
        builder.Services.AddSingleton<StartGameViewModel>();
        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<RegisterViewModel>();
        builder.Services.AddTransient<UserSettingsViewModel>();

        // PAGES
        builder.Services.AddSingleton<StartGame>();
        builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddSingleton<HighScore>();
        builder.Services.AddSingleton<Settings>();
        builder.Services.AddSingleton<UserSettings>();
        builder.Services.AddSingleton<RegisterPage>();
        builder.Services.AddSingleton<Game>();
        

        return builder.Build();
    }
}
