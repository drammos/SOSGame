using System.Diagnostics;

namespace SOS.UseControl;

public partial class FlyoutHead : ContentView
{
	public FlyoutHead()
	{
		InitializeComponent();
		if(App.User != null)
		{
			lblUserName.Text = "Logged in as:" + App.User.UserName;
			lblUserEmail.Text = App.User.Email;
			lblUserPhoto.Source = App.User.FilePath;
		}
	}
}