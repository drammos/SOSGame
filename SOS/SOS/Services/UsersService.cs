using System.Diagnostics;
using SOS.Models;
using SOS.Utils;

namespace SOS.Services
{
    public partial class UsersService : IUsersRepo
    {
        public UsersService() { }

        public async Task<List<User>> TakeAllUsers()
        {
            var database = await DBUtils.GetDatabase();
            List<User> users = null;

            try
            {
                users = await database.Table<User>().ToListAsync();
            }
            catch (Exception E)
            {
                Debug.WriteLine(E.Message);
            }

            return users;
        }
    }
}
