using SOS.Models;
using Plugin.CloudFirestore;

namespace SOS.Firebase
{
    public class FirebaseClient : IFirebaseClient
    {
        AuthenticationService authenticationService;
        public FirebaseClient() 
        {
            this.authenticationService = new AuthenticationService();
        }

        public async Task<bool> SaveUserToFirestore(User user)
        {
            await CrossCloudFirestore.Current
                .Instance
                .Collection("users")
                .Document(user.Email)
                .SetAsync(user);

            return true;
        }

        public async Task<bool> SaveSettingsToFirestore(SettingsData settings)
        {
            await CrossCloudFirestore.Current
                .Instance
                .Collection("settings")
                .Document(settings.Email)
                .SetAsync(settings);

            return true;
        }

        public async Task<bool> RegisterFirebaseAuhtenticaiton(string email, string password)
        {
            return await this.authenticationService.Register(email, password);
        }
        public async Task<bool> LoginFirebaseAuhtenticaiton(string email, string password)
        {
           return await this.authenticationService.Login(email, password);
        }

        public async Task<bool> RegisterWithGoogle()
        {
            return await this.authenticationService.RegisterWithGoogle();
        }
        public async Task<bool> LoginWithGoogle()
        {
            return await this.LoginWithGoogle();
        }

        // GET USER
        public async Task<User> GetUserFromFirestore(string email)
        {
            var document = await CrossCloudFirestore.Current
            .Instance
            .Collection("users")
            .Document(email)
            .GetAsync();

            if (document.Exists)
            {
                var user = document.ToObject<User>();
                return user;
            }
            else
            {
                return null; // Ή μπορείς να χειριστείς την απουσία του εγγράφου ανάλογα με τις ανάγκες σου
            }
        }

        // GET SETTINGS
        public async Task<SettingsData> GetSettingsFromFirestore(string email)
        {
            var document = await CrossCloudFirestore.Current
                .Instance
                .Collection("settings")
                .Document(email)
                .GetAsync();

            if (document.Exists)
            {
                var settings = document.ToObject<SettingsData>();
                return settings;
            }
            else
            {
                return null;
            }
        }

        // UPDATE USER
        public async Task<bool> UpdateUserInFirestore(User updatedUser)
        {
            try
            {
                await CrossCloudFirestore.Current
                    .Instance
                    .Collection("users")
                    .Document(updatedUser.Email)
                    .UpdateAsync(updatedUser);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Σφάλμα κατά την ενημέρωση: {ex.Message}");
                return false;
            }
        }
        // UPDATE SETTINGS
        public async Task<bool> UpdateSettingsInFirestore(SettingsData updatedSettings)
        {
            try
            {
                await CrossCloudFirestore.Current
                    .Instance
                    .Collection("settings")
                    .Document(updatedSettings.Email)
                    .UpdateAsync(updatedSettings);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Σφάλμα κατά την ενημέρωση: {ex.Message}");
                return false;
            }
        }
    }
}