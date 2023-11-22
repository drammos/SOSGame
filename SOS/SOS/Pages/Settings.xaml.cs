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

    protected async override void OnDisappearing()
    {
        base.OnDisappearing();
        int board = this.settingsViewModel.GetGridBoard();
        int players = int.Parse(this.settingsViewModel.SelectedPlayersOption);

        await this.settingsViewModel.UpdateUserSettings(
                App.UserSettings.Email,
                board,
                this.settingsViewModel.SelectedLevelOption,
                players
            );
    }

    protected override bool OnBackButtonPressed()
    {
        return false;
    }
    protected async override void OnAppearing()
    {
        base.OnAppearing();

        int boardPrice = App.UserSettings.Board;
        if(boardPrice != 0)
        {
            if (boardPrice == 4) { this.settingsViewModel.SelectedBoardOption = "4x4"; }
            else if (boardPrice == 5) { this.settingsViewModel.SelectedBoardOption = "5x5"; }
            else if (boardPrice == 6) { this.settingsViewModel.SelectedBoardOption = "6x6"; }
            else if (boardPrice == 7) { this.settingsViewModel.SelectedBoardOption = "7x7"; }
            else if (boardPrice == 8) { this.settingsViewModel.SelectedBoardOption = "8x8"; }
        }

        string levelPrice = App.UserSettings.Level;
        if (!string.IsNullOrEmpty(levelPrice))
        {
            this.settingsViewModel.SelectedLevelOption = levelPrice;
        }

        int playersPrice = App.UserSettings.Players;
        if (playersPrice != 0)
        {
            this.settingsViewModel.SelectedPlayersOption = playersPrice.ToString();
        }
    }
}