using SOS.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOS.Services
{
    public class LoginService : ILoginRepo
    {
        public Task<Info> Login(string username, string password)
        {
            return null;
        }
    }
}
