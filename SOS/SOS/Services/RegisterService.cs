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
        public async Task<bool> Register(User user, SettingsData settings)
        {
            var database = await DBUtils.GetDatabase();

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
}
