using CommunityToolkit.Maui.Views;
using SOS.ViewModel;

namespace SOS.Popups;

public partial class PopUp : Popup
{

    public PopUp(VarMessage varMessage)
    {
        InitializeComponent();
        this.BindingContext = varMessage;
    }

    private void OKButton_Clicked(object sender, EventArgs e)
    {
        Close();
    }
}