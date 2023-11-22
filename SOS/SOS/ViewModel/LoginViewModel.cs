using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text.Json;

using SOS.Services;
using SOS.Models;
using SOS.UseControl;
using SOS.Popups;
using SOS.Firebase;

namespace SOS.ViewModel
{

    public class HyperlinkSpan : Span
    {
        public static readonly BindableProperty UrlProperty =
            BindableProperty.Create(nameof(Url), typeof(string), typeof(HyperlinkSpan), null);

        public string Url
        {
            get { return (string)GetValue(UrlProperty); }
            set { SetValue(UrlProperty, value); }
        }

        public HyperlinkSpan()
        {
            TextDecorations = TextDecorations.Underline;
            TextColor = Colors.Blue;
            GestureRecognizers.Add(new TapGestureRecognizer
            {
                // Launcher.OpenAsync is provided by Essentials.
                Command = new Command(async () => await Launcher.OpenAsync(Url))
            });
        }
    }



    public partial class LoginViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string _userName;

        [ObservableProperty]
        private string _password;

        [ObservableProperty]
        private string _email;

        public SignInModel SignIn { get; set; }

        readonly IFirebaseClient firebaseCLient;
        readonly IPopupService popupService;

        public LoginViewModel(IFirebaseClient firebaseClient, IPopupService opupService)
        {
            this.firebaseCLient = firebaseClient;
            this.popupService = opupService;
            this.SignIn = new SignInModel();
        }

        [RelayCommand]
        public async Task IsValid()
        {
            string password = PasswordHasher.HashPassword(Password);
            bool login = await this.firebaseCLient.LoginFirebaseAuhtenticaiton(Email, password);
            if (!login)
            {
                var mes = new VarMessage("Invalid email or password. Please try again");
                var pop = new PopUp(mes);
                popupService.ShowPopup(pop);
                return;
            }

            User user = await firebaseCLient.GetUserFromFirestore(Email);
            if (user!=null)
            {
                if(Preferences.ContainsKey(nameof(App.User)))
                {
                    Preferences.Remove(nameof(App.User));
                }
                string userDetails = JsonSerializer.Serialize(user);
                Preferences.Set(nameof(App.User), userDetails);
                App.User = user;
                App.UserSettings = await firebaseCLient.GetSettingsFromFirestore(user.Email);

                AppShell.Current.FlyoutHeader = new FlyoutHead();
                await Shell.Current.GoToAsync($"//{nameof(StartGame)}");
            }
            else
            {
                var mes = new VarMessage("Invalid username or password. Please try again");
                var pop = new PopUp(mes);
                popupService.ShowPopup(pop);
            }

            Dispose();
        }

        [RelayCommand]
        public async Task Tap()
        {
            await Shell.Current.GoToAsync(nameof(RegisterPage));
        }

        public void Dispose()
        {
            UserName = "";
            Password = "";
            Email = "";
        }
    }
}
