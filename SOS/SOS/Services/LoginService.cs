using SOS.Models;
using SQLite;
using System.Diagnostics;
using System.Text;
using System.Security.Cryptography;

namespace SOS.Services
{
    public class LoginService : ILoginRepo
    {
        SQLiteAsyncConnection Database;
        public LoginService(SQLiteAsyncConnection database)
        {
            Database = database;
        }

        //async Task Seed()
        //{
        //    await Register("Test", "9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08", "b@n.c");
        //    await Register("Test1", "1b4f0e9851971998e732078544c96b36c3d01cedf7caa332359d6f1d83567014", "n@n.c");
        //}

        public async Task<bool> IsValid(string username, string password)
        {
            string hashedPassword = PasswordHasher.HashPassword(password);

            User user = await Database.Table<User>().Where(i => i.UserName == username).FirstOrDefaultAsync();
            bool isMatch = false;
            if (user != null) 
            {
                isMatch = PasswordHasher.VerifyPassword(password, user.Password);
            }

            if (isMatch)
            {
                return true;
            }
            return false;
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