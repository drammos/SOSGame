using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using SOS.Box;
using Microsoft.Maui;
using System.Diagnostics;
using SOS.Services;
using SOS.Popups;

namespace SOS.ViewModel
{
    public partial class GridGameViewModel : BaseViewModel
    {
        public ObservableCollection<GridGameBox> GridList { get; set; } = new ObservableCollection<GridGameBox>();

        [ObservableProperty]
        private int _boardSpan;

        [ObservableProperty]
        private int _gridLength;

        readonly IPopupService popupService;

        // Initialize the game Tic-Tac-Toe
        public GridGameViewModel(IPopupService popupService)
        {
            this.PlayerTurn = "X";
            this.popupService = popupService;
            SetUpGame();
        }

        public int RestartBoard()
        {
            SetUpGame();
            return this.BoardSpan;
        }

        public String Winner
        {
            get
            {
                return "The player " + _theWinner + " is Winner!";
            }
        }

        private String _theWinner;
        public String TheWinner
        {
            get { return _theWinner; }
            set { _playerTurn = value; OnPropertyChanged("Winner"); }
        }



        // Screen Player for label
        public string ScreenPlayer
        {
            get
            {
                return "Player " + _playerTurn;
            }
        }


        // The player is 'X' or 'O'
        private string _playerTurn;
        public String PlayerTurn
        {
            get
            {
                return _playerTurn;
            }
            set
            {
                _playerTurn = value;
                OnPropertyChanged("ScreenPlayer");
            }
        }


        // SetUp the Game
        private void SetUpGame()
        {
            this.PlayerTurn = "X";
            GridList.Clear();
            string key = App.User.UserName + "Board";
            BoardSpan = Preferences.Get(key, BoardSpan);
            if (BoardSpan == 4) GridLength = 80;
            else if (BoardSpan == 5) GridLength = 70;
            else if (BoardSpan == 6) GridLength = 60;
            else if (BoardSpan == 7) GridLength = 52;
            else if (BoardSpan == 8) GridLength = 45;

            int board = BoardSpan * BoardSpan;
            for (int i = 0; i < board; i++)
            {
                GridList.Add(new GridGameBox(i));
            }  
        }

        [RelayCommand]
        public void Reset()
        {
            SetUpGame();
        }

        [RelayCommand]
        public async void QuitGame()
        {
            await Shell.Current.GoToAsync($"//{nameof(StartGame)}");
        }

        [RelayCommand]
        public async void SelectedItem(GridGameBox selectedItem)
        {
            if (selectedItem.Player != null)
            {
                return;
            }

            var pop = new BoardPopUp();
            var res = await popupService.ShowPopup<string>(pop);

            //Dispose();

            if (PlayerTurn == "X")
            {
                if(res == "S")
                {
                    selectedItem.SelectedText = "S";
                }
                else if(res == "O")
                {
                    selectedItem.SelectedText = "O";
                }
                selectedItem.Player = 0;
                this.PlayerTurn = "O";
                this._playerTurn = "O";
            }
            else
            {
                if (res == "S")
                {
                    selectedItem.SelectedText = "S";
                }
                else if(res == "O")
                {
                    selectedItem.SelectedText = "O";
                }
                selectedItem.Player = 1;
                this._playerTurn = "X";
                this.PlayerTurn = "X";
            }
        }
    }
}
