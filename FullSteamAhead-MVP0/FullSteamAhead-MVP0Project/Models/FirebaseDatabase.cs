using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using UsernamePasswordProject.Models;

namespace UsernamePasswordProject.Models
{
    public class FirebaseDatabase : IDatabase
    {
        private readonly string ChildName = "Accounts";

        readonly FirebaseClient firebase;

        public FirebaseDatabase(string dbPath)
        {
            firebase = new FirebaseClient(dbPath);
        }

        public async Task<Account> GetAccountAsync(string userName)
        {
            var allPersons = await GetAccountsAsync();
            await firebase
                .Child(ChildName)
                .OnceAsync<Account>();
            return allPersons.FirstOrDefault(a => a.Username == userName);
        }

        public async Task<List<Account>> GetAccountsAsync()
        {
            return (await firebase
                .Child(ChildName)
                .OnceAsync<Account>()).Select(item => new Account
                {
                    Username = item.Object.Username, 
                    Password = item.Object.Password
                }).ToList();
        }

        public async Task SaveAccountAsync(Account account)
        {
            await firebase.Child(ChildName).PostAsync(account);
        }

        public async Task<bool> IsAccountValid(Account account)
        {
            Account account2 = await GetAccountAsync(account.Username);
            if (account2 == null || account2.Password != account.Password)
            {
                return false;
            }
            return true;
        }
    }
}