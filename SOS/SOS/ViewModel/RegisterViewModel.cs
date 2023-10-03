using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SOS.Services;
using SOS.ViewModel;

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


        readonly IRegisterRepo registerRepo;
        readonly IPopupService popupService;

        public RegisterViewModel(IRegisterRepo registerRepo, IPopupService popupService)
        {
            this.registerRepo = registerRepo;
            this.popupService = popupService;
        }

        [RelayCommand]
        public async Task Register()
        {
            if (UserName.Length == 0)
            {
                var mes = new VarMessage("This username is wrong. Please try again");
                var pop = new PopUp(mes);
                popupService.ShowPopup(pop);
            }
            if (Password != ConfirmPassword)
            {
                var mes = new VarMessage("The Password isn't match. Please try again");
                var pop = new PopUp(mes);
                popupService.ShowPopup(pop);
            }
            if (Password.Length == 0 || ConfirmPassword.Length == 0)
            {
                var mes = new VarMessage("Please insert your passowrd. Please try again");
                var pop = new PopUp(mes);
                popupService.ShowPopup(pop);
            }

            bool res = await registerRepo.Register(UserName, Password, Email);
            if (res)
            {
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


        [RelayCommand]
        public async Task PreviousPage()
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            Dispose();
        }

        public void Dispose()
        {
            UserName = "";
            Password = "";
            ConfirmPassword = "";
            Email = "";
        }
    }
}
