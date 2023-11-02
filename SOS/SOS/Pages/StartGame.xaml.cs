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

            string keyBoard = App.User.UserName + "Board";
            string keyLevel = App.User.UserName + "Level";
            string keyPlayers = App.User.UserName + "Players";

            int board = -1;
            string level = "";
            int players = -1;

            board = Preferences.Get(keyBoard, board);
            level = Preferences.Get(keyLevel, level);
            players = Preferences.Get(keyPlayers, players);



            if (board != -1 && level != "" && players != -1)
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