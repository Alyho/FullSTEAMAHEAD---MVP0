using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace UsernamePasswordProject.Models
{
    public class LocalDatabase : IDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public LocalDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Account>().Wait();
        }

        public Task<Account> GetAccountAsync(string userName)
        {
            // var existingItem = _database.GetAsync<Account>(userName); // ? not completely sure if this is how you do it
            // return existingItem;

            return _database.Table<Account>()
                            .Where(i => i.Username == userName)
                            .FirstOrDefaultAsync();
        }

        public Task<List<Account>> GetAccountsAsync()
        {
            return _database.Table<Account>().ToListAsync();
        }

        public Task SaveAccountAsync(Account account)
        {
            return _database.InsertAsync(account);
        }

        bool IDatabase.IsAccountValid(Account account)
        {
            throw new System.NotImplementedException();
        }
    }
}