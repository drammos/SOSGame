using SOS.Models;
using SQLite;


namespace SOS.Utils
{
    public class DBUtils
    {
        private static SQLiteAsyncConnection Database;

        private DBUtils() { }
        public static async Task<SQLiteAsyncConnection> GetDatabase()
        {
            if (Database == null)
            {
                Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
                await Database.CreateTableAsync<User>();
                await Database.CreateTableAsync<SettingsData>();
            }

            return Database;
        }
    }
}
