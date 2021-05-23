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
        private readonly string Announcements = "Announcements";
        private readonly string User_Requests = "User_Requests";
        private readonly string Team_Requests = "Team_Requests";










        readonly FirebaseClient firebase;

        public FirebaseDatabase(string dbPath)
        {
            firebase = new FirebaseClient(dbPath);
        }











        // Account methods

        public async Task<User> GetAccountAsync(string username) // returns the User that matches with the username
        {
            var allPersons = await GetAccountsAsync();
            await firebase
                .Child(Users)
                .OnceAsync<User>();
            return allPersons.FirstOrDefault(a => a.Username == username);
        }

        public async Task<List<User>> GetAccountsAsync() // returns list of all Users in database
        {
            return (await firebase
                .Child(Users)
                .OnceAsync<User>()).Select(item => new User
                {
                    Username = item.Key,
                    Password = item.Object.Password,
                    Nickname = item.Object.Nickname,
                    Information = item.Object.Information,
                    Email = item.Object.Email,
                    Team_Requests = item.Object.Team_Requests
                }).ToList();
        }

        public async Task SaveAccountAsync(User account) // saves User to database
        {
            await firebase.Child(Users).Child(account.Username).PutAsync(account);
        }

        public async Task UpdateAccount(User account) // updates User in database
        {
            await firebase.Child(Users).Child(account.Username).PatchAsync(account);
        }

        public async Task<bool> IsAccountValid(User account) // returns if User has created an account and if the password matches
        {
            User account2 = await GetAccountAsync(account.Username);
            if (account2 == null || account2.Password != account.Password)
            {
                return false;
            }
            return true;
        }

        public async Task AddTeamRequest(User user, string username)
        {
            Team team = await GetTeamAsync(username);
            user.Team_Requests.Add(username, team);
            await UpdateTeamRequests(user);
        }

        public async Task RemoveTeamRequest(User user, string username)
        {
            user.Team_Requests.Remove(username);
            await UpdateTeamRequests(user);
        }

        public async Task<Dictionary<string, Team>> GetTeamRequests(User user)
        {
            var allPersons = await GetAccountsAsync();
            User account2 = allPersons.FirstOrDefault(a => a.Username == user.Username);
            return account2.Team_Requests;
        }

        public async Task UpdateTeamRequests(User user)
        {
            await firebase.Child(Users).Child(user.Username).Child(Team_Requests).PatchAsync(user.Team_Requests);
        }


















        // Account searching and filtering methods

        public async Task<List<User>> AccountSearch(string name) // returns list of Users that match the name (either username or nickname)
        {
            var allPersons = await GetAccountsAsync();
            // nickname or username: null issues
            List<User> users = allPersons.Where(a => (a.Username != null && a.Username.ToLower() == name) || (a.Nickname != null && a.Nickname.ToLower() == name)).ToList();
            return users;
        }

        public List<User> FilterBestAccountResults(List<User> users, Team team) // cleans out users for best results that match with the team variable
        {
            users = FilterAccountGender(users, team.Team_Information.Gender);
            users = FilterAccountCity(users, team.Team_Information.City, team.Team_Information.State);
            users = FilterAccountPrivacy(users);
            users = FilterAccountAge(users, team.Team_Information.Min_Age, team.Team_Information.Max_Age);
            return users;
        }

        public List<User> FilterAccountGender(List<User> users, string gender) // cleans out users based on gender
        {
            for (int i = 0; i < users.Count; i++)
            {
                if (!(users[i].Information.Preferences.Gender == gender || users[i].Information.Preferences.Gender == "All Genders"))
                {
                    users.RemoveAt(i);
                    i--;
                }
            }
            return users;
        }

        public List<User> FilterAccountCity(List<User> users, string city, string state) // cleans out users based on city AND state
        {
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Information.City != city || users[i].Information.State != state)
                {
                    users.RemoveAt(i);
                    i--;
                }
            }
            return users;
        }

        public List<User> FilterAccountState(List<User> users, string state) // cleans out users based on state
        {
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Information.State != state)
                {
                    users.RemoveAt(i);
                    i--;
                }
            }
            return users;
        }

        public List<User> FilterAccountPrivacy(List<User> users) // cleans out private users
        {
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Information.Preferences.Privacy == "Private")
                {
                    users.RemoveAt(i);
                    i--;
                }
            }
            return users;
        }

        public List<User> FilterAccountAge(List<User> users, string teamMinAge, string teamMaxAge) // cleans out users based on age
        {
            if (string.IsNullOrWhiteSpace(teamMinAge) || string.IsNullOrWhiteSpace(teamMaxAge))
            {
                return users;
            }

            int userAge;
            int minAge = Int32.Parse(teamMinAge);
            int maxAge = Int32.Parse(teamMaxAge);
            for (int i = 0; i < users.Count; i++)
            {
                userAge = Int32.Parse(users[i].Information.Age);
                if (userAge < minAge || userAge > maxAge)
                {
                    users.RemoveAt(i);
                    i--;
                }
            }
            return users;
        }

        // Note: this is the only method not called in FilterBestAccountResults()
        public List<User> FilterAccountRole(List<User> users, string role) // cleans out users based on role (member / mentor)
        {
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Information.Role != role)
                {
                    users.RemoveAt(i);
                    i--;
                }
            }
            return users;
        }


















        // Team methods

        public async Task<Team> GetTeamAsync(string username) // returns the Team that matches with the username
        {
            var teams = await GetTeamsAsync();
            await firebase
                .Child(Teams)
                .OnceAsync<Team>();
            return teams.FirstOrDefault(a => a.Team_Username == username);
        }

        public async Task<Admin> GetAdminAsync(string username) // returns the Admin that matches with the username
        {
            List<Team> teams = await GetTeamsAsync();
            for (int i = 0; i < teams.Count; i++)
            {
                Team team = teams[i];
                Dictionary<string, Admin> admins = team.Team_Admins;
                foreach (KeyValuePair<string, Admin> entry in admins)
                {
                    if (entry.Key == username)
                    {
                        return entry.Value;
                    }
                }
            }
            return null;
        }

        public async Task<List<Team>> GetTeamsAsync() // returns list of all Teams in database
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
                    Members = item.Object.Members,
                    Mentors = item.Object.Mentors,
                    Announcements = item.Object.Announcements,
                    User_Requests = item.Object.User_Requests
                }).ToList();
        }

        public async Task SaveTeamAsync(Team team) // saves Team to database
        {
            await firebase.Child(Teams).Child(team.Team_Username).PutAsync(team);
        }

        public async Task AddUser(Team team, User account) // adds User to Team, either in Mentors list or Members list
        {
            if (account.Information.Role == "Mentor")
            {
                team.Mentors.Add(account.Username, account);
                await UpdateTeamMentors(team);
            }
            else if (account.Information.Role == "Student")
            {
                team.Members.Add(account.Username, account);
                await UpdateTeamMembers(team);
            }
        }

        public async Task AddTeamAdmin(Team team, Admin admin) // adds Admin to Team, through the Team_Admins list
        {
            team.Team_Admins.Add(admin.Username, admin);
            await UpdateTeamAdmins(team);
        }

        public async Task UpdateTeamMembers(Team team) // updates Members list in database
        {
            await firebase.Child(Teams).Child(team.Team_Username).Child(Members).PatchAsync(team.Members);
        }

        public async Task UpdateTeamMentors(Team team) // updates Mentors list in database
        {
            await firebase.Child(Teams).Child(team.Team_Username).Child(Mentors).PatchAsync(team.Mentors);
        }

        public async Task UpdateTeamAdmins(Team team) // updates Team_Admins list in database
        {
            await firebase.Child(Teams).Child(team.Team_Username).Child(Team_Admins).PatchAsync(team.Team_Admins);
        }

        public async Task<Team> IsTeamValid(Team team, Admin admin) // returns Team if it exists in database + password matches + admin user and password matches, otherwise returns null
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

        public async Task<int> TeamExists(Team team) // returns an integer - given a Team, checks with database: (0) username & password matches / (1) username matches / (2) neither
        {
            var allPersons = await GetTeamsAsync();
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

        public async Task<bool> TeamAdminExists(Team team, Admin admin) // returns if Admin username already exists within Team
        {
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

        public async Task AddAnnouncement(Team team, string announcement)
        {
            int maxIndex = 0;
            foreach (KeyValuePair<string, string> entry in team.Announcements)
            {
                int cur = Int32.Parse(entry.Key);
                if (cur > maxIndex)
                {
                    maxIndex = cur;
                }
            }
            team.Announcements.Add((maxIndex+1) + "", announcement);
            await UpdateAnnouncements(team);
        }

        public async Task RemoveAnnouncement(Team team, int index)
        {
            int curInd = 0;
            foreach (KeyValuePair<string, string> entry in team.Announcements)
            {
                if (index == curInd)
                {
                    team.Announcements.Remove(entry.Key);
                    await UpdateAnnouncements(team);
                    return;
                }
                curInd++;
            }
        }

        public async Task<Dictionary<string, string>> GetAnnouncements(Team team)
        {
            var allPersons = await GetTeamsAsync();
            Team team2 = allPersons.FirstOrDefault(a => a.Team_Username == team.Team_Username);
            return team2.Announcements;
        }

        public async Task UpdateAnnouncements(Team team)
        {
            await firebase.Child(Teams).Child(team.Team_Username).Child(Announcements).PatchAsync(team.Announcements);
        }

        public async Task AddUserRequest(Team team, string username)
        {
            User user = await GetAccountAsync(username);
            team.User_Requests.Add(username, user);
            await UpdateUserRequests(team);
        }

        public async Task RemoveUserRequest(Team team, string username)
        {
            team.User_Requests.Remove(username);
            await UpdateUserRequests(team);
        }

        public async Task<Dictionary<string, User>> GetUserRequests(Team team)
        {
            var allPersons = await GetTeamsAsync();
            Team team2 = allPersons.FirstOrDefault(a => a.Team_Username == team.Team_Username);
            return team2.User_Requests;
        }

        public async Task UpdateUserRequests(Team team)
        {
            await firebase.Child(Teams).Child(team.Team_Username).Child(User_Requests).PatchAsync(team.User_Requests);
        }


















        // Team searching and filtering methods

        public async Task<List<Team>> TeamSearch(string name) // returns list of Teams that match the name (either username or nickname)
        {
            var allPersons = await GetTeamsAsync();
            // nickname or username
            List<Team> teams = allPersons.Where(a => (a.Team_Nickname != null && a.Team_Nickname.ToLower() == name) || a.Team_Username.ToLower() == name).ToList();
            return teams;
        }

        public List<Team> FilterBestTeamResults(List<Team> teams, User user) // cleans out teams for best results that match with the user variable
        {
            teams = FilterTeamGender(teams, user.Information.Preferences.Gender);
            teams = FilterTeamCity(teams, user.Information.City, user.Information.State);
            teams = FilterTeamPrivacy(teams);
            teams = FilterTeamAge(teams, user.Information.Age);
            return teams;
        }

        public List<Team> FilterTeamGender(List<Team> teams, string gender) // cleans out teams based on gender
        {
            for (int i = 0; i < teams.Count; i++)
            {
                if (!(teams[i].Team_Information.Gender == gender || teams[i].Team_Information.Gender == "All Genders"))
                {
                    teams.RemoveAt(i);
                    i--;
                }
            }
            return teams;
        }

        public List<Team> FilterTeamCity(List<Team> teams, string city, string state) // cleans out teams based on city AND state
        {
            for (int i = 0; i < teams.Count; i++)
            {
                if (teams[i].Team_Information.City != city || teams[i].Team_Information.State != state)
                {
                    teams.RemoveAt(i);
                    i--;
                }
            }
            return teams;
        }

        public List<Team> FilterTeamState(List<Team> teams, string state) // cleans out teams based on state
        {
            for (int i = 0; i < teams.Count; i++)
            {
                if (teams[i].Team_Information.State != state)
                {
                    teams.RemoveAt(i);
                    i--;
                }
            }
            return teams;
        }

        public List<Team> FilterTeamPrivacy(List<Team> teams) // cleans out private teams
        {
            for (int i = 0; i < teams.Count; i++)
            {
                if (teams[i].Team_Information.Privacy == "Private")
                {
                    teams.RemoveAt(i);
                    i--;
                }
            }
            return teams;
        }

        public List<Team> FilterTeamAge(List<Team> teams, string age) // cleans out teams based on age
        {
            int minAge, maxAge;
            int userAge = Int32.Parse(age);
            for (int i = 0; i < teams.Count; i++)
            {
                minAge = Int32.Parse(teams[i].Team_Information.Min_Age);
                maxAge = Int32.Parse(teams[i].Team_Information.Max_Age);
                if (userAge < minAge || userAge > maxAge)
                {
                    teams.RemoveAt(i);
                    i--;
                }
            }
            return teams;
        }












    }
}