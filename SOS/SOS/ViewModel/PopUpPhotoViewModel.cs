using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
