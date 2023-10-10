using SOS.Models;

namespace SOS.Services
{
    public interface IUsersRepo
    {
        Task<List<User>> TakeAllUsers();
    }
}
