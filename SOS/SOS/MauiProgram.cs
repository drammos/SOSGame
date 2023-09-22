using SOS.Models;
using SOS.RegisterModel;
using SOS.Services;
using SOS.ViewModel;
using SQLite;
using Syncfusion.Maui.Core.Hosting;
namespace SOS;
using CommunityToolkit.Maui;

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


        builder.Services.AddSingleton((_) =>
        {
            var database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            database.CreateTableAsync<User>();
            return database;
        });
        builder.Services.AddSingleton<ILoginRepo, LoginService>();
        builder.Services.AddSingleton<IRegisterRepo, RegisterService>();
        builder.Services.AddSingleton<StartGame>();
		builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddSingleton<HighScore>();
        builder.Services.AddSingleton<Settings>();
		builder.Services.AddSingleton<RegisterPage>();
        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<RegisterViewModel>();
        return builder.Build();
	}
}
