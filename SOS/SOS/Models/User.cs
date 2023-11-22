using SQLite;
using System.Text;
using System.Security.Cryptography;
using TableAttribute = SQLite.TableAttribute;

namespace SOS.Models
{
    [Table("users")]
    public class User
    {
        [PrimaryKey]
        public Guid Gid { get; set; }
        [Unique]
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FilePath { get; set; }
        public int Score { get; set; }
        public string Theme {  get; set; }

        public User() { }

        public User(string username, string password, string email, string filePath)
        {
            string hashedPassword = PasswordHasherRegister.HashPassword(password);
            if (string.IsNullOrEmpty(filePath))
            {
                filePath = "user.png";
            }
            Gid = Guid.NewGuid();
            UserName = username;
            Password = hashedPassword;
            Email = email;
            FilePath = filePath;
            Score = 0;
            Theme = "Light";
        }
    }
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
