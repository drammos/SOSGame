using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SOS.ViewModel;

namespace SOS;

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