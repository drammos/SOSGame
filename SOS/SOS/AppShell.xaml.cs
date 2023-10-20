using SOS.ViewModel;

namespace SOS;

public partial class AppShell : Shell
{
    public AppShell(AppShellViewModel appShellViewModel)
    {
        InitializeComponent();
        Routing.RegisterRoute($"{nameof(LoginPage)}/{nameof(RegisterPage)}", typeof(RegisterPage));
        Routing.RegisterRoute($"{nameof(StartGame)}/{nameof(Game)}", typeof(Game));
        this.BindingContext = appShellViewModel;
    }
}