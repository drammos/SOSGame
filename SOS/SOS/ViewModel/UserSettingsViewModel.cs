using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SOS.Models;
using SOS.Services;
using SOS.UseControl;

namespace SOS.ViewModel
{
    public partial class UserSettingsViewModel : BaseViewModel
    {

        [ObservableProperty]
        private string _userName;

        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string _password;

        [ObservableProperty]
        private Guid _gid;

        [ObservableProperty]
        private bool _isUpdateButtonEnable;

        [ObservableProperty]
        private string _filePath;

        [ObservableProperty]
        private int _score;

        readonly IUpdateRepo updateRepo;
        readonly IPopupService popupService;

        public UserSettingsViewModel(IUpdateRepo updateRepo, IPopupService popupService)
        {
            this.updateRepo = updateRepo;
            this.popupService = popupService;
        }

        public async Task<bool> PopUp()
        {
            var res = await ShowYesCancelASync("Upload", "New Photo", "Pick Photo");
            return res;
        }

        public Task<bool> ShowYesCancelASync(string message, string OK = "OK", string Cancel = "Cancel")
        {
            var tcs = new TaskCompletionSource<bool>();



            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    if (string.IsNullOrEmpty(Cancel) || Cancel.Equals("Cancel"))
                        Cancel = "_cancel";



                    if (!string.IsNullOrEmpty(message))
                        message = message.Replace("\\n", Environment.NewLine);



                    var result = await Application.Current.MainPage.DisplayAlert("", message, OK, Cancel);
                    tcs.TrySetResult(result);
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
            });



            return tcs.Task;
        }


        private bool IsEmailValid(string email)
        {
            // Έλεγχος για το email (π.χ., να περιέχει @ και έγκυρη κατάληξη)
            return !string.IsNullOrWhiteSpace(email) && email.Contains("@") && email.Split('@').Length == 2 && email.Split('@')[1].Contains(".");
        }

        public void IsAllEntriesFilled()
        {
            if (!string.IsNullOrEmpty(Email))
            {
                IsUpdateButtonEnable = true;
            }
            else
            {
                IsUpdateButtonEnable = false;
            }
        }

        [RelayCommand]
        public async Task Update()
        {
            if (!IsEmailValid(Email))
            {
                var mes = new VarMessage("The Mail is wrong. Please try again");
                var pop = new PopUp(mes);
                popupService.ShowPopup(pop);
                return;
            }

            bool res = await updateRepo.Update(Gid, UserName, Password, Email, FilePath, Score);

            if (res)
            {
                App.User.FilePath = FilePath;
                App.User.Email = Email;
                AppShell.Current.FlyoutHeader = new FlyoutHead();
                await Shell.Current.GoToAsync($"//{nameof(StartGame)}");
            }
            else
            {
                var mes = new VarMessage("The update failed!");
                var pop = new PopUp(mes);
                popupService.ShowPopup(pop);
            }
        }
    }
}
