using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SOS.ViewModel;
using SOS.Pages;
using Plugin.Media.Abstractions;
using Plugin.Media;

namespace SOS;

public partial class PopUpPhoto : Popup
{

	public PopUpPhoto(VarMessage varMessage)
	{
		InitializeComponent();
        this.BindingContext = varMessage;
        CanBeDismissedByTappingOutsideOfPopup = true;
    }


    private async void OnPickPhotoClicked(object sender, EventArgs e)
    {
        var result = await CrossMedia.Current.PickPhotoAsync();
        if (result is null) return;

        string res = result?.Path;
        await CloseAsync(res);
    }

    private async void OnTakePhotoClicked(object sender, EventArgs e)
    {
        var options = new StoreCameraMediaOptions { CompressionQuality = 100 };
        var result = await CrossMedia.Current.TakePhotoAsync(options);
        if (result is null) return;

        string res =  result?.Path;
        await CloseAsync(res);
    }
}