using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace FullSteamAheadMVP0Project.Models
{
    /*
    public class LocalDatabase : IDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public LocalDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<User>().Wait();
        }

        public Task<User> GetAccountAsync(string userName)
        {
            // var existingItem = _database.GetAsync<Account>(userName); // ? not completely sure if this is how you do it
            // return existingItem;

            return _database.Table<User>()
                            .Where(i => i.Username == userName)
                            .FirstOrDefaultAsync();
        }

        public Task<List<User>> GetAccountsAsync()
        {
            return _database.Table<User>().ToListAsync();
        }

        public Task SaveAccountAsync(User account)
        {
            return _database.InsertAsync(account);
        }

        public Task<bool> IsAccountValid(User account)
        {
            throw new System.NotImplementedException();
        }
    }
    */
}