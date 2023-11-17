using SQLite;
using TableAttribute = SQLite.TableAttribute;

namespace SOS.Models
{
    [Table("users")]
    public class User
    {
        [PrimaryKey]
        public Guid Gid { get; set; }
        [Unique]
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FilePath { get; set; }
        public int Score { get; set; }
        public string Theme {  get; set; }
    }
}
