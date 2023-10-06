using CommunityToolkit.Maui.Views;
using Microsoft.Win32;
using SOS.ViewModel;

using Plugin.Media;
using Plugin.Media.Abstractions;
using System.Diagnostics;

namespace SOS;

public partial class RegisterPage : ContentPage
{

    private int selectedCompressionQuality;
    private RegisterViewModel registerViewModel;

    public RegisterPage(RegisterViewModel registerViewModel)
    {
        
        InitializeComponent();
        this.selectedCompressionQuality = 10;
        this.BindingContext = registerViewModel;
        this.registerViewModel = registerViewModel;
        CounterBtn.IsEnabled = registerViewModel.IsSignUpButtonEnabled();
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        // Καλέστε την μέθοδο για επιστροφή πίσω
        await Navigation.PopAsync();
    }


    public async void PopUpButton(object sender, EventArgs e)
    {
        bool result = await this.registerViewModel.PopUp();
        if (result) 
        {
            OnPickPhotoClicked();
        }
        else
        {
            OnTakePhotoClicked();
        }
    }

    private async void OnPickPhotoClicked()
    {
        var result = await CrossMedia.Current.PickPhotoAsync();
        if (result is null) return;

        string res = result?.Path;
        UploadedOrSelectedImage.Source = res;
    }

    private async void OnTakePhotoClicked()
    {
        var options = new StoreCameraMediaOptions { CompressionQuality = 100 };
        var result = await CrossMedia.Current.TakePhotoAsync(options);
        if (result is null) return;

        string res = result?.Path;
        UploadedOrSelectedImage.Source = res;
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        this.registerViewModel?.Dispose();
        UploadedOrSelectedImage.Source = "user.png";

        UserFrameEntry.Text = null;
        PasswordFrameEntry.Text = null;
        ConfirmPasswordFrameEntry.Text = null;
        EmailFrameEntry.Text = null;
    }
}