using CommunityToolkit.Mvvm.ComponentModel;

namespace SOS.ViewModel;

public partial class VarMessage : BaseViewModel
{
    [ObservableProperty]
    private string _textMessage;

    public VarMessage(string message)
    {
        _textMessage = message;
    }
}