using SOS.Models;
using System.Text;
using System.Security.Cryptography;
using SOS.Utils;

namespace SOS.Services
{
    public class LoginService : ILoginRepo
    {
        public LoginService() { }
        public async Task<User> IsValid(string username, string password)
        {
            var database = await DBUtils.GetDatabase();
            string hashedPassword = PasswordHasher.HashPassword(password);

            User user = await database.Table<User>().Where(i => i.UserName == username).FirstOrDefaultAsync();
            bool isMatch = false;
            if (user != null)
            {
                isMatch = PasswordHasher.VerifyPassword(password, user.Password);
                if (isMatch)
                {
                    return user;
                }
            }
            return null;
        }

        public async Task<SettingsData> TakeUserSettings(string email)
        {
            var database = await DBUtils.GetDatabase();
            if (!string.IsNullOrEmpty(email))
            {
                SettingsData settingsData = await database.Table<SettingsData>().Where(i => i.Email == email).FirstOrDefaultAsync();
                return settingsData;
            }
            return null;
        }
    }


    // Pasword Hash
    public class PasswordHasher
    {

        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            string hashedInput = HashPassword(password);
            return string.Equals(hashedInput, hashedPassword);
        }
    }
}