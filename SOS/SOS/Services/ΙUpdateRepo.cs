using SOS.Models;

namespace SOS.Services
{
    public interface IUpdateRepo
    {
        Task<bool> Update(Guid gid, string username, string password, string email, string filePath);
    }
}
