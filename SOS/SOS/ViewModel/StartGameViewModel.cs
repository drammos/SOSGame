using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOS.ViewModel
{
    public partial class StartGameViewModel : BaseViewModel
    {
        public StartGameViewModel() { }

        [ObservableProperty]
        private bool _isStartGameButtonEnable;

        [ObservableProperty]
        private bool _isNotCompleteSettingsEnable;

        [RelayCommand]
        public async Task StartGame() 
        {
            await Shell.Current.GoToAsync($"{nameof(Game)}");
        }
    }
}
