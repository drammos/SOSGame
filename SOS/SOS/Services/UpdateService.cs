using System.Diagnostics;
using SOS.Models;
using SOS.Utils;

namespace SOS.Services
{
    public partial class UpdateService : IUpdateRepo
    {
        public UpdateService() { }

        public async Task<bool> Update(Guid gid,string username, string password, string email, string filePath, int score, string theme)
        {
            var database = await DBUtils.GetDatabase();
            User user = new User()
            {
                Gid = gid,
                UserName = username,
                Password = password,
                Email = email,
                FilePath = filePath,
                Score = score,
                Theme = theme
            };

            try
            {
                await database.UpdateAsync(user);
                App.User = user;
                return true;
            }
            catch (Exception E)
            {
                Debug.WriteLine(E.Message);
                return false;
            }
        }

        public async Task<bool> UpdateSettings( string username, int board, string level, int players)
        {
            var database = await DBUtils.GetDatabase();
            SettingsData settings = new SettingsData()
            {
                Username = username,
                Board = board,
                Level = level,
                Players = players
            };

            try
            {
                await database.UpdateAsync(settings);
                App.UserSettings = settings;
                return true;
            }
            catch(Exception E)
            {
                Debug.WriteLine(E.Message); 
                return false;
            }
        }
    }
}
