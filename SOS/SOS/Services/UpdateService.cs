using System.Diagnostics;
using SOS.Models;
using SOS.Utils;

namespace SOS.Services
{
    public partial class UpdateService : IUpdateRepo
    {
        public UpdateService() { }

        public async Task<bool> Update(Guid gid,string username, string password, string email, string filePath)
        {
            var database = await DBUtils.GetDatabase();
            User user = new User()
            {
                Gid = gid,
                UserName = username,
                Password = password,
                Email = email,
                FilePath = filePath
            };

            try
            {
                await database.UpdateAsync(user);
                Debug.WriteLine("\n\nPASSWORD-1: " + gid);
                Debug.WriteLine("\n\nPASSWORD-2: " + username);
                Debug.WriteLine("\n\nPASSWORD-3: " + password);
                Debug.WriteLine("\n\nPASSWORD-4: " + email);
                Debug.WriteLine("\n\nPASSWORD-5: " + filePath);


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
