
using SOS.Models;

namespace SOS;

public partial class App : Application
{
	public static Info UserInfo;

	public App()
	{
		InitializeComponent();
		MainPage = new AppShell();
	}
}
