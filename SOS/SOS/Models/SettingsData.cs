using SQLite;
using TableAttribute = SQLite.TableAttribute;

namespace SOS.Models
{
    [Table("settings")]
    public class  SettingsData
    {
        [PrimaryKey]
        public string Email { get; set; }
        public int Board {  get; set; }
        public string Level { get; set; }
        public int Players { get; set; }
        
        public SettingsData(string email, int board, string level, int players) 
        {
            Email = email;
            Board = board;
            Level = level;
            Players = players;
        }

        public SettingsData() { }
    }
}
