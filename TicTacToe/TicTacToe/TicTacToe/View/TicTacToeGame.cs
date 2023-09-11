using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using TicTacToe.Box;

namespace TicTacToe.View
{
    public partial class TicTacToeGame : ObservableObject
    {
        public ObservableCollection<TicTacToeBox> TicTacToeList { get; set; } = new ObservableCollection<TicTacToeBox>();


        public TicTacToeGame()
        {
            this.PlayerTurn = "X";
            SetUpGame();
        }


        public string ScreenPlayer
        {
            get
            {
                return "Player " + _playerTurn;
            }
        }

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

        private void SetUpGame()
        {
            this.PlayerTurn = "X";
            TicTacToeList.Clear();
            for(int i = 0; i<9; i++)
            {
                TicTacToeList.Add(new TicTacToeBox(i));
            }
        }

        [RelayCommand]
        public void SelectedItem(TicTacToeBox selectedItem)
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


            CheckStateForGame();
        }


        public void CheckStateForGame()
        {
            // Check for X player
            for(int i = 0; i<9;i++)
            {

            }

            // Check for O player
        }
    }



}
