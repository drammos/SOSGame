using CommunityToolkit.Maui.Views;
using Microsoft.Win32;
using SOS.RegisterModel;
using SOS.ViewModel;

namespace SOS;

public partial class RegisterPage : ContentPage
{
	public RegisterPage(RegisterViewModel registerViewModel)
	{
		InitializeComponent();
        this.BindingContext = registerViewModel;
    }

    private async void Simple(object sender, EventArgs e)
    {
        var mes = new VarMessage("ela111");
        var pop = new PopUp(mes);
        //this.ShowPopup(pop);
    }

    private async void SignUp()
    {
        RegisterViewModel registerViewModel = this.BindingContext;
    }

}