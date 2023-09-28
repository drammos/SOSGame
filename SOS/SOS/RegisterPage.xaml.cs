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

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        // Καλέστε την μέθοδο για επιστροφή πίσω
        await Navigation.PopAsync();
    }
}