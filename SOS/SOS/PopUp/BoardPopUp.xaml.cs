using CommunityToolkit.Maui.Views;
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