using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

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
