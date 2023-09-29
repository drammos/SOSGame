using SOS.ViewModel;

namespace SOS;

public partial class AppShell : Shell
{
	public AppShell(AppShellViewModel appShellViewModel)
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
        this.BindingContext = appShellViewModel;	
	}
}
