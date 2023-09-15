using SOS.ViewModel;

namespace SOS;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});


		builder.Services.AddSingleton<StartGame>();
		builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddSingleton<HighScore>();
        builder.Services.AddSingleton<Settings>();

        builder.Services.AddSingleton<LoginViewModel>();

        return builder.Build();
	}
}
