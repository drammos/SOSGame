using Plugin.Media.Abstractions;
using Plugin.Media;
using SOS.ViewModel;

namespace SOS.Pages;

public partial class UserSettings : ContentPage
{
    public UserSettingsViewModel userSettingsViewModel;

	public UserSettings(UserSettingsViewModel userSettingsViewModel)
	{
		InitializeComponent();
        this.BindingContext = userSettingsViewModel;
        this.userSettingsViewModel = userSettingsViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (App.User != null)
        {
            this.userSettingsViewModel.UserName = App.User.UserName;
            this.userSettingsViewModel.Email = App.User.Email;
            if (App.User.FilePath == null || App.User.FilePath.Length == 0)
            {
                this.userSettingsViewModel.FilePath = "user.png";
            }
            else
            {
                this.userSettingsViewModel.FilePath = App.User.FilePath;
            }
            this.userImage.Source = this.userSettingsViewModel.FilePath;
            this.userSettingsViewModel.Gid = App.User.Gid;
            this.userSettingsViewModel.Password = App.User.Password;
        }
    }

    public async void PopUpButton(object sender, EventArgs e)
    {
        bool result = await this.userSettingsViewModel.PopUp();
        if (!result)
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
        this.userSettingsViewModel.FilePath = res;
    }

    private async void OnTakePhotoClicked()
    {
        var options = new StoreCameraMediaOptions { CompressionQuality = 100 };
        var result = await CrossMedia.Current.TakePhotoAsync(options);
        if (result is null) return;

        string res = result?.Path;
        this.userSettingsViewModel.FilePath = res;
    }

    private async void EntryTextChanged(object sender, TextChangedEventArgs e)
    {
        if (e.NewTextValue != null)
        {
            this.userSettingsViewModel.IsAllEntriesFilled();
        }
    }

}