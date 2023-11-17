using SOS.ViewModel;

namespace SOS
{
    public partial class StartGame : ContentPage 
    {
        StartGameViewModel startGameViewModel;
        public StartGame(StartGameViewModel startGameViewModel)
        {
            InitializeComponent();
            this.BindingContext = startGameViewModel;
            this.startGameViewModel = startGameViewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(100);
            imgLoader.IsAnimationPlaying = true;
            if (App.User.Theme == "Dark")
            {
                Application.Current.UserAppTheme = AppTheme.Dark;
            }
            else
            {
                Application.Current.UserAppTheme = AppTheme.Light;
            }

            int board = App.UserSettings.Board;
            string level = App.UserSettings.Level;
            int players = App.UserSettings.Players;

            if (board != 0 && !String.IsNullOrEmpty(level) && players != 0)
            {
                this.startGameViewModel.IsNotCompleteSettingsEnable = false;
                this.startGameViewModel.IsStartGameButtonEnable = true;
            }
            else
            {
                this.startGameViewModel.IsNotCompleteSettingsEnable = true;
            }
        }

        public async void NewGame(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(Game)}");
        }

        public async void GameSettings(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(Settings)}");
        }

    }
}