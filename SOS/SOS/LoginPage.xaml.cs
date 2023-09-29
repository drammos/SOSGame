using SOS.ViewModel;
using System.Windows.Input;

namespace SOS;

public partial class LoginPage : ContentPage
{
    public ICommand TapCommand => new Command<string>(async (url) => await Launcher.OpenAsync(url));


    public LoginPage(LoginViewModel loginViewModel)
    {
        InitializeComponent();
        this.BindingContext = loginViewModel;
    }
}