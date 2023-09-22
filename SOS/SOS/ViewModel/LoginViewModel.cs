﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Graphics.Text;
using SOS.Services;
using System;
using System.Diagnostics;
using System.Windows.Input;

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
        public LoginViewModel(ILoginRepo loginRepo)
        {
            this.loginRepo = loginRepo;
        }

        [RelayCommand]
        public async Task IsValid()
        {
            bool res = await loginRepo.IsValid(UserName, Password);
            if (res)
            {
                await Shell.Current.GoToAsync($"//{nameof(StartGame)}");
            }
            else
            {
                var mes = new VarMessage("This username is used by another user. Please try with different username");
                var pop = new PopUp(mes);
                RegisterPage.ShowPopup(pop);
            }
            Dispose();
        }

        [RelayCommand]
        public async Task Tap()
        {
            await Shell.Current.GoToAsync($"//{nameof(RegisterPage)}");
        }

        public void Dispose()
        {
            UserName = "";
            Password = "";
            Email = "";
        }
    }
}
