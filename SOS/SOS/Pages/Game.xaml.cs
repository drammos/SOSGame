using System.Collections.ObjectModel;
using SOS.Box;
using SOS.ViewModel;

namespace SOS
{
    public partial class Game : ContentPage
    {
        public ObservableCollection<GridGameBox> GridGameList { get; set; } = new ObservableCollection<GridGameBox>();
        private GridGameViewModel GridGameInstance { get; set; }


        public Game(GridGameViewModel gridGameViewModel)
        {
            InitializeComponent();
            this.BindingContext = gridGameViewModel;
            this.GridGameInstance = gridGameViewModel;
        }
 

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            GridGameInstance.RestartBoard();
        }
    }
}