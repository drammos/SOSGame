using System.Collections.ObjectModel;
using SOS.Box;
using SOS.ViewModel;

namespace SOS
{
    public partial class StartGame : ContentPage 
    {

        public ObservableCollection<GridGameBox> GridGameList { get; set; } = new ObservableCollection<GridGameBox>();
        private GridGameViewModel GridGameInstance { get; set; }
        public int GridPanel { get; set; }


        public StartGame()
        {
            InitializeComponent();
            GridGameInstance = new GridGameViewModel();
            GridPanel = GridGameInstance.BoardSpan;

            this.BindingContext = GridGameInstance;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            GridPanel = GridGameInstance.RestartBoard();

            if(GridPanel == 4)
            {
                
            }
        }
    }
}