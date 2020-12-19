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
        private readonly string Team_Members = "Team_Members";














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

        public async Task<List<User>> AccountSearch(string name)
        {
            var allPersons = await GetAccountsAsync();

            // nickname or username
            List<User> users = allPersons.Where(a => a.Username.ToLower() == name || a.Nickname.ToLower() == name).ToList();

            return users;
        }
















        // Team methods

        public async Task<List<Team>> TeamSearch(string name)
        {
            var allPersons = await GetTeamsAsync();

            //await firebase
            //    .Child(Teams)
            //    .OnceAsync<Team>();


            // nickname or username
            List<Team> teams = allPersons.Where(a => a.Team_Nickname.ToLower() == name || a.Team_Username.ToLower() == name).ToList();

            return teams;
        }

        public async Task<List<Team>> GetTeamsAsync()
        {
            return (await firebase
                .Child(Teams)
                .OnceAsync<Team>()).Select(item => new Team
                {
                    Team_Username = item.Key,
                    Team_Password = item.Object.Team_Password,
                    Team_Nickname = item.Object.Team_Nickname,
                    Team_Information = item.Object.Team_Information,
                    Team_Admins = item.Object.Team_Admins,
                    Members = item.Object.Members
                }).ToList();
        }

        public async Task SaveTeamAsync(Team team)
        {
            await firebase.Child(Teams).Child(team.Team_Username).PutAsync(team);
        }

        public async Task<Team> IsTeamValid(Team team, Admin admin)
        {
            var allPersons = await GetTeamsAsync();

            //await firebase
            //    .Child(Teams)
            //    .OnceAsync<Team>();

            Team team2 = allPersons.FirstOrDefault(a => a.Team_Username == team.Team_Username);
            if (team2 == null || team2.Team_Password != team.Team_Password)
            {
                return null;
            }

            List<Admin> allAdmins = team.Team_Admins;
            for (int i = 0; i < allAdmins.Count; i++)
            {
                if (allAdmins[i].Username == admin.Username)
                {
                    if (allAdmins[i].Password != admin.Password)
                    {
                        return null;
                    }
                    return team;
                }
            }

            return null;
        }

        public async Task<bool> TeamExists(Team team)
        {
            var allPersons = await GetTeamsAsync();

            //await firebase
            //    .Child(Teams)
            //    .OnceAsync<Team>();

            Team team2 = allPersons.FirstOrDefault(a => a.Team_Username == team.Team_Username);
            if (team2 == null)
            {
                return false;
            }

            return true;
        }

        public async Task AddTeamMember(Team team, User account)
        {
            team.Members.Add(account);
            await UpdateTeamMembers(team);
        }

        public async Task UpdateTeamMembers(Team team)
        {
            await firebase.Child(Teams).Child(team.Team_Username).PatchAsync(team.Members);
        }

        public async Task AddTeamAdmin(Team team, Admin admin)
        {
            team.Team_Admins.Add(admin);
            await UpdateTeamAdmins(team);
        }

        public async Task UpdateTeamAdmins(Team team)
        {
            await firebase.Child(Teams).Child(team.Team_Username).PatchAsync(team.Team_Admins);
        }

    }
}