using SOS.Services;
using SOS.Models;

namespace SOS.ViewModel
{
    public partial class HighScoreViewModel
    {
        public IUsersRepo Users;
        public HighScoreViewModel(IUsersRepo users)
        {
            this.Users = users;
        }

        public async Task<List<User>> TakeAllUsers()
        {
            List<User> list = await this.Users.TakeAllUsers();
            return list;
        }
    }
}
