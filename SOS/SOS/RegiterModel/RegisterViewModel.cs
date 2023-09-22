using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SOS.Services;

namespace SOS.RegisterModel
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



    public partial class RegisterViewModel : BaseRegViewModel, IDisposable
    {
        [ObservableProperty]
        private string _userName;

        [ObservableProperty]
        private string _password;

        [ObservableProperty]
        private string _email;


        readonly IRegisterRepo registerRepo;

        public RegisterViewModel(IRegisterRepo registerRepo)
        {
            this.registerRepo = registerRepo;
        }

        [RelayCommand]
        public async Task Register()
        {
            bool res = await registerRepo.Register(UserName, Password, Email);
            if (res)
            {
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            }
            else
            {
                var mes = new VarMessage("ela111");
                var pop = new PopUp(mes);
                this.

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
            Email = "";
        }


    }
}
