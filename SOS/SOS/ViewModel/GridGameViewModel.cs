using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOS.Box;
using SOS;
using System.Diagnostics;

namespace SOS.ViewModel
{
    public partial class GridGameViewModel : ObservableObject
    {
        public ObservableCollection<GridGameBox> GridList { get; set; } = new ObservableCollection<GridGameBox>();
        public int BoardSpan {  get; set; }

        // Initialize the game Tic-Tac-Toe
        public GridGameViewModel()
        {
            this.PlayerTurn = "X";
            SetUpGame();
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
            BoardSpan = App.Board;
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
