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

        private int completeCount;

        [ObservableProperty]
        private int _boardSpan;

        [ObservableProperty]
        private int _gridLength;

        [ObservableProperty]
        private string _userName;

        [ObservableProperty]
        private int _userScore;

        [ObservableProperty]
        private int _computerScore;

        [ObservableProperty]
        private string _filePath;

        [ObservableProperty]
        private bool _isUser;

        [ObservableProperty]
        private bool _isComputer;

        private CancellationTokenSource cancellationTokenSource;


        readonly IPopupService popupService;

        // Initialize the game Tic-Tac-Toe
        public GridGameViewModel(IPopupService popupService)
        {
            this.PlayerTurn = "user";
            this.popupService = popupService;
            cancellationTokenSource = new CancellationTokenSource();
            SetUpGame();
            var task = this.StartAsync();
        }

        public async Task StartAsync()
        {
            while (!cancellationTokenSource.Token.IsCancellationRequested)
            {
                await Task.Delay(500);

                // Εδώ μπορείτε να τρέξετε τον κώδικα που θέλετε να εκτελείται σε ατέρμονα βρόχο.
                bool result;
                if (this.IsComputer)
                {
                    if (completeCount < BoardSpan * BoardSpan)
                    {

                        result = this.SelectPC();
                        if (result==false)
                        {
                            this.PlayerTurn = "user";
                            this._playerTurn = "user";
                            this.IsUser = true;
                            this.IsComputer = false;
                        }
                        else 
                        {
                            this.ComputerScore += 10;
                        }
                    }   
                }
            }
        }

        public int RestartBoard()
        {
            SetUpGame();
            UserName = App.User.UserName;
            UserScore = App.User.Score;
            FilePath = App.User.FilePath;
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
            this.PlayerTurn = "user";
            this.IsUser = true;
            this.IsComputer = false;
            _userName = App.User.UserName;
            _userScore = App.User.Score;
            _filePath = App.User.FilePath;
            _computerScore = 0;
            if(string.IsNullOrEmpty(_filePath) )
            {
                _filePath = "user.png";
            }
            GridList.Clear();
            string key = App.User.UserName + "Board";
            BoardSpan = Preferences.Get(key, BoardSpan);
            if (BoardSpan == 4) GridLength = 80;
            else if (BoardSpan == 5) GridLength = 70;
            else if (BoardSpan == 6) GridLength = 60;
            else if (BoardSpan == 7) GridLength = 52;
            else if (BoardSpan == 8) GridLength = 45;

            completeCount = 0;
            int board = BoardSpan * BoardSpan;
            for (int i = 0; i < board; i++)
            {
                GridList.Add(new GridGameBox(i));
            }  
        }

        [RelayCommand]
        public void Reset()
        {
            UserName = App.User.UserName;
            UserScore = 0;
            ComputerScore = 0;
            FilePath = App.User.FilePath;
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

            if (PlayerTurn == "user")
            {
                if (res == "S")
                {
                    selectedItem.SetText("S", 0);
                    if (checkForSOS(selectedItem) == false)
                    {
                        this.PlayerTurn = "pc";
                        this._playerTurn = "pc";
                        this.IsUser = false;
                        this.IsComputer = true;
                    }
                    else
                    {
                        this.UserScore += 10;
                    }
                    completeCount++;
                }
                else if (res == "O")
                {
                    selectedItem.SetText("O", 0);
                    if (checkForSOS(selectedItem) == false)
                    {
                        this.PlayerTurn = "pc";
                        this._playerTurn = "pc";
                        this.IsUser = false;
                        this.IsComputer = true;
                    }
                    else
                    {
                        this.UserScore += 10;
                    }
                    completeCount++;
                }

                if(completeCount == BoardSpan*BoardSpan) 
                {
                    if(this.)
                    var mes = new VarMessage("C");
                    var pop = new PopUp(mes);
                    popupService.ShowPopup(pop);
                }
            }
        }

        public bool checkForSOS(GridGameBox box)
        {
            int num, num1;
            int board = BoardSpan;
            int size = board * board;
            int randomNumber = box.Index;


            if (box.SelectedText == "S")
            {
                //1st line
                if (randomNumber % board == 0)
                {
                    num = randomNumber - 2 * board + 2;
                    if (num >= 0 && num < size)
                    {
                        GridGameBox box2 = GridList[num];

                        if (box2.SelectedText == "S")
                        {
                            num1 = randomNumber - board + 1;
                            GridGameBox box3 = GridList[num1];
                            if (box3.SelectedText == "O")
                            {
                                box.SetDisableColors();
                                box2.SetDisableColors();
                                box3.SetDisableColors();
                                return true;
                            }

                        }
                    }
                    num = randomNumber + 2 * board + 2;
                    if (num >= 0 && num < size)
                    {
                        GridGameBox box2 = GridList[num];
                        if (box2.SelectedText == "S")
                        {
                            num1 = randomNumber + board + 1;
                            GridGameBox box3 = GridList[num1];
                            if (box3.SelectedText == "O")
                            {
                                box.SetDisableColors();
                                box2.SetDisableColors();
                                box3.SetDisableColors();
                                return true;
                            }
                        }
                    }

                    num = randomNumber + 2;
                    if (num >= 0 && num < size)
                    {
                        GridGameBox box2 = GridList[num];
                        if (box2.SelectedText == "S")
                        {
                            num1 = randomNumber + 1;
                            GridGameBox box3 = GridList[num1];
                            if (box3.SelectedText == "O")
                            {
                                box.SetDisableColors();
                                box2.SetDisableColors();
                                box3.SetDisableColors();
                                return true;
                            }
                        }
                    }
                }
                //last line
                else if ((randomNumber + 1) % board == 0)
                {
                    num = randomNumber - 2 * board - 2;
                    if (num >= 0 && num < size)
                    {
                        GridGameBox box2 = GridList[num];
                        if (box2.SelectedText == "S")
                        {
                            num1 = randomNumber - board - 1;
                            GridGameBox box3 = GridList[num1];
                            if (box3.SelectedText == "O")
                            {
                                box.SetDisableColors();
                                box2.SetDisableColors();
                                box3.SetDisableColors();
                                return true;
                            }
                        }
                    }
                    num = randomNumber + 2 * board - 2;
                    if (num >= 0 && num < size)
                    {
                        GridGameBox box2 = GridList[num];
                        if (box2.SelectedText == "S")
                        {
                            num1 = randomNumber + board - 1;
                            GridGameBox box3 = GridList[num1];
                            if (box3.SelectedText == "O")
                            {
                                box.SetDisableColors();
                                box2.SetDisableColors();
                                box3.SetDisableColors();
                                return true;
                            }
                        }
                    }

                    num = randomNumber - 2;
                    if (num >= 0 && num < size)
                    {
                        GridGameBox box2 = GridList[num];
                        if (box2.SelectedText == "S")
                        {
                            num1 = randomNumber - 1;
                            GridGameBox box3 = GridList[num1];
                            if (box3.SelectedText == "O")
                            {
                                box.SetDisableColors();
                                box2.SetDisableColors();
                                box3.SetDisableColors();
                                return true;
                            }
                        }
                    }

                }
                else
                {
                    num = randomNumber - 2 * board + 2;
                    if (num >= 0 && num < size)
                    {
                        GridGameBox box2 = GridList[num];
                        if (box2.SelectedText == "S")
                        {
                            num1 = randomNumber - board + 1;
                            GridGameBox box3 = GridList[num1];
                            if (box3.SelectedText == "O")
                            {
                                box.SetDisableColors();
                                box2.SetDisableColors();
                                box3.SetDisableColors();
                                return true;
                            }
                        }
                    }
                    num = randomNumber + 2 * board - 2;
                    if (num >= 0 && num < size)
                    {
                        GridGameBox box2 = GridList[num];
                        if (box2.SelectedText == "S")
                        {
                            num1 = randomNumber + board - 1;
                            GridGameBox box3 = GridList[num1];
                            if (box3.SelectedText == "O")
                            {
                                box.SetDisableColors();
                                box2.SetDisableColors();
                                box3.SetDisableColors();
                                return true;
                            }
                        }
                    }


                    /////////////////
                    num = randomNumber - 2 * board + 2;
                    if (num >= 0 && num < size)
                    {
                        GridGameBox box2 = GridList[num];
                        if (box2.SelectedText == "S")
                        {
                            num1 = randomNumber - board + 1;
                            GridGameBox box3 = GridList[num1];
                            if (box3.SelectedText == "O")
                            {
                                box.SetDisableColors();
                                box2.SetDisableColors();
                                box3.SetDisableColors();
                                return true;
                            }
                        }
                    }
                    num = randomNumber + 2 * board + 2;
                    if (num >= 0 && num < size)
                    {
                        GridGameBox box2 = GridList[num];
                        if (box2.SelectedText == "S")
                        {
                            num1 = randomNumber + board + 1;
                            GridGameBox box3 = GridList[num1];
                            if (box3.SelectedText == "O")
                            {
                                box.SetDisableColors();
                                box2.SetDisableColors();
                                box3.SetDisableColors();
                                return true;
                            }
                        }
                    }

                    
                    if((randomNumber -1)%board != 0)
                    {
                        num = randomNumber - 2;
                        if (num >= 0 && num < size)
                        {
                            GridGameBox box2 = GridList[num];
                            if (box2.SelectedText == "S")
                            {
                                num1 = randomNumber - 1;
                                GridGameBox box3 = GridList[num1];
                                if (box3.SelectedText == "O")
                                {
                                    box.SetDisableColors();
                                    box2.SetDisableColors();
                                    box3.SetDisableColors();
                                    return true;
                                }
                            }
                        }
                    }
                    if ((randomNumber + 1) % board != 0)
                    {
                        num = randomNumber + 2;
                        if (num >= 0 && num < size)
                        {
                            GridGameBox box2 = GridList[num];
                            if (box2.SelectedText == "S")
                            {
                                num1 = randomNumber + 1;
                                GridGameBox box3 = GridList[num1];
                                if (box3.SelectedText == "O")
                                {
                                    box.SetDisableColors();
                                    box2.SetDisableColors();
                                    box3.SetDisableColors();
                                    return true;
                                }
                            }
                        }
                    }
                }

                num = randomNumber - 2 * board;
                if (num >= 0 && num < size)
                {
                    GridGameBox box2 = GridList[num];
                    if (box2.SelectedText == "S")
                    {
                        num1 = randomNumber - board;
                        GridGameBox box3 = GridList[num1];
                        if (box3.SelectedText == "O")
                        {
                            box.SetDisableColors();
                            box2.SetDisableColors();
                            box3.SetDisableColors();
                            return true;
                        }
                    }
                }
                

                num = randomNumber + 2 * board;
                if (num >= 0 && num < size)
                {
                    GridGameBox box2 = GridList[num];
                    if (box2.SelectedText == "S")
                    {
                        num1 = randomNumber + board;
                        GridGameBox box3 = GridList[num1];
                        if (box3.SelectedText == "O")
                        {
                            box.SetDisableColors();
                            box2.SetDisableColors();
                            box3.SetDisableColors();
                            return true;
                        }
                    }
                }
            }
            else if(box.SelectedText == "O")
            {
                if (randomNumber % board == 0)
                {
                    //tha koitakso mono deksia aristera kai katw
                    num = randomNumber - board;
                    if(num >= 0 && num < size)
                    {
                        GridGameBox box2 = GridList[num];
                        if(box2.SelectedText == "S")
                        {
                            num1 = randomNumber + board;
                            if(num1 >= 0 && num1 < size)
                            {
                                GridGameBox box3 = GridList[num1];
                                if (box3.SelectedText == "S")
                                {
                                    box.SetDisableColors();
                                    box2.SetDisableColors();
                                    box3.SetDisableColors();
                                    return true;
                                }
                            }
                        }
                    }

                }
                else if ((randomNumber + 1) % board == 0)
                {
                    // tha koitaks deksia aristera kai panw
                    num = randomNumber - board;
                    if (num >= 0 && num < size)
                    {
                        GridGameBox box2 = GridList[num];
                        if (box2.SelectedText == "S")
                        {
                            num1 = randomNumber + board;
                            if (num1 >= 0 && num1 < size)
                            {
                                GridGameBox box3 = GridList[num1];
                                if (box3.SelectedText == "S")
                                {
                                    box.SetDisableColors();
                                    box2.SetDisableColors();
                                    box3.SetDisableColors();
                                    return true;
                                }
                            }
                        }
                    }
                }
                else
                {
                    // tha koitakso deksio deksi aristera panw kai katw kai diagwnia

                    //deksia aristera
                    num = randomNumber - board;
                    if (num >= 0 && num < size)
                    {
                        GridGameBox box2 = GridList[num];
                        if (box2.SelectedText == "S")
                        {
                            num1 = randomNumber + board;
                            if (num1 >= 0 && num1 < size)
                            {
                                GridGameBox box3 = GridList[num1];
                                if (box3.SelectedText == "S")
                                {
                                    box.SetDisableColors();
                                    box2.SetDisableColors();
                                    box3.SetDisableColors();
                                    return true;
                                }
                            }
                        }
                    }

                    //panw katw
                    num = randomNumber - 1;
                    if (num >= 0 && num < size)
                    {
                        GridGameBox box2 = GridList[num];
                        if (box2.SelectedText == "S")
                        {
                            num1 = randomNumber + 1;
                            if (num1 >= 0 && num1 < size)
                            {
                                GridGameBox box3 = GridList[num1];
                                if (box3.SelectedText == "S")
                                {
                                    box.SetDisableColors();
                                    box2.SetDisableColors();
                                    box3.SetDisableColors();
                                    return true;
                                }
                            }
                        }
                    }

                    //deksia diagwnios
                    num = randomNumber - board - 1;
                    if (num >= 0 && num < size)
                    {
                        GridGameBox box2 = GridList[num];
                        if (box2.SelectedText == "S")
                        {
                            num1 = randomNumber + board + 1;
                            if (num1 >= 0 && num1 < size)
                            {
                                GridGameBox box3 = GridList[num1];
                                if (box3.SelectedText == "S")
                                {
                                    box.SetDisableColors();
                                    box2.SetDisableColors();
                                    box3.SetDisableColors();
                                    return true;
                                }
                            }
                        }
                    }

                    //aristera diagwnios
                    num = randomNumber - board + 1;
                    if (num >= 0 && num < size)
                    {
                        GridGameBox box2 = GridList[num];
                        if (box2.SelectedText == "S")
                        {
                            num1 = randomNumber + board - 1;
                            if (num1 >= 0 && num1 < size)
                            {
                                GridGameBox box3 = GridList[num1];
                                if (box3.SelectedText == "S")
                                {
                                    box.SetDisableColors();
                                    box2.SetDisableColors();
                                    box3.SetDisableColors();
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        public bool SelectPC()
        {
            Thread.Sleep(1000);

            int board = BoardSpan;

            // σε γραμμες παμε
            int box;
            for (int i = 0; i < board; i++)
            {
                box = i;
                for(int j = 0; j < board; j++)
                {
                    GridGameBox boxPrice = GridList[box];
                    // ειμαστε στην 1η γραμμη
                    if (i == 0)
                    {
                        if (boxPrice.SelectedText == "S")
                        {
                            //koitame orizontia
                            if (j + 1 < board - 1)
                            {
                                GridGameBox boxPriceNext = GridList[box + board];
                                GridGameBox boxPriceDoubleNext = GridList[box + 2*board];
                                if (boxPriceNext.SelectedText == "O" && string.IsNullOrEmpty(boxPriceDoubleNext.SelectedText))
                                {
                                    boxPriceDoubleNext.SetText("S", 1);
                                    boxPriceDoubleNext.SetDisableColors();
                                    GridList[box + 2*board] = boxPriceDoubleNext;
                                    boxPriceNext.SetDisableColors();
                                    boxPrice.SetDisableColors();
                                    completeCount++;
                                    return true;
                                }
                                else if (string.IsNullOrEmpty(boxPriceNext.SelectedText) && boxPriceDoubleNext.SelectedText == "S")
                                {
                                    boxPriceNext.SetText("O", 1);
                                    boxPriceNext.SetDisableColors();
                                    GridList[box + board] = boxPriceNext;
                                    boxPrice.SetDisableColors();
                                    boxPriceDoubleNext.SetDisableColors();
                                    completeCount++;
                                    return true;
                                }
                            }

                            //koitame katheta
                            GridGameBox boxDownNext = GridList[box + 1];
                            GridGameBox boxDownDoubleNext = GridList[box + 2];
                            if (boxDownNext.SelectedText == "O" && string.IsNullOrEmpty(boxDownDoubleNext.SelectedText))
                            {
                                boxDownDoubleNext.SetText("S", 1);
                                boxDownDoubleNext.SetDisableColors();
                                boxDownNext.SetDisableColors();
                                boxPrice.SetDisableColors();
                                GridList[box + 2] = boxDownDoubleNext;
                                completeCount++;
                                return true;
                            }
                            else if (string.IsNullOrEmpty(boxDownNext.SelectedText) && boxDownDoubleNext.SelectedText == "S")
                            {
                                boxDownNext.SetText("O", 1);
                                boxPrice.SetDisableColors();
                                boxDownDoubleNext.SetDisableColors();
                                boxDownNext.SetDisableColors();
                                GridList[box + 1] = boxDownNext;
                                completeCount++;
                                return true;
                            }

                            //koitame diagwnia

                            //pros deksia katw-diagwnia
                            if (j + 1 < board - 1)
                            {
                                GridGameBox boxDowndiagonalsNext = GridList[box + board + 1];
                                GridGameBox boxDowndiagonalsDoubleNext = GridList[box + 2 * board + 2];

                                if (boxDowndiagonalsNext.SelectedText == "O" && string.IsNullOrEmpty(boxDowndiagonalsDoubleNext.SelectedText))
                                {
                                    boxDowndiagonalsDoubleNext.SetText("S", 1);
                                    boxDowndiagonalsDoubleNext.SetDisableColors();
                                    boxPrice.SetDisableColors();
                                    boxDowndiagonalsNext.SetDisableColors();
                                    GridList[box + 2 * board + 2] = boxDowndiagonalsDoubleNext;
                                    completeCount++;
                                    return true;
                                }
                                else if (string.IsNullOrEmpty(boxDowndiagonalsNext.SelectedText) && boxDowndiagonalsDoubleNext.SelectedText == "S")
                                {
                                    boxDowndiagonalsNext.SetText("O", 1);
                                    boxDowndiagonalsNext.SetDisableColors();
                                    boxPrice.SetDisableColors();
                                    boxDowndiagonalsDoubleNext.SetDisableColors();
                                    GridList[box + board + 1] = boxDowndiagonalsNext;
                                    completeCount++;
                                    return true;
                                }
                            }

                            //pros aristera katw-diagwnia
                            if (j - 1 > 0)
                            {
                                GridGameBox boxDownDiagonalsPrevious = GridList[box - board + 1];
                                GridGameBox boxDownDiagonalsDoublePrevious = GridList[box - 2 * board + 2];
                                if (boxDownDiagonalsPrevious.SelectedText == "O" && string.IsNullOrEmpty(boxDownDiagonalsDoublePrevious.SelectedText))
                                {
                                    boxDownDiagonalsDoublePrevious.SetText("S", 1);
                                    boxDownDiagonalsDoublePrevious.SetDisableColors();
                                    boxPrice.SetDisableColors();
                                    boxDownDiagonalsPrevious.SetDisableColors();
                                    GridList[box - 2 * board + 2] = boxDownDiagonalsDoublePrevious;
                                    completeCount++;
                                    return true;
                                }
                                else if (string.IsNullOrEmpty(boxDownDiagonalsPrevious.SelectedText) && boxDownDiagonalsDoublePrevious.SelectedText == "S")
                                {
                                    boxDownDiagonalsPrevious.SetText("O", 1);
                                    boxDownDiagonalsPrevious.SetDisableColors();
                                    boxPrice.SetDisableColors();
                                    boxDownDiagonalsDoublePrevious.SetDisableColors();
                                    GridList[box - board + 1] = boxDownDiagonalsPrevious;
                                    completeCount++;
                                    return true;
                                }
                            }
                        }
                        else if (string.IsNullOrEmpty(boxPrice.SelectedText))
                        {
                            // koitame orizontia
                            if (j + 1 < board - 1)
                            {
                                GridGameBox boxPriceNext = GridList[box + board];
                                GridGameBox boxPriceDoubleNext = GridList[box + 2 * board];
                                if (boxPriceNext.SelectedText == "O" && boxPriceDoubleNext.SelectedText == "S")
                                {
                                    boxPrice.SetText("S", 1);
                                    boxPrice.SetDisableColors();
                                    boxPriceNext.SetDisableColors();
                                    boxPriceDoubleNext.SetDisableColors();
                                    GridList[box] = boxPrice;
                                    completeCount++;
                                    return true;
                                }
                            }

                            //koitame katheta
                            GridGameBox boxDownNext = GridList[box + 1];
                            GridGameBox boxDownDoubleNext = GridList[box + 2];
                            if (boxDownNext.SelectedText == "O" && boxDownDoubleNext.SelectedText == "S")
                            {
                                boxPrice.SetText("S", 1);
                                boxPrice.SetDisableColors();
                                boxDownNext.SetDisableColors(); 
                                boxDownDoubleNext.SetDisableColors();
                                GridList[box] = boxPrice;
                                completeCount++;
                                return true;
                            }

                            //koitame diagwnia
                            //pros deksia katw-diagwnia
                            if (j + 1 < board - 1)
                            {
                                GridGameBox boxDowndiagonalsNext = GridList[box + board + 1];
                                GridGameBox boxDowndiagonalsDoubleNext = GridList[box + 2 * board + 2];

                                if (boxDowndiagonalsNext.SelectedText == "O" && boxDowndiagonalsDoubleNext.SelectedText == "S")
                                {
                                    boxPrice.SetText("S", 1);
                                    boxPrice.SetDisableColors();
                                    boxDowndiagonalsNext.SetDisableColors();
                                    boxDowndiagonalsDoubleNext.SetDisableColors();
                                    GridList[box] = boxPrice;
                                    completeCount++;
                                    return true;
                                }
                            }

                            //pros aristera katw-diagwnia
                            if (j - 1 > 0)
                            {
                                GridGameBox boxDownDiagonalsPrevious = GridList[box - board + 1];
                                GridGameBox boxDownDiagonalsDoublePrevious = GridList[box - 2 * board + 2];
                                if (boxDownDiagonalsPrevious.SelectedText == "O" && boxDownDiagonalsDoublePrevious.SelectedText == "S")
                                {
                                    boxPrice.SetText("S", 1);
                                    boxPrice.SetDisableColors();
                                    boxDownDiagonalsDoublePrevious.SetDisableColors();
                                    boxDownDiagonalsPrevious.SetDisableColors();
                                    GridList[box] = boxPrice;
                                    completeCount++;
                                    return true;
                                }
                            }
                        }
                    }
                    // ειμαστε στην τελευατια γραμμη
                    else if (i == board - 1)
                    {
                        if (boxPrice.SelectedText == "S")
                        {
                            //koitame orizontia
                            if (j + 1 < board - 1)
                            {
                                GridGameBox boxPriceNext = GridList[box + board];
                                GridGameBox boxPriceDoubleNext = GridList[box + 2*board];
                                if (boxPriceNext.SelectedText == "O" && string.IsNullOrEmpty(boxPriceDoubleNext.SelectedText))
                                {
                                    boxPriceDoubleNext.SetText("S",1);
                                    boxPriceDoubleNext.SetDisableColors();
                                    boxPriceNext.SetDisableColors();
                                    boxPrice.SetDisableColors();
                                    GridList[box + 2 * board] = boxPriceDoubleNext;
                                    completeCount++;
                                    return true;
                                }
                                else if (string.IsNullOrEmpty(boxPriceNext.SelectedText) && boxPriceDoubleNext.SelectedText == "S")
                                {
                                    boxPriceNext.SetText("O", 1);
                                    boxPriceNext.SetDisableColors();
                                    boxPrice.SetDisableColors();
                                    boxPriceDoubleNext.SetDisableColors();
                                    GridList[box + board] = boxPriceNext;
                                    completeCount++;
                                    return true;
                                }
                            }

                        }
                        else if (string.IsNullOrEmpty(boxPrice.SelectedText))
                        {
                            // koitame orizontia
                            if (j + 1 < board - 1)
                            {
                                GridGameBox boxPriceNext = GridList[box + board];
                                GridGameBox boxPriceDoubleNext = GridList[box + 2 * board];
                                if (boxPriceNext.SelectedText == "O" && boxPriceDoubleNext.SelectedText == "S")
                                {
                                    boxPrice.SetText("S", 1);
                                    boxPrice.SetDisableColors();
                                    boxPriceDoubleNext.SetDisableColors();
                                    boxPriceNext.SetDisableColors();
                                    GridList[box] = boxPrice;
                                    completeCount++;
                                    return true;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (boxPrice.SelectedText == "S")
                        {
                            //koitame orizontia
                            if (j + 1 < board - 1)
                            {
                                GridGameBox boxPriceNext = GridList[box + board];
                                GridGameBox boxPriceDoubleNext = GridList[box + 2 * board];
                                if (boxPriceNext.SelectedText == "O" && string.IsNullOrEmpty(boxPriceDoubleNext.SelectedText))
                                {
                                    boxPriceDoubleNext.SetText("S", 1);
                                    boxPriceDoubleNext.SetDisableColors();
                                    boxPrice.SetDisableColors();
                                    boxPriceNext.SetDisableColors();
                                    GridList[box+2 * board] = boxPriceDoubleNext;
                                    completeCount++;
                                    return true;
                                }
                                else if (string.IsNullOrEmpty(boxPriceNext.SelectedText) && boxPriceDoubleNext.SelectedText == "S")
                                {
                                    boxPriceNext.SetText("O", 1);
                                    boxPriceNext.SetDisableColors();
                                    boxPriceDoubleNext.SetDisableColors();
                                    boxPrice.SetDisableColors();
                                    GridList[box+board] = boxPriceNext;
                                    completeCount++;
                                    return true;
                                }
                            }

                            //koitame katheta

                            if (i + 1 < board - 1)
                            {
                                GridGameBox boxDownNext = GridList[box + 1];
                                GridGameBox boxDownDoubleNext = GridList[box + 2];
                                if (boxDownNext.SelectedText == "O" && string.IsNullOrEmpty(boxDownDoubleNext.SelectedText))
                                {
                                    boxDownDoubleNext.SetText("S", 1);
                                    boxDownDoubleNext.SetDisableColors();
                                    boxPrice.SetDisableColors();
                                    boxDownNext.SetDisableColors();
                                    GridList[box + 2] = boxDownDoubleNext;
                                    completeCount++;
                                    return true;
                                }
                                else if (string.IsNullOrEmpty(boxDownNext.SelectedText) && boxDownDoubleNext.SelectedText == "S")
                                {
                                    boxDownNext.SetText("O", 1);
                                    boxDownNext.SetDisableColors();
                                    boxPrice.SetDisableColors();
                                    boxDownDoubleNext.SetDisableColors();
                                    GridList[box + 1] = boxDownNext;
                                    completeCount++;
                                    return true;
                                }
                            }

                            //koitame diagwnia
                            if (i + 1 < board - 1)
                            {
                                //pros deksia katw-diagwnia
                                if (j + 1 < board - 1)
                                {
                                    GridGameBox boxDowndiagonalsNext = GridList[box + board + 1];
                                    GridGameBox boxDowndiagonalsDoubleNext = GridList[box + 2 * board + 2];

                                    if (boxDowndiagonalsNext.SelectedText == "O" && string.IsNullOrEmpty(boxDowndiagonalsDoubleNext.SelectedText))
                                    {
                                        boxDowndiagonalsDoubleNext.SetText("S", 1);
                                        boxDowndiagonalsDoubleNext.SetDisableColors();
                                        boxPrice.SetDisableColors();
                                        boxDowndiagonalsNext.SetDisableColors();
                                        GridList[box + 2*board + 2] = boxDowndiagonalsDoubleNext;
                                        completeCount++;
                                        return true;
                                    }
                                    else if (string.IsNullOrEmpty(boxDowndiagonalsNext.SelectedText) && boxDowndiagonalsDoubleNext.SelectedText == "S")
                                    {
                                        boxDowndiagonalsNext.SetText("O", 1);
                                        boxDowndiagonalsNext.SetDisableColors();
                                        boxDowndiagonalsDoubleNext.SetDisableColors();
                                        boxPrice.SetDisableColors();
                                        GridList[box + board + 1] = boxDowndiagonalsNext;
                                        completeCount++;
                                        return true;
                                    }
                                }

                                //pros aristera katw-diagwnia
                                if (j - 1 > 0)
                                {
                                    GridGameBox boxDownDiagonalsPrevious = GridList[box - board + 1];
                                    GridGameBox boxDownDiagonalsDoublePrevious = GridList[box - 2 * board + 2];
                                    if (boxDownDiagonalsPrevious.SelectedText == "O" && string.IsNullOrEmpty(boxDownDiagonalsDoublePrevious.SelectedText))
                                    {
                                        boxDownDiagonalsDoublePrevious.SetText("S", 1);
                                        boxDownDiagonalsDoublePrevious.SetDisableColors();
                                        boxPrice.SetDisableColors();
                                        boxDownDiagonalsPrevious.SetDisableColors();
                                        GridList[box - 2 * board + 2] = boxDownDiagonalsDoublePrevious;
                                        completeCount++;
                                        return true;
                                    }
                                    else if (string.IsNullOrEmpty(boxDownDiagonalsPrevious.SelectedText) && boxDownDiagonalsDoublePrevious.SelectedText == "S")
                                    {
                                        boxDownDiagonalsPrevious.SetText("O", 1);
                                        boxDownDiagonalsPrevious.SetDisableColors();
                                        boxPrice.SetDisableColors();
                                        boxDownDiagonalsDoublePrevious.SetDisableColors();
                                        GridList[box - board + 1] = boxDownDiagonalsPrevious;
                                        completeCount++;
                                        return true;
                                    }
                                }
                            }

                        }
                        else if (string.IsNullOrEmpty(boxPrice.SelectedText))
                        {
                            // koitame orizontia
                            if (j + 1 < board - 1)
                            {
                                GridGameBox boxPriceNext = GridList[box + board];
                                GridGameBox boxPriceDoubleNext = GridList[box + 2*board];
                                if (boxPriceNext.SelectedText == "O" && boxPriceDoubleNext.SelectedText == "S")
                                {
                                    boxPrice.SetText("S", 1);
                                    boxPrice.SetDisableColors();
                                    boxPriceNext.SetDisableColors();
                                    boxPriceDoubleNext.SetDisableColors();
                                    GridList[box] = boxPrice;
                                    completeCount++;
                                    return true;
                                }
                            }

                            if(i + 1 < board - 1)
                            {
                                //koitame katheta
                                GridGameBox boxDownNext = GridList[box + 1];
                                GridGameBox boxDownDoubleNext = GridList[box + 2];
                                if (boxDownNext.SelectedText == "O" && boxDownDoubleNext.SelectedText == "S")
                                {
                                    boxPrice.SetText("S", 1);
                                    boxPrice.SetDisableColors();
                                    boxDownNext.SetDisableColors();
                                    boxDownDoubleNext.SetDisableColors();
                                    GridList[box] = boxPrice;
                                    completeCount++;
                                    return true;
                                }
                            }


                            //koitame diagwnia
                            if (i + 1 < board - 1)
                            {
                                //pros deksia katw-diagwnia
                                if (j + 1 < board - 1)
                                {
                                    GridGameBox boxDowndiagonalsNext = GridList[box + board + 1];
                                    GridGameBox boxDowndiagonalsDoubleNext = GridList[box + 2 * board + 2];

                                    if (boxDowndiagonalsNext.SelectedText == "O" && boxDowndiagonalsDoubleNext.SelectedText == "S")
                                    {
                                        boxPrice.SetText("S", 1);
                                        boxPrice.SetDisableColors();
                                        boxDowndiagonalsNext.SetDisableColors();
                                        boxDowndiagonalsDoubleNext.SetDisableColors();
                                        GridList[box] = boxPrice;
                                        completeCount++;
                                        return true;
                                    }
                                }

                                //pros aristera katw-diagwnia
                                if (j - 1 > 0)
                                {
                                    GridGameBox boxDownDiagonalsPrevious = GridList[box - board + 1];
                                    GridGameBox boxDownDiagonalsDoublePrevious = GridList[box - 2 * board + 2];
                                    if (boxDownDiagonalsPrevious.SelectedText == "O" && boxDownDiagonalsDoublePrevious.SelectedText == "S")
                                    {
                                        boxPrice.SetText("S", 1);
                                        boxPrice.SetDisableColors();
                                        boxDownDiagonalsPrevious.SetDisableColors();
                                        boxDownDiagonalsDoublePrevious.SetDisableColors();
                                        GridList[box] = boxPrice;
                                        completeCount++;
                                        return true;
                                    }
                                }
                            }
                        }

                    }

                    box += board;
                }
            }

            int seed = (int)DateTime.Now.Ticks;
            Random random = new Random(seed);
            //allios dialego random
            int size = BoardSpan * BoardSpan;
            while (true)
            {
                int number = random.Next();
                int randomNumber = number % size;
                GridGameBox randomBox = GridList[randomNumber];
                if (string.IsNullOrEmpty(randomBox.SelectedText))
                {
                    //edo prepei na checkaro ta guro guro
                    // idia grammi i=0 tha einai (-2*board-2), (+2*board - 2),-2*board, -2 ,2, 2*board, (-2*board+2), (+2*board+2)
                    int num, num1;
                    //1st line
                    if ( randomNumber%board == 0)
                    {
                        num = randomNumber - 2 * board + 2;
                        if (num >= 0 && num < size)
                        {
                            GridGameBox box2 = GridList[num];
                            if (box2.SelectedText == "S")
                            {
                                num1 = randomNumber - board + 1;
                                GridGameBox box3 = GridList[num1];
                                if (string.IsNullOrEmpty(box3.SelectedText))
                                {
                                    continue;
                                }
                            }
                        }
                        num = randomNumber + 2 * board + 2;
                        if (num >= 0 && num < size)
                        {
                            GridGameBox box2 = GridList[num];
                            if (box2.SelectedText == "S")
                            {
                                num1 = randomNumber + board + 1;
                                GridGameBox box3 = GridList[num1];
                                if (string.IsNullOrEmpty(box3.SelectedText))
                                {
                                    continue;
                                }
                            }
                        }
                    }
                    //last line
                    else if((randomNumber+1)%board == 0) 
                    {
                        num = randomNumber - 2 * board - 2;
                        if (num >= 0 && num < size)
                        {
                            GridGameBox box2 = GridList[num];
                            if (box2.SelectedText == "S")
                            {
                                num1 = randomNumber -board - 1;
                                GridGameBox box3 = GridList[num1];
                                if (string.IsNullOrEmpty(box3.SelectedText))
                                {
                                    continue;
                                }
                            }
                        }
                        num = randomNumber + 2 * board - 2;
                        if (num >= 0 && num < size)
                        {
                            GridGameBox box2 = GridList[num];
                            if (box2.SelectedText == "S")
                            {
                                num1 = randomNumber + board - 1;
                                GridGameBox box3 = GridList[num1];
                                if (string.IsNullOrEmpty(box3.SelectedText))
                                {
                                    continue;
                                }
                            }
                        }

                    }
                    //other lines
                    else
                    {
                        num = randomNumber - 2 * board + 2;
                        if (num >= 0 && num < size)
                        {
                            GridGameBox box2 = GridList[num];
                            if (box2.SelectedText == "S")
                            {
                                num1 = randomNumber - board + 1;
                                GridGameBox box3 = GridList[num1];
                                if (string.IsNullOrEmpty(box3.SelectedText))
                                {
                                    continue;
                                }
                            }
                        }
                        num = randomNumber + 2 * board - 2;
                        if (num >= 0 && num < size)
                        {
                            GridGameBox box2 = GridList[num];
                            if (box2.SelectedText == "S")
                            {
                                num1 = randomNumber + board - 1;
                                GridGameBox box3 = GridList[num1];
                                if (string.IsNullOrEmpty(box3.SelectedText))
                                {
                                    continue;
                                }
                            }
                        }


                        /////////////////
                        num = randomNumber - 2 * board + 2;
                        if (num >= 0 && num < size)
                        {
                            GridGameBox box2 = GridList[num];
                            if (box2.SelectedText == "S")
                            {
                                num1 = randomNumber - board + 1;
                                GridGameBox box3 = GridList[num1];
                                if (string.IsNullOrEmpty(box3.SelectedText))
                                {
                                    continue;
                                }
                            }
                        }
                        num = randomNumber + 2 * board + 2;
                        if (num >= 0 && num < size)
                        {
                            GridGameBox box2 = GridList[num];
                            if (box2.SelectedText == "S")
                            {
                                num1 = randomNumber + board + 1;
                                GridGameBox box3 = GridList[num1];
                                if (string.IsNullOrEmpty(box3.SelectedText))
                                {
                                    continue;
                                }
                            }
                        }
                    }

                    
                    num = randomNumber -2 *board;
                    if (num >= 0 && num < size)
                    {
                        GridGameBox box2 = GridList[num];
                        if (box2.SelectedText == "S")
                        {
                            num1 = randomNumber - board;
                            GridGameBox box3 = GridList[num1];
                            if (string.IsNullOrEmpty(box3.SelectedText))
                            {
                                continue;
                            }
                        }
                    }
                    num = randomNumber-2;
                    if (num >= 0 && num < size)
                    {
                        GridGameBox box2 = GridList[num];
                        if (box2.SelectedText == "S")
                        {
                            num1 = randomNumber - 1;
                            GridGameBox box3 = GridList[num1];
                            if (string.IsNullOrEmpty(box3.SelectedText))
                            {
                                continue;
                            }
                        }
                    }
                    num = randomNumber + 2;
                    if (num >= 0 && num < size)
                    {
                        GridGameBox box2 = GridList[num];
                        if (box2.SelectedText == "S")
                        {
                            num1 = randomNumber - board + 1;
                            GridGameBox box3 = GridList[num1];
                            if (string.IsNullOrEmpty(box3.SelectedText))
                            {
                                continue;
                            }
                        }
                    }

                    num = randomNumber + 2 * board;
                    if (num >= 0 && num < size)
                    {
                        GridGameBox box2 = GridList[num];
                        if (box2.SelectedText == "S")
                        {
                            num1 = randomNumber + board;
                            GridGameBox box3 = GridList[num1];
                            if (string.IsNullOrEmpty(box3.SelectedText))
                            {
                                continue;
                            }
                        }
                    }



                    randomBox.SetText("S", 1);
                    GridList[randomNumber] = randomBox;
                    completeCount++;
                    return false;
                }
            }
        }
    }
}


