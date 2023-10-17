using CommunityToolkit.Mvvm.ComponentModel;

namespace SOS.ViewModel
{
    public partial class PopUpPhotoViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string _iconPath;


        public PopUpPhotoViewModel()
        {
            _iconPath = "user.png";
        }
    }
}
