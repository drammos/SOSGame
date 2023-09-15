using SOS.ViewModel;
namespace SOS;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginViewModel loginViewModel)
	{
		InitializeComponent();
		this.BindingContext = loginViewModel;
	}
}