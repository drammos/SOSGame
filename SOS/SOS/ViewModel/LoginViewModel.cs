using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Graphics.Text;
using SOS.Services;
using SOS.Models;
using System.Text.Json.Serialization;
using System.Text.Json;
using SOS.UseControl;
using SOS.Popups;

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


        readonly ILoginRepo loginRepo;
        readonly IPopupService popupService;
        public LoginViewModel(ILoginRepo loginRepo, IPopupService opupService)
        {
            this.loginRepo = loginRepo;
            this.popupService = opupService;
        }

        [RelayCommand]
        public async Task IsValid()
        {
            User user = await loginRepo.IsValid(UserName, Password);
            if (user!=null)
            {
                if(Preferences.ContainsKey(nameof(App.User)))
                {
                    Preferences.Remove(nameof(App.User));
                }
                string userDetails = JsonSerializer.Serialize(user);
                Preferences.Set(nameof(App.User), userDetails);
                App.User = user;

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
