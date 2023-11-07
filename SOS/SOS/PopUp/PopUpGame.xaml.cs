using CommunityToolkit.Maui.Views;
using SOS.ViewModel;


namespace SOS.Popups;

public partial class PopUpGame : Popup
{
    public PopUpGame(VarMessage varMessage)
    {
        InitializeComponent();
        this.BindingContext = varMessage;
    }

    private void ClickedPlay(object sender, EventArgs e)
    {
        Close("play");
    }

    private void ClickedQuit(object sender, EventArgs e)
    {
        Close("quit");
    }
}