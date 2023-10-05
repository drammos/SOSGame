using System.Diagnostics;

namespace SOS.UseControl;

public partial class FlyoutHead : ContentView
{
	public FlyoutHead()
	{
		InitializeComponent();
		if(App.User != null)
		{
			lblUserName.Text = App.User.UserName;
			if(App.User.FilePath != null && App.User.FilePath != "") 
			{
                lblUserPhoto.Source = App.User.FilePath;
            }
		}
	}
}