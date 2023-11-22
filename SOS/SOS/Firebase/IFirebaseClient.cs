using SOS.Models;

namespace SOS.Firebase
{
    public interface IFirebaseClient 
    {
        Task<bool> SaveUserToFirestore(User user);
        Task<bool> SaveSettingsToFirestore(SettingsData settings);
        // GET USER
        Task<User> GetUserFromFirestore(string email);
        // GET SETTINGS
        Task<SettingsData> GetSettingsFromFirestore(string email);
        // UPDATE USER
        Task<bool> UpdateUserInFirestore(User updatedUser);
        // UPDATE SETTINGS
        Task<bool> UpdateSettingsInFirestore(SettingsData updatedSettings);

        Task<bool> RegisterFirebaseAuhtenticaiton(string email, string password);

        Task<bool> LoginFirebaseAuhtenticaiton(string email, string password);

        Task<bool> RegisterWithGoogle();
        Task<bool> LoginWithGoogle();

    }
}
