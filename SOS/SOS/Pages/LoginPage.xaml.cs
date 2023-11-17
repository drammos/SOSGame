using SOS.ViewModel;
using System.Windows.Input;
namespace SOS;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginViewModel loginViewModel)
    {
        InitializeComponent();
        this.BindingContext = loginViewModel;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await Task.Delay(100);
        img.IsAnimationPlaying = true;
        Application.Current.UserAppTheme = AppTheme.Light;
        PasswordFrameEntry.IsPassword = true;
        ImageButtonEye.Source = "hide.png";
    }

    private void OnShowHidePasswordClicked(object sender, EventArgs e)
    {
        if (PasswordFrameEntry.IsPassword)
        {
            PasswordFrameEntry.IsPassword = false;
            ImageButtonEye.Source = "show.png";
        }
        else
        {
            PasswordFrameEntry.IsPassword = true;
            ImageButtonEye.Source = "hide.png";
        }
    }
}