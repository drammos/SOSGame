// Libraries
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

// Lcocal namespaces
using SOS.Box;
using SOS.Services;
using SOS.Popups;

namespace SOS.ViewModel
{
    public partial class GridGameViewModel : BaseViewModel
    {
        public ObservableCollection<GridGameBox> GridList { get; set; } = new ObservableCollection<GridGameBox>();
        private int completeCount;    
        private List<int> FreeList = new List<int>();

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

        [ObservableProperty]
        private string _level;

        private CancellationTokenSource cancellationTokenSource;

        readonly IUpdateRepo updateRepo;
        readonly IPopupService popupService;

        // Initialize the game Grid Game
        public GridGameViewModel(IUpdateRepo updateRepo, IPopupService popupService)
        {
            this.PlayerTurn = "user";
            this.popupService = popupService;
            this.updateRepo = updateRepo;
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
                int result;
                
                if (this.IsComputer)
                {
                    if (completeCount < BoardSpan * BoardSpan)
                    {
                        if(Level == "Easy")
                        {
                            result = this.SelectPCEasy();
                            if (result == 0)
                            {
                                this.PlayerTurn = "user";
                                this._playerTurn = "user";
                                this.IsUser = true;
                                this.IsComputer = false;
                            }
                            else
                            {
                                this.ComputerScore += 5*result;
                            }
                        }
                        else if(Level == "Hard")
                        {
                            result = this.SelectPCHard();
                            if (result == 0)
                            {
                                this.PlayerTurn = "user";
                                this._playerTurn = "user";
                                this.IsUser = true;
                                this.IsComputer = false;
                            }
                            else
                            {
                                this.ComputerScore += 10*result;
                            }
                        }

                        if (completeCount == BoardSpan * BoardSpan)
                        {
                            EndGame();
                        }
                    }   
                }
            }
        }

        public int RestartBoard()
        {
            SetUpGame();
            UserScore = 0;
            ComputerScore = 0;
            UserName = App.User.UserName;
            FilePath = App.User.FilePath;
            Level = App.UserSettings.Level;
            return this.BoardSpan;
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
            UserName = App.User.UserName;
            FilePath = App.User.FilePath;
            Level = App.UserSettings.Level;
            ComputerScore = 0;
            GridList.Clear();
            BoardSpan = App.UserSettings.Board;
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
            Level = App.UserSettings.Level;
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
                    int count = checkForSOS(selectedItem);
                    if (count == 0)
                    {
                        this.PlayerTurn = "pc";
                        this._playerTurn = "pc";
                        this.IsUser = false;
                        this.IsComputer = true;
                    }
                    else
                    {
                        if (Level == "Easy")
                        {
                            this.UserScore += 5*count;
                        }
                        else
                        {
                            this.UserScore += 10*count;
                        }
                    }
                    completeCount++;
                }
                else if (res == "O")
                {
                    selectedItem.SetText("O", 0);
                    int count = checkForSOS(selectedItem);
                    if (count == 0)
                    {
                        this.PlayerTurn = "pc";
                        this._playerTurn = "pc";
                        this.IsUser = false;
                        this.IsComputer = true;
                    }
                    else
                    {
                        if (Level == "Easy")
                        {
                            this.UserScore += 5 * count;
                        }
                        else
                        {
                            this.UserScore += 10 * count;
                        }
                    }
                    completeCount++;
                }

                if(completeCount == BoardSpan*BoardSpan) 
                {
                   EndGame();
                }
            }
        }

        public async void EndGame()
        {

            VarMessage mes;
            if (this.UserScore > this.ComputerScore)
            {
                mes = new VarMessage("You Win!!!");
                UpdateDataBase();
            }
            else if (this.UserScore < this.ComputerScore)
            {
                mes = new VarMessage("Game Over!");
            }
            else
            {
                mes = new VarMessage("Draw!!!");
                UpdateDataBase();
            }

            var popUp = new PopUpGame(mes);
            var result = await popupService.ShowPopup<string>(popUp);
            if (result == "play")
            {
                this.Reset();
            }
            else if (result == "quit")
            {
                this.QuitGame();
            }
           
        }

        public async void UpdateDataBase()
        {
            int score = App.User.Score + UserScore;
            bool res = await updateRepo.Update(App.User.Gid, UserName, App.User.Password, App.User.Email, App.User.FilePath, score, App.User.Theme);
            App.User.Score = score;
            if (!res)
            {
                var mes = new VarMessage("The User score don't update!");
                var pop = new PopUp(mes);
                popupService.ShowPopup(pop);
            }
        }

        public bool checkForS(int randomNumber, int num, int num1)
        {
            int board = BoardSpan;
            int size = board * board;
            GridGameBox box = GridList[randomNumber];
            
            if (num >= 0 && num < size)
            {
                GridGameBox box2 = GridList[num];

                if (box2.SelectedText == "S")
                {
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
            return false;
        }

        public bool checkForO(int randomNumber, int num, int num1)
        {
            int board = BoardSpan;
            int size = board * board;
            GridGameBox box = GridList[randomNumber];
            if (num >= 0 && num < size)
            {
                GridGameBox box2 = GridList[num];
                if (box2.SelectedText == "S")
                {
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
            return false;
        }

        public int checkForSOS(GridGameBox box)
        {
            int board = BoardSpan;
            int size = board * board;
            int randomNumber = box.Index;
            int check = 0;
            //prepei na testaro ola ta senaria 

            if (box.SelectedText == "S")
            {
                //1st line
                if (randomNumber % board == 0)
                {
                    if((randomNumber - 2 * board + 2) > 0)
                    {
                        if (checkForS(randomNumber, randomNumber - 2 * board + 2, randomNumber - board + 1)) check++;
                    }
                    
                    if((randomNumber + 2 * board + 2) < size)
                    {
                        if (checkForS(randomNumber, randomNumber + 2 * board + 2, randomNumber + board + 1)) check++;
                    }
                    if (checkForS(randomNumber, randomNumber + 2, randomNumber + 1)) check++;
                }
                //last line
                else if ((randomNumber + 1) % board == 0)
                {
                    if ((randomNumber - 2 * board - 2) > 0)
                    {
                        if (checkForS(randomNumber, randomNumber - 2 * board - 2, randomNumber - board - 1)) check++;
                    }
                    if ((randomNumber + 2 * board - 2) < size)
                    {
                        if (checkForS(randomNumber, randomNumber + 2 * board - 2, randomNumber + board - 1)) check++;
                    }

                    if (checkForS(randomNumber, randomNumber - 2, randomNumber - 1)) check++;
                }
                else
                {
                    if ((randomNumber + 2) % board == 0)
                    {
                        if (randomNumber - 2 * board - 2 >= 0)
                        {
                            if (checkForS(randomNumber, randomNumber - 2 * board - 2, randomNumber - board - 1)) check++;
                        }
                        if (randomNumber + 2 * board - 2 < size)
                        {
                            if (checkForS(randomNumber, randomNumber + 2 * board - 2, randomNumber + board - 1)) check++;
                        }
                        if (checkForS(randomNumber, randomNumber - 2, randomNumber - 1)) check++;
                    }
                    else if ((randomNumber - 1) % board == 0)
                    {
                        if ((randomNumber - 2 * board + 2) > 0)
                        {
                            if (checkForS(randomNumber, randomNumber - 2 * board + 2, randomNumber - board + 1)) check++;
                        }
                        if ((randomNumber + 2 * board + 2) < size)
                        {
                            if (checkForS(randomNumber, randomNumber + 2 * board + 2, randomNumber + board + 1)) check++;
                        }
                        if (checkForS(randomNumber, randomNumber + 2, randomNumber + 1)) check++;
                    }
                    else
                    {
                        if ((randomNumber - 2 * board + 2) > 0)
                        {
                            if (checkForS(randomNumber, randomNumber - 2 * board + 2, randomNumber - board + 1)) check++;
                        }
                        if (randomNumber + 2 * board - 2 < size)
                        {
                            if (checkForS(randomNumber, randomNumber + 2 * board - 2, randomNumber + board - 1)) check++;
                        }
                        if ((randomNumber + 2 * board + 2) < size)
                        {
                            if (checkForS(randomNumber, randomNumber + 2 * board + 2, randomNumber + board + 1)) check++;
                        }
                        if (randomNumber - 2 * board - 2 >= 0)
                        {
                            if (checkForS(randomNumber, randomNumber - 2 * board - 2, randomNumber - board - 1)) check++;
                        }
                        if (checkForS(randomNumber, randomNumber + 2, randomNumber + 1)) check++;
                        if (checkForS(randomNumber, randomNumber - 2, randomNumber - 1)) check++;
                    }
                }
                if ((randomNumber + 2 * board) < size)
                {
                    if (checkForS(randomNumber, randomNumber + 2*board, randomNumber + board)) check++;
                }
                if ((randomNumber - 2 * board) >= 0)
                {
                    if (checkForS(randomNumber, randomNumber - 2*board, randomNumber - board)) check++;
                }
            }
            else if(box.SelectedText == "O")
            {
                if (randomNumber % board == 0)
                {
                    if(checkForO(randomNumber, randomNumber - board, randomNumber + board)) check++;
                }
                else if ((randomNumber + 1) % board == 0)
                {
                    if (checkForO(randomNumber, randomNumber - board, randomNumber + board)) check++;
                }
                else
                {
                    // tha koitakso deksio deksi aristera panw kai katw kai diagwnia
                    if (checkForO(randomNumber, randomNumber - board, randomNumber + board)) check++;
                    if (checkForO(randomNumber, randomNumber - 1, randomNumber + 1)) check++;
                    if (checkForO(randomNumber, randomNumber - board -1, randomNumber + board +1)) check++;
                    if (checkForO(randomNumber, randomNumber - board +1, randomNumber + board -1)) check++;
                }
            }
            return check;
        }

        public int SelectPCEasy()
        {
            Thread.Sleep(1000);
            int size = BoardSpan * BoardSpan;
            int seed = (int)DateTime.Now.Ticks;
            Random random = new Random(seed);
            GridGameBox box;
            int randomNumber;
            while (true) 
            {
                int number = random.Next();
                randomNumber = number % size;
                box = GridList[randomNumber];
                if (string.IsNullOrEmpty(box.SelectedText)) break;
            }
            int numberNum = random.Next();
            if ((numberNum % 2) == 0)
            {
                box.SetText("S", 1);
            }
            else
            {
                box.SetText("O", 1);
            }
            GridList[randomNumber] = box;
            completeCount++;
            return checkForSOS(box);
        }
        
        public int SelectPCHard()
        {
            Thread.Sleep(1000);
            int board = BoardSpan;
            int box;
            for (int i = 0; i < board; i++)
            {
                box = i;
                for(int j = 0; j < board; j++)
                {
                    int resultBox;
                    GridGameBox boxPrice = GridList[box];
                    // First Line
                    if (i == 0)
                    {   
                        // Check Horizontal Line
                        if (j + 1 < board - 1) if((resultBox=CheckTheBoxes(boxPrice, box + board, box + 2 * board))!=0) return resultBox;
                        // Check Vertical Line
                        if ((resultBox = CheckTheBoxes(boxPrice, box + 1, box + 2))!=0) return resultBox;
                        //Check Down Right Line
                        if (j + 1 < board - 1) if((resultBox = CheckTheBoxes(boxPrice, box + board + 1, box + 2 * board + 2))!=0) return resultBox;
                        //Check Down Left Line
                        if (j - 1 > 0) if((resultBox = CheckTheBoxes(boxPrice, box - board + 1, box - 2 * board + 2)) != 0) return resultBox;
                    }
                    // Last Line
                    else if (i == board - 1)
                    {
                        // Check Horizontal Line
                        if (j + 1 < board - 1) if ((resultBox = CheckTheBoxes(boxPrice, box + board, box + 2 * board)) != 0) return resultBox;
                    }
                    // Other Lines
                    else
                    {
                        // Check Horizontal Line
                        if (j + 1 < board - 1) if ((resultBox = CheckTheBoxes(boxPrice, box + board, box + 2 * board)) != 0) return resultBox;
                        
                        // Check Lines
                        if (i + 1 < board - 1)
                        {
                            // Check Vertical Line
                            if ((resultBox = CheckTheBoxes(boxPrice, box + 1, box + 2)) != 0) return resultBox;
                            //Check Down Right Diagonal Line
                            if (j + 1 < board - 1) if ((resultBox = CheckTheBoxes(boxPrice, box + board + 1, box + 2 * board + 2)) != 0) return resultBox;
                            //Check Down Left Diagonal Line
                            if (j - 1 > 0) if ((resultBox = CheckTheBoxes(boxPrice, box - board + 1, box - 2 * board + 2)) != 0) return resultBox;
                        }
                    }
                    box += board;
                }
            }

            int seed = (int)DateTime.Now.Ticks;
            Random random = new Random(seed);
            //allios dialego random
            int size = BoardSpan * BoardSpan;

            FreeList.Clear();
            FreeList = new List<int>();
            int res;
            while (true)
            {
                int number = random.Next();
                int randomNumber = number % size;
                GridGameBox randomBox = GridList[randomNumber];
                if (string.IsNullOrEmpty(randomBox.SelectedText))
                {
                    // Check For S
                    //Check The First Line
                    if (randomNumber % board == 0)
                    {
                        // eimaste sti proti grammi kai theloume na doume an einai diagwnios ara randomNumber -2board +2 na einai >0 < board -1
                        if ((randomNumber - 2 * board + 2) > 0)
                        {
                            res = CheckTheRandomBox(randomNumber, randomNumber - 2 * board + 2, randomNumber - board + 1);
                            if (res == 1)
                            {
                                return 0;
                            }
                            else if(res == 0)
                            {
                                continue;
                            }
                        }
                        if ((randomNumber + 2 * board + 2) < size)
                        {
                            res = CheckTheRandomBox(randomNumber, randomNumber + 2 * board + 2, randomNumber + board + 1);
                            if (res == 1)
                            {
                                return 0;
                            }
                            else if (res == 0)
                            {
                                continue;
                            }
                        }
                        res = CheckTheRandomBox(randomNumber, randomNumber + 2, randomNumber + 1);
                        if (res == 1)
                        {
                            return 0;
                        }
                        else if (res == 0)
                        {
                            continue;
                        }

                    }
                    // Check The Last Line
                    else if((randomNumber+1) % board == 0)
                    {
                        if ((randomNumber - 2 * board - 2) > 0) 
                        {
                            res = CheckTheRandomBox(randomNumber, randomNumber - 2 * board - 2, randomNumber - board - 1);
                            if (res == 1)
                            {
                                return 0;
                            }
                            else if (res == 0)
                            {
                                continue;
                            }
                        }
                        if ((randomNumber + 2 * board - 2) < size)
                        {
                            res = CheckTheRandomBox(randomNumber, randomNumber + 2 * board - 2, randomNumber + board - 1);
                            if (res == 1)
                            {
                                return 0;
                            }
                            else if (res == 0)
                            {
                                continue;
                            }
                        }
                        res = CheckTheRandomBox(randomNumber, randomNumber - 2, randomNumber - 1);
                        if (res == 1)
                        {
                            return 0;
                        }
                        else if (res == 0)
                        {
                            continue;
                        }
                    }
                    // Check Other Lines
                    else
                    {
                        if ((randomNumber + 2) % board == 0)
                        {
                            if (randomNumber - 2 * board - 2 >= 0)
                            {
                                res = CheckTheRandomBox(randomNumber, randomNumber - 2 * board - 2, randomNumber - board - 1);
                                if (res == 1)
                                {
                                    return 0;
                                }
                                else if (res == 0)
                                {
                                    continue;
                                }
                            }
                            if (randomNumber + 2 * board - 2 < size)
                            {
                                res = CheckTheRandomBox(randomNumber, randomNumber + 2 * board - 2, randomNumber + board - 1);
                                if (res == 1)
                                {
                                    return 0;
                                }
                                else if (res == 0)
                                {
                                    continue;
                                }
                            }
                            res = CheckTheRandomBox(randomNumber, randomNumber - 2, randomNumber - 1);
                            if (res == 1)
                            {
                                return 0;
                            }
                            else if (res == 0)
                            {
                                continue;
                            }
                        }
                        //2h grammi dne exei panw diagwnious
                        else if ((randomNumber - 1) % board == 0)
                        {
                            if ((randomNumber - 2 * board + 2) > 0)
                            {
                                res = CheckTheRandomBox(randomNumber, randomNumber - 2 * board + 2, randomNumber - board + 1);
                                if (res == 1)
                                {
                                    return 0;
                                }
                                else if (res == 0)
                                {
                                    continue;
                                }
                            }
                            if ((randomNumber + 2 * board + 2) < size)
                            {
                                res = CheckTheRandomBox(randomNumber, randomNumber + 2 * board + 2, randomNumber + board + 1);
                                if (res == 1)
                                {
                                    return 0;
                                }
                                else if (res == 0)
                                {
                                    continue;
                                }
                            }
                            res = CheckTheRandomBox(randomNumber, randomNumber + 2, randomNumber + 1);
                            if (res == 1)
                            {
                                return 0;
                            }
                            else if (res == 0)
                            {
                                continue;
                            }
                        }
                        else
                        {
                            if ((randomNumber - 2 * board + 2) > 0)
                            {
                                res = CheckTheRandomBox(randomNumber, randomNumber - 2 * board + 2, randomNumber - board + 1);
                                if (res == 1)
                                {
                                    return 0;
                                }
                                else if (res == 0)
                                {
                                    continue;
                                }
                            }
                            if (randomNumber + 2 * board - 2 < size)
                            {
                                res = CheckTheRandomBox(randomNumber, randomNumber + 2 * board - 2, randomNumber + board - 1);
                                if (res == 1)
                                {
                                    return 0;
                                }
                                else if (res == 0)
                                {
                                    continue;
                                }
                            }
                            if ((randomNumber + 2 * board + 2) < size)
                            {
                                res = CheckTheRandomBox(randomNumber, randomNumber + 2 * board + 2, randomNumber + board + 1);
                                if (res == 1)
                                {
                                    return 0;
                                }
                                else if (res == 0)
                                {
                                    continue;
                                }
                            }
                            if (randomNumber - 2 * board - 2 >= 0)
                            {
                                res = CheckTheRandomBox(randomNumber, randomNumber - 2 * board - 2, randomNumber - board - 1);
                                if (res == 1)
                                {
                                    return 0;
                                }
                                else if (res == 0)
                                {
                                    continue;
                                }
                            }
                            res = CheckTheRandomBox(randomNumber, randomNumber + 2, randomNumber + 1);
                            if (res == 1)
                            {
                                return 0;
                            }
                            else if (res == 0)
                            {
                                continue;
                            }
                            res = CheckTheRandomBox(randomNumber, randomNumber - 2, randomNumber - 1);
                            if (res == 1)
                            {
                                return 0;
                            }
                            else if (res == 0)
                            {
                                continue;
                            }
                        }
                    }

                    if ((randomNumber + 2 * board) < size)
                    {
                        res = CheckTheRandomBox(randomNumber, randomNumber + 2 * board, randomNumber + board);
                        if (res == 1)
                        {
                            return 0;
                        }
                        else if (res == 0)
                        {
                            continue;
                        }
                    }
                    if ((randomNumber - 2 * board) >= 0)
                    {
                        res = CheckTheRandomBox(randomNumber, randomNumber - 2 * board, randomNumber - board);
                        if (res == 1)
                        {
                            return 0;
                        }
                        else if (res == 0)
                        {
                            continue;
                        }
                    }

                    randomBox.SetText("S", 1);
                    GridList[randomNumber] = randomBox;
                    completeCount++;
                    return 0;
                }
            }
        }
    
        public int CheckTheBoxes(GridGameBox box1, int index1, int index2)
        {
            GridGameBox box2 = GridList[index1];
            GridGameBox box3 = GridList[index2];
            if (box1.SelectedText == "S")
            {
                if (box2.SelectedText == "O" && string.IsNullOrEmpty(box3.SelectedText))
                {
                    box3.SetText("S", 1);
                    box3.SetDisableColors();
                    box1.SetDisableColors();
                    box2.SetDisableColors();
                    completeCount++;
                    return checkForSOS(box3);
                }
                else if (string.IsNullOrEmpty(box2.SelectedText) && box3.SelectedText == "S")
                {
                    box2.SetText("O", 1);
                    box2.SetDisableColors();
                    box1.SetDisableColors();
                    box3.SetDisableColors();
                    completeCount++;
                    return checkForSOS(box2);
                }
            }
            else if (string.IsNullOrEmpty(box1.SelectedText))
            {
                GridGameBox boxPriceNext = GridList[index1];
                GridGameBox boxPriceDoubleNext = GridList[index2];
                if (boxPriceNext.SelectedText == "O" && boxPriceDoubleNext.SelectedText == "S")
                {
                    box1.SetText("S", 1);
                    box1.SetDisableColors();
                    box2.SetDisableColors();
                    box3.SetDisableColors();
                    completeCount++;
                    return checkForSOS(box1);
                }
            }
            return 0;
        }
    
        public int CheckTheRandomBox(int randomNumber, int index1, int index2)
        {
            GridGameBox box = GridList[randomNumber];
            int size = BoardSpan * BoardSpan;
            int freeBox = size - completeCount;
            if(index1>=0 && index1<size)
            {
                GridGameBox box2 = GridList[index1];
                if (box2.SelectedText == "S")
                {
                    GridGameBox box3 = GridList[index2];
                    if (string.IsNullOrEmpty(box3.SelectedText))
                    {
                        if (FreeList.Contains(randomNumber))
                        {
                            return 0;
                        }
                        else
                        {
                            FreeList.Add(randomNumber);
                            if (FreeList.Count == freeBox)
                            {
                                box.SetText("S", 1);
                                GridList[randomNumber] = box;
                                completeCount++;
                                return 1;
                            }
                            return 0;
                        }
                    }
                }
                else if(string.IsNullOrEmpty(box2.SelectedText))
                {
                    GridGameBox box3 = GridList[index2];
                    if(box3.SelectedText == "O")
                    {
                        if(FreeList.Contains(randomNumber))
                        {
                            return 0;
                        }
                        else
                        {
                            FreeList.Add(randomNumber);
                            if(FreeList.Count == freeBox)
                            {
                                box.SetText("S", 1);
                                GridList[randomNumber] = box;
                                completeCount++;
                                return 1;
                            }
                            return 0;
                        }
                    }
                }
            }
            return 2;
        }
    }
}


