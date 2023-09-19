using SOS.Services;
using SOS.ViewModel;
using Syncfusion.Maui.Core.Hosting;
namespace SOS;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureSyncfusionCore()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});


		builder.Services.AddSingleton<StartGame>();
		builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddSingleton<HighScore>();
        builder.Services.AddSingleton<Settings>();
		builder.Services.AddSingleton<RegisterPage>();
        builder.Services.AddSingleton<LoginViewModel>();
		builder.Services.AddSingleton<LoginService>();

        return builder.Build();
	}
}
