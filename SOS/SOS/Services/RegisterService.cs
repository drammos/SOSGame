using System.Diagnostics;
using SOS.Models;
using System.Text;
using System.Security.Cryptography;
using SOS.Utils;

namespace SOS.Services
{
    public class RegisterService : IRegisterRepo
    {
        public RegisterService() { }
        public async Task<bool> Register(string username, string password, string email, string filePath)
        {
            var database = await DBUtils.GetDatabase();
            //First check the usename
            string hashedPassword = PasswordHasherRegister.HashPassword(password);
            if (string.IsNullOrEmpty(filePath))
            {
                filePath = "user.png";
            }
            User user = new User()
            {
                Gid = Guid.NewGuid(),
                UserName = username,
                Password = hashedPassword,
                Email = email,
                FilePath = filePath,
                Score = 0,
                Theme = "Light"
            };
            SettingsData settings = new SettingsData()
            {
                Username = username,
                Board = 0,
                Level = string.Empty,
                Players = 0
            };

            try
            {
                await database.InsertAsync(user);
                await database.InsertAsync(settings);
                return true;
            }
            catch (Exception E)
            {
                Debug.WriteLine(E.Message);
                return false;
            }
        }
    }


    // Pasword Hash
    public class PasswordHasherRegister
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
    }
}
