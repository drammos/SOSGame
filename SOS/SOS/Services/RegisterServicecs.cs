using SQLite;
using System.Diagnostics;
using SOS.Models;
using System.Text;
using System.Security.Cryptography;

namespace SOS.Services
{
    public class RegisterService : IRegisterRepo
    {
        SQLiteAsyncConnection Database;
        public RegisterService(SQLiteAsyncConnection database)
        {
            Database = database;
        }

        public async Task<bool> Register(string username, string password, string email)
        {
            //First check the usename
            string hashedPassword = PasswordHasherRegister.HashPassword(password);
            User user = new User()
            {
                Guid = Guid.NewGuid().ToString(),
                UserName = username,
                Password = hashedPassword,
                Email = email
            };

            try
            {
                await Database.InsertAsync(user);
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
