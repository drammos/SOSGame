using SOS.Models;

namespace SOS.Services
{
    public interface ILoginRepo
    {
        Task<User> IsValid(string username, string password);
        Task<SettingsData> TakeUserSettings(string username);
    }
}
