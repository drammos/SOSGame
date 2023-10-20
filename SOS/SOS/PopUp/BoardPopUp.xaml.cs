using Plugin.Media.Abstractions;
using Plugin.Media;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SOS.ViewModel;
namespace SOS.Popups;

public partial class BoardPopUp : Popup
{
	public BoardPopUp()
	{
		InitializeComponent();

		CanBeDismissedByTappingOutsideOfPopup = true;
	}

    private void CheckS(object sender, EventArgs e)
    {
        Close("S");
    }

    private void CheckO(object sender, EventArgs e)
    {
        Close("O");
    }
}