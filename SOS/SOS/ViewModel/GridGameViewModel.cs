using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using SOS.Box;
using Microsoft.Maui;
using System.Diagnostics;

namespace SOS.ViewModel
{
    public partial class GridGameViewModel : BaseViewModel
    {
        public ObservableCollection<GridGameBox> GridList { get; set; } = new ObservableCollection<GridGameBox>();

        [ObservableProperty]
        private int _boardSpan;

        [ObservableProperty]
        private int _gridLength;
        // Initialize the game Tic-Tac-Toe
        public GridGameViewModel()
        {
            this.PlayerTurn = "X";
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
            BoardSpan = Preferences.Get("Board", BoardSpan);
            if (BoardSpan == 4)
            {
            Debug.WriteLine("\n\n\nelaa1");

                GridLength = 80;
            }
            else if (BoardSpan == 5)
            {
                GridLength = 70;
                Debug.WriteLine("\n\n\nelaa2");

            }
            else if (BoardSpan == 6)
            {
                GridLength = 60;
                Debug.WriteLine("elaa3");

            }
            else if (BoardSpan == 7)
            {
                GridLength = 52;
                Debug.WriteLine("elaa4");

            }
            else if (BoardSpan == 8)
            {
                GridLength = 45;
                Debug.WriteLine("elaa5");

            }

            Debug.WriteLine("elaa");
            int board = BoardSpan * BoardSpan;
            for (int i = 0; i < board; i++)
            {
                GridList.Add(new GridGameBox(i));
            }  
        }

        [RelayCommand]
        public void SelectedItem(GridGameBox selectedItem)
        {
            if (selectedItem.Player != null)
            {
                return;
            }

            if (PlayerTurn == "X")
            {
                selectedItem.SelectedText = "X";
                selectedItem.Player = 0;
                this.PlayerTurn = "O";
                this._playerTurn = "O";
            }
            else
            {
                selectedItem.SelectedText = "O";
                selectedItem.Player = 1;
                this._playerTurn = "X";
                this.PlayerTurn = "X";
            }
        }
    }
}
