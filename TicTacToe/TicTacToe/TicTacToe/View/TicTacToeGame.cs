using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TicTacToe.Box;

namespace TicTacToe.View
{
    public partial class TicTacToeGame : ObservableObject
    {
        public ObservableCollection<TicTacToeBox> TicTacToeList { get; set; } = new ObservableCollection<TicTacToeBox>();


        public TicTacToeGame()
        {
            SetUpGame();
        }


        private String PlayerTurn
        {
            get;
            set;   
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
            }
            else
            {
                selectedItem.SelectedText = "O";
                selectedItem.Player = 1;
                this.PlayerTurn = "X";
            }

        }
    }
}
