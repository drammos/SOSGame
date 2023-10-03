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
        Task<User> IsValid(string username, string password);
    }
}
