using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SOS.Services;
using SOS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Diagnostics;

namespace SOS.ViewModel
{
    public partial class LoginViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string _userName;

        [ObservableProperty]
        private string _password;

        readonly ILoginRepo loginRepo = new LoginService();

        [RelayCommand]
        public async void Login()
        {

            Debug.WriteLine("\n\nELAAAAAAAAAAAAAA\n\n");
            if (!string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrWhiteSpace(Password))
            {
                Info user = await loginRepo.Login(UserName, Password);
                if (Preferences.ContainsKey(nameof(App.UserInfo)))
                {
                    Preferences.Remove(nameof(App.UserInfo));
                }

                string userDetails = JsonSerializer.Serialize(user);
            }


            Console.WriteLine("\n\nELAAAAAAAAAAAAAA\n\n");
        }
    }
}
