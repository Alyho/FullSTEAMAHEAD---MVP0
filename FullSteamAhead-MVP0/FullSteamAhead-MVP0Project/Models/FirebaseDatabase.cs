using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;

namespace FullSteamAheadMVP0Project.Models
{
    public class FirebaseDatabase : IDatabase
    {


        private readonly string Users = "Users";
        private readonly string Teams = "Teams";
        private readonly string TeamAdmins = "Team_Admins";
        private readonly string TeamMembers = "Team_Members";




        readonly FirebaseClient firebase;

        public FirebaseDatabase(string dbPath)
        {
            firebase = new FirebaseClient(dbPath);
        }




        public async Task<User> GetAccountAsync(string username)
        {
            var allPersons = await GetAccountsAsync();
            await firebase
                .Child(Users)
                .OnceAsync<User>();
            return allPersons.FirstOrDefault(a => a.Username == username);
        }

        public async Task<List<User>> GetAccountsAsync()
        {
            return (await firebase
                .Child(Users)
                .OnceAsync<User>()).Select(item => new User
                {
                    Username = item.Object.Username, 
                    Password = item.Object.Password,
                    Information = item.Object.Information
                }).ToList();
        }

        public async Task SaveAccountAsync(User account)
        {
            await firebase.Child(Users).Child(account.Username).PutAsync(account);
        }

        public async Task<bool> IsAccountValid(User account)
        {
            User account2 = await GetAccountAsync(account.Username);
            if (account2 == null || account2.Password != account.Password)
            {
                return false;
            }
            return true;
        }




        // Team methods


    }
}