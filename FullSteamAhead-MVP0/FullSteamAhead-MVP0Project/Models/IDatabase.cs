using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace FullSteamAheadMVP0Project.Models
{
    public interface IDatabase
    {
        Task<Account> GetAccountAsync(string userName);
        Task<List<Account>> GetAccountsAsync();
        Task SaveAccountAsync(Account account);
        Task<bool> IsAccountValid(Account account);
    }
}
