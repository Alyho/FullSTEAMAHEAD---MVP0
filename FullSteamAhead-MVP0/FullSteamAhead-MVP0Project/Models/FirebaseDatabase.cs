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



        // Account methods

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
                    Username = item.Key, 
                    Password = item.Object.Password
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

        public async Task<Team> GetTeamAsync(string teamUserName)
        {
            var allPersons = await GetTeamsAsync();
            await firebase
                .Child(Teams)
                .OnceAsync<Team>();
            return allPersons.FirstOrDefault(a => a.Team_Username == teamUserName);
        }

        public async Task<List<Team>> GetTeamsAsync()
        {
            return (await firebase
                .Child(Teams)
                .OnceAsync<Team>()).Select(item => new Team
                {
                    Team_Username = item.Key,
                    Team_Password = item.Object.Team_Password
                }).ToList();
        }

        public async Task SaveTeamAsync(Team team)
        {
            await firebase.Child(Users).Child(team.Team_Username).PutAsync(team);
        }

        public async Task<bool> IsTeamValid(Team team)
        {
            Team team2 = await GetTeamAsync(team.Team_Username);
            if (team2 == null || team2.Team_Password != team.Team_Password)
            {
                return false;
            }
            return true;
        }

    }
}