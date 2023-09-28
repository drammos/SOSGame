using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOS.ViewModel
{
    public partial class AppShellViewModel :BaseViewModel
    {
        [RelayCommand]
        public async Task SignOut()
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}
