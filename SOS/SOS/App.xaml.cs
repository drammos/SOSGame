
using SOS.Models;

namespace SOS;

public partial class App : Application
{

	public App()
	{
		InitializeComponent();
		MainPage = new AppShell();
	}
}
