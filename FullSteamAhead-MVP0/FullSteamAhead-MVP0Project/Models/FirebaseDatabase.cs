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
        private readonly string Members = "Members";
        private readonly string Mentors = "Mentors";
        private readonly string Team_Admins = "Team_Admins";










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
                    Password = item.Object.Password,
                    Nickname = item.Object.Nickname,
                    Information = item.Object.Information
                }).ToList();
        }

        public async Task SaveAccountAsync(User account)
        {
            await firebase.Child(Users).Child(account.Username).PutAsync(account);
        }

        public async Task UpdateAccount(User account)
        {
            await firebase.Child(Users).Child(account.Username).PatchAsync(account);
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

            // nickname or username: null issues
           
            List<User> users = allPersons.Where(a => (a.Username != null && a.Username.ToLower() == name) || (a.Nickname != null && a.Nickname.ToLower() == name)).ToList();

            return users;
        }
















        // Team methods

        public async Task<List<Team>> TeamSearch(string name)
        {
            var allPersons = await GetTeamsAsync();

            // nickname or username
            List<Team> teams = allPersons.Where(a => (a.Team_Nickname != null && a.Team_Nickname.ToLower() == name) || a.Team_Username.ToLower() == name).ToList();

            return teams;
        }

        public List<Team> FilterTeamGender(List<Team> teams, string gender) // cleans out based on gender
        {

            for (int i = 0; i < teams.Count; i++)
            {
                if (teams[i].Team_Information.Gender == gender)
                {
                    teams.RemoveAt(i);
                    i--;
                }
            }

            return teams;
        }

        public List<Team> FilterTeamCity(List<Team> teams, string city) // cleans out based on city
        {

            for (int i = 0; i < teams.Count; i++)
            {
                if (teams[i].Team_Information.City == city)
                {
                    teams.RemoveAt(i);
                    i--;
                }
            }

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

            Team team2 = allPersons.FirstOrDefault(a => a.Team_Username == team.Team_Username);
            if (team2 == null || team2.Team_Password != team.Team_Password)
            {
                return null;
            }

            Dictionary<string, Admin> allAdmins = team2.Team_Admins;
            foreach (KeyValuePair<string, Admin> entry in allAdmins)
            {
                if (entry.Key == admin.Username)
                {
                    if (entry.Value.Password != admin.Password)
                    {
                        return null;
                    }
                    return team;
                }
            }

            return null;
        }

        public async Task<int> TeamExists(Team team)
        {
            var allPersons = await GetTeamsAsync();
            // (0) username & password matches / (1) username matches / (2) neither
            Team team2 = allPersons.FirstOrDefault(a => a.Team_Username == team.Team_Username);
            if (team2 == null)
            {
                return 2;
            }
            if (team2.Team_Password == team.Team_Password)
            {
                return 0;
            }
            return 1;
        }

        public async Task<bool> TeamAdminExists(Team team, Admin admin)
        {
            // add method TeamAdminExists - checks if the username is already within the team
            var allPersons = await GetTeamsAsync();
            Team team2 = allPersons.FirstOrDefault(a => a.Team_Username == team.Team_Username);
            Dictionary<string, Admin> allAdmins = team2.Team_Admins;
            foreach (KeyValuePair<string, Admin> entry in allAdmins)
            {
                if (entry.Key == admin.Username)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task AddUser(Team team, User account)
        {
            if (account.Information.Role == "Mentor")
            {
                team.Mentors.Add(account.Username, account);
                await UpdateTeamMentors(team);
            } else if (account.Information.Role == "Student")
            {
                team.Members.Add(account.Username, account);
                await UpdateTeamMembers(team);
            }
        }

        public async Task UpdateTeamMembers(Team team)
        {
            await firebase.Child(Teams).Child(team.Team_Username).Child(Members).PatchAsync(team.Members);
        }

        public async Task UpdateTeamMentors(Team team)
        {
            await firebase.Child(Teams).Child(team.Team_Username).Child(Mentors).PatchAsync(team.Mentors);
        }

        public async Task AddTeamAdmin(Team team, Admin admin)
        {
            team.Team_Admins.Add(admin.Username, admin);
            await UpdateTeamAdmins(team);
        }

        public async Task UpdateTeamAdmins(Team team)
        {
            await firebase.Child(Teams).Child(team.Team_Username).Child(Team_Admins).PatchAsync(team.Team_Admins);
        }

    }
}