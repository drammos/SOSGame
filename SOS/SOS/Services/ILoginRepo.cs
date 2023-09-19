using SOS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOS.Services
{
    public interface ILoginRepo
    {
        Task<User> Login(string username, string password);

        Task<bool> IsValid(string username, string password);
    }
}
