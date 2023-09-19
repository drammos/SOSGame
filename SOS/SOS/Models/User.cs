using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOS.Models
{
    [Table("users")]
    public class User
    {
        [PrimaryKey]
        public string Guid { get; set; }
        [Unique]
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
