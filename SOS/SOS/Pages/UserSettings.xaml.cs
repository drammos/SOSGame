namespace SOS.Pages;

public partial class UserSettings : ContentPage
{
	public UserSettings()
	{
		InitializeComponent();
        if (App.User != null)
        {
            lblSetUserName.Text = App.User.UserName;
            lblSetUserMail.Text = App.User.Email;
            if (App.User.FilePath != null && App.User.FilePath != "")
            {
                lblSetUserPhotoSettings.Source = App.User.FilePath;
            }
        }
    }
}