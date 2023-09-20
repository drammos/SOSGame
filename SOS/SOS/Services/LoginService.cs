using SOS.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace SOS.Services
{
    public class LoginService : ILoginRepo
    {
        SQLiteAsyncConnection Database;
        public LoginService()
        {
        }

        async Task Init()
        {
            if (Database is not null) return;

            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            await Database.CreateTableAsync<User>();
        }

        public async Task<User> Login(string username, string password, string email)
        {
            await Init();
            //First check the usename 
            
            string hashedPassword = PasswordHasher.HashPassword(password);
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
            }catch(Exception E)
            {
                Debug.WriteLine(E.Message);
            }

            return user;
        }

        public async Task<bool> IsValid(string username, string password)
        {
            await Init();
            string hashedPassword = PasswordHasher.HashPassword(password);

            User user = await Database.Table<User>().Where(i => i.UserName == username).FirstOrDefaultAsync();
            bool isMatch = PasswordHasher.VerifyPassword( password, user.Password);

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