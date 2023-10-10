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
                await Database.CreateTableAsync<User>();
            }

            return Database;
        }
    }
}
