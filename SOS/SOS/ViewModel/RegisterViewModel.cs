using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SOS.Services;
using SOS.ViewModel;
using SOS.Popups;
using System.ComponentModel.DataAnnotations;

namespace SOS.ViewModel
{
    public partial class RegisterViewModel : BaseViewModel, IDisposable
    {
        [ObservableProperty]
        private string _userName;

        [ObservableProperty]
        private string _password;

        [ObservableProperty]
        private string _confirmPassword;
        
        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private bool _isRegisterButtonEnable;

        public string FilePath {  get; set; }


        readonly IRegisterRepo registerRepo;
        readonly IPopupService popupService;

        public RegisterViewModel(IRegisterRepo registerRepo, IPopupService popupService)
        {
            this.registerRepo = registerRepo;
            this.popupService = popupService;
        }

        private bool IsUsernameValid(string username)
        {
            // Έλεγχος για το username (π.χ., να ξεκινάει με χαρακτήρα)
            return !string.IsNullOrWhiteSpace(username) && username.Length >= 2 && char.IsLetter(username[0]);
        }

        private bool IsPasswordValid(string password)
        {
            // Έλεγχος για τον κωδικό (π.χ., να έχει τουλάχιστον 4 ψηφία)
            return !string.IsNullOrWhiteSpace(password) && password.Length >= 4 && (password.Any(char.IsDigit) || password.Any(char.IsLetter));
        }

        private bool IsEmailValid(string email)
        {
            // Έλεγχος για το email (π.χ., να περιέχει @ και έγκυρη κατάληξη)
            return !string.IsNullOrWhiteSpace(email) && email.Contains("@") && email.Split('@').Length == 2 && email.Split('@')[1].Contains(".");
        }

        [RelayCommand]
        public async Task LoginBack()
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }


        [RelayCommand]
        public async Task Register()
        {
            if ( !IsUsernameValid(UserName))
            {
                var mes = new VarMessage("This username is wrong. The username must start with characters and have at least 2 characters, please try again");
                var pop = new PopUp(mes);
                popupService.ShowPopup(pop);
                Dispose();
                return;
            }

            if (!IsPasswordValid(Password) || !IsPasswordValid(ConfirmPassword))
            {
                var mes = new VarMessage("The Password isn't wrong. The code must contain at least four characters. Please try again");
                var pop = new PopUp(mes);
                popupService.ShowPopup(pop);
                Dispose();
                return;
            }


            if (Password != ConfirmPassword)
            {
                var mes = new VarMessage("The Password isn't match. Please try again");
                var pop = new PopUp(mes);
                popupService.ShowPopup(pop);
                Dispose();
                return;
            }

            if (!IsEmailValid(Email))
            {
                var mes = new VarMessage("The Mail is wrong. Please try again");
                var pop = new PopUp(mes);
                popupService.ShowPopup(pop);
                
                Dispose();
                return;
            }

            bool res = await registerRepo.Register(UserName, Password, Email, FilePath);

            if (res)
            {
                String board = UserName + "Board";
                String level = UserName + "Level";
                String players = UserName + "Players";

                Preferences.Set(board, -1);
                Preferences.Set(level, "");
                Preferences.Set(players, -1);
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            }
            else
            {
                var mes = new VarMessage("This username is used by another user. Please try with different username");
                var pop = new PopUp(mes);
                popupService.ShowPopup(pop);
            }
            Dispose();
        }

        public async Task<bool> PopUp()
        {
            var res = await ShowYesCancelASync("Upload", "New Photo", "Pick Photo");
            return res;
        }

        [RelayCommand]
        public async Task PreviousPage()
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            Dispose();
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

        
        private bool IsSignUpButtonEnabled()
        {
            if(UserName != null && Password != null && ConfirmPassword != null && Email != null && UserName != "" && Password != "" && ConfirmPassword != "" && Email != "")
            {
                return true;
            }
            return false;
        }

        public void IsAllEntriesFilled()
        {
            if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(ConfirmPassword) && !string.IsNullOrEmpty(Email))
            {
                IsRegisterButtonEnable = true;
            }
            else
            {
                IsRegisterButtonEnable = false;
            }
        }

        public void Dispose()
        {
            UserName = "";
            Password = "";
            ConfirmPassword = "";
            Email = "";
            IsRegisterButtonEnable = false;
        }
    }
}
