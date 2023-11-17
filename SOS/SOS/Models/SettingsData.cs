using SQLite;
using TableAttribute = SQLite.TableAttribute;

namespace SOS.Models
{
    [Table("settings")]
    public class  SettingsData
    {
        [PrimaryKey]
        public string Username { get; set; }
        public int Board {  get; set; }
        public string Level { get; set; }
        public int Players { get; set; }
    }
}
