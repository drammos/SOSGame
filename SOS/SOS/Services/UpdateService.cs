using System.Diagnostics;
using SOS.Models;
using SOS.Utils;

namespace SOS.Services
{
    public partial class UpdateService : IUpdateRepo
    {
        public UpdateService() { }

        public async Task<bool> Update(Guid gid,string username, string password, string email, string filePath, int score)
        {
            var database = await DBUtils.GetDatabase();
            User user = new User()
            {
                Gid = gid,
                UserName = username,
                Password = password,
                Email = email,
                FilePath = filePath,
                Score = score
            };

            try
            {
                await database.UpdateAsync(user);
                return true;
            }
            catch (Exception E)
            {
                Debug.WriteLine(E.Message);
                return false;
            }
        }
    }
}
