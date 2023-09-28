using SOS.ViewModel;

namespace SOS;

public partial class AppShell : Shell
{
	public AppShell(AppShellViewModel appShellViewModel)
	{
		InitializeComponent();
		this.BindingContext = appShellViewModel;	
	}
}
