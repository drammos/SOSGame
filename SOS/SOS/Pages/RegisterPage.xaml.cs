using CommunityToolkit.Maui.Views;
using Microsoft.Win32;
using SOS.ViewModel;

using Plugin.Media;
using Plugin.Media.Abstractions;
using System.IO.Compression;

namespace SOS;

public partial class RegisterPage : ContentPage
{

    private int selectedCompressionQuality;

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


    private async void OnPickPhotoClicked(object sender, EventArgs e)
    {
        var result = await CrossMedia.Current.PickPhotoAsync();
        if (result is null) return;

        UploadedOrSelectedImage.Source = result?.Path;

        var fileInfo = new FileInfo(result?.Path);
        var fileLength = fileInfo.Length;

        FileSizeLabel.Text = $"Image size: {fileLength / 1000} kB";
    }

    private async void OnTakePhotoClicked(object sender, EventArgs e)
    {
        var options = new StoreCameraMediaOptions { CompressionQuality = selectedCompressionQuality };
        var result = await CrossMedia.Current.TakePhotoAsync(options);
        if (result is null) return;

        UploadedOrSelectedImage.Source = result?.Path;

        var fileInfo = new FileInfo(result?.Path);
        var fileLength = fileInfo.Length;

        FileSizeLabel.Text = $"Image size: {fileLength / 1000} kB";
    }

}