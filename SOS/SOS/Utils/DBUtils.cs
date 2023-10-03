using SOS.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                Debug.WriteLine("ela1111\n\n\n\n\n");
                await Database.CreateTableAsync<User>();
                Debug.WriteLine("\n\n\n\nela2222\n\n\n");
            }

            Debug.WriteLine("ela\n");
            return Database;
        }
    }
}
