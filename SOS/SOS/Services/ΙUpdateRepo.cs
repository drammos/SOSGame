using SOS.Models;

namespace SOS.Services
{
    public interface IUpdateRepo
    {
        Task<bool> Update(Guid gid, string username, string password, string email, string filePath, int score, string theme);

        Task<bool> UpdateSettings(string username, int board, string level, int players);
    }
}
