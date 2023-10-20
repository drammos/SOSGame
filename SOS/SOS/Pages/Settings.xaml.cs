using SOS.ViewModel;

namespace SOS;


public partial class Settings : ContentPage
{
    SettingsViewModel settingsViewModel;
    public Settings(SettingsViewModel settingsViewModel)
    {
        InitializeComponent();
        this.BindingContext = settingsViewModel;
        this.settingsViewModel = settingsViewModel;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        string board = App.User.UserName + "Board";
        string level = App.User.UserName + "Level";
        string players = App.User.UserName + "Players";
        
        
        int boardPrice = -1;
        boardPrice = Preferences.Get(board, boardPrice);
        if(boardPrice != -1)
        {
            if (boardPrice == 4) { this.settingsViewModel.SelectedBoardOption = "4x4"; }
            else if (boardPrice == 5) { this.settingsViewModel.SelectedBoardOption = "5x5"; }
            else if (boardPrice == 6) { this.settingsViewModel.SelectedBoardOption = "6x6"; }
            else if (boardPrice == 7) { this.settingsViewModel.SelectedBoardOption = "7x7"; }
            else if (boardPrice == 8) { this.settingsViewModel.SelectedBoardOption = "8x8"; }
        }

        string levelPrice = "";
        levelPrice = Preferences.Get(level, levelPrice);
        if (levelPrice!="")
        {
            this.settingsViewModel.SelectedLevelOption = levelPrice;
        }

        int playersPrice = -1;
        playersPrice = Preferences.Get(players, playersPrice);
        if (playersPrice != -1)
        {
            this.settingsViewModel.SelectedPlayersOption = playersPrice.ToString();
        }
    }
}