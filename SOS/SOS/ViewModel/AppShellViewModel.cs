using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.Diagnostics;

namespace SOS.ViewModel
{
    public partial class AppShellViewModel : BaseViewModel, INotifyPropertyChanged
    {

        [ObservableProperty]
        private string _iconPath;

        public void setTheIcon(string iconPath)
        {
            IconPath = iconPath;
        }

        public AppShellViewModel() 
        {
            _iconPath = "user.png";
        }

        [RelayCommand]
        public async Task SignOut()
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }

    }
}
