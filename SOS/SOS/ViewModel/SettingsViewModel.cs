using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOS.ViewModel
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        private string[] _pickerLevelOptions = { "Easy", "Hard" };
        private string _selectedLevelOption;

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

        /// 
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
        /// </summary>


        private int[] _pickerPlayersOptions = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        private int _selectedPlayersOption;

        public int[] PickerPlayersOptions
        {
            get { return _pickerPlayersOptions; }
            set
            {
                _pickerPlayersOptions = value;
                OnPropertyChanged(nameof(PickerPlayersOptions));
            }
        }

        public int SelectedPlayersOption
        {
            get { return _selectedPlayersOption; }
            set
            {
                _selectedPlayersOption = value;
                OnPropertyChanged(nameof(SelectedPlayersOption));
            }
        }




        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
