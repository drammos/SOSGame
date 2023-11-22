using System.ComponentModel;
using SOS.Firebase;
using SOS.Models;

namespace SOS.ViewModel
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        private string[] _pickerLevelOptions = { "Easy", "Hard" };
        private string _selectedLevelOption;
        public IFirebaseClient firebaseClient;

        public SettingsViewModel(IFirebaseClient firebaseClient)
        {
            this.firebaseClient = firebaseClient;
        }

        public string[] PickerLevelOptions
        {
            get { return _pickerLevelOptions; }
            set
            {
                _pickerLevelOptions = value;
                OnPropertyChanged(nameof(PickerLevelOptions));
            }
        }

        public string SelectedLevelOption
        {
            get { return _selectedLevelOption; }
            set
            {
                _selectedLevelOption = value;
                OnPropertyChanged(nameof(SelectedLevelOption));
            }
        }

        private string[] _pickerBoardOptions = { "4x4", "5x5", "6x6", "7x7", "8x8" };
        private string _selectedBoardOption;

        public string[] PickerBoardOptions
        {
            get { return _pickerBoardOptions; }
            set
            {
                _pickerBoardOptions = value;
                OnPropertyChanged(nameof(PickerBoardOptions));
            }
        }

        public string SelectedBoardOption
        {
            get { return _selectedBoardOption; }
            set
            {
                _selectedBoardOption = value;
                OnPropertyChanged(nameof(SelectedBoardOption));
            }
        }

        private string[] _pickerPlayersOptions = { "1", "2", "3",  "4",  "5",  "6",  "7", "8", "9",  "10" };
        private string _selectedPlayersOption;

        public string[] PickerPlayersOptions
        {
            get { return _pickerPlayersOptions; }
            set
            {
                _pickerPlayersOptions = value;
                OnPropertyChanged(nameof(PickerPlayersOptions));
            }
        }

        public string SelectedPlayersOption
        {
            get { return _selectedPlayersOption; }
            set
            {
                _selectedPlayersOption = value;
                OnPropertyChanged(nameof(SelectedPlayersOption));
            }
        }


        public int GetGridBoard()
        {
            if(SelectedBoardOption == null || SelectedBoardOption == "")
            {
                return -1;
            }

            char[] array = SelectedBoardOption.ToCharArray();
            int board = array[0] - '0';

            return board;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task UpdateUserSettings(string email, int board, string level, int players)
        {
            SettingsData updateSettings = new SettingsData()
            {
                Email = email,
                Board = board,
                Level = level,
                Players = players
            };

            bool result = await this.firebaseClient.UpdateSettingsInFirestore(updateSettings);
            if (!result)
            {
                App.UserSettings = updateSettings;
            }
        }
    }
}
