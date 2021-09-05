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
        private readonly string Students = "Students";
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











        // User methods

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
                    Email = item.Object.Email
                }).ToList();
        }

        public async Task SaveAccountAsync(User user) // saves User to database
        {
            await firebase.Child(Users).Child(user.Username).PutAsync(user);
        }

        public async Task UpdateAccount(User user) // updates User in database
        {
            await firebase.Child(Users).Child(user.Username).PatchAsync(user);
        }

        public async Task UpdateUsername(User user, string username) // updates User's username in database, deleting the old copy
        {
            await firebase.Child(Users).Child(user.Username).DeleteAsync();
            user.Username = username;
            await SaveAccountAsync(user);
        }

        public async Task<bool> IsAccountValid(User user) // returns if User has created an account and if the password matches
        {
            User user2 = await GetAccountAsync(user.Username);
            if (user2 == null || user2.Password != user.Password)
            {
                return false;
            }
            return true;
        }

        public async Task AddTeamRequest(User user, string username)
        {
            await firebase.Child(Users).Child(user.Username).Child(Team_Requests).PutAsync(username);
        }

        public async Task RemoveTeamRequest(User user, string username)
        {
            await firebase.Child(Users).Child(user.Username).Child(Team_Requests).Child(username).DeleteAsync();
        }

        public async Task<Dictionary<string, Team>> GetTeamRequests(User user)
        {
            List<string> usernames = (await firebase.Child(Users).Child(user.Username).Child(Team_Requests).OnceAsync<string>()).Select(item => item.Key).ToList();
            Dictionary<string, Team> teams = new Dictionary<string, Team>();
            for (int i = 0; i < usernames.Count; i++)
            {
                string team = usernames[i];
                teams.Add(team, await GetTeamAsync(team));
            }
            return teams;
        }

        public async Task<bool> TeamRequestExists(User user, Team team)
        {
            Dictionary<string, Team> allTeamRequests = await GetTeamRequests(user);
            return allTeamRequests.ContainsKey(team.Team_Username);
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

        public async Task UpdateTeam(Team team) // updates Team in database
        {
            await firebase.Child(Teams).Child(team.Team_Username).PatchAsync(team);
        }

        public async Task UpdateTeamUsername(Team team, string username) // updates Team's username in database, deleting the old copy
        {
            await firebase.Child(Teams).Child(team.Team_Username).DeleteAsync();
            team.Team_Username = username;
            await SaveTeamAsync(team);
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
                    Announcements = item.Object.Announcements
                }).ToList();
        }

        public async Task SaveTeamAsync(Team team) // saves Team to database
        {
            await firebase.Child(Teams).Child(team.Team_Username).PutAsync(team);
        }

        public async Task AddUser(Team team, User user) // adds User to Team, either in Mentors list or Students list
        {
            if (user.Information.Role == "Mentor")
            {
                await firebase.Child(Teams).Child(team.Team_Username).Child(Mentors).PutAsync(user.Username);
            }
            else if (user.Information.Role == "Student")
            {
                await firebase.Child(Teams).Child(team.Team_Username).Child(Students).PutAsync(user.Username);
            }
        }

        public async Task RemoveUser(Team team, User user) // removes User from Team, either in Mentors list or Students list
        {
            if (user.Information.Role == "Mentor")
            {
                await firebase.Child(Teams).Child(team.Team_Username).Child(Mentors).Child(user.Username).DeleteAsync();
            }
            else if (user.Information.Role == "Student")
            {
                await firebase.Child(Teams).Child(team.Team_Username).Child(Students).Child(user.Username).DeleteAsync();
            }
        }

        public async Task<Dictionary<string, User>> GetMentors(Team team) // gets mentors in the team
        {
            List<string> mentornames = (await firebase.Child(Teams).Child(team.Team_Username).Child(Mentors).OnceAsync<string>()).Select(item => item.Key).ToList();
            Dictionary<string, User> mentors = new Dictionary<string, User>();
            for (int i = 0; i < mentornames.Count; i++)
            {
                string mentor = mentornames[i];
                mentors.Add(mentor, await GetAccountAsync(mentor));
            }
            return mentors;
        }

        public async Task<Dictionary<string, User>> GetStudents(Team team) // gets students in the team
        {
            List<string> studentnames = (await firebase.Child(Teams).Child(team.Team_Username).Child(Students).OnceAsync<string>()).Select(item => item.Key).ToList();
            Dictionary<string, User> students = new Dictionary<string, User>();
            for (int i = 0; i < studentnames.Count; i++)
            {
                string student = studentnames[i];
                students.Add(student, await GetAccountAsync(student));
            }
            return students;
        }

        public async Task AddTeamAdmin(Team team, Admin admin) // adds Admin to Team, through the Team_Admins list
        {
            team.Team_Admins.Add(admin.Username, admin);
            await UpdateTeamAdmins(team);
        }

        public async Task RemoveTeamAdmin(Team team, Admin admin) // removes Admin from Team, through the Team_Admins list
        {
            team.Team_Admins.Remove(admin.Username);
            await UpdateTeamAdmins(team);
        }

        public async Task UpdateTeamAdmins(Team team) // updates Team_Admins list in database
        {
            await firebase.Child(Teams).Child(team.Team_Username).Child(Team_Admins).DeleteAsync();
            await firebase.Child(Teams).Child(team.Team_Username).Child(Team_Admins).PutAsync(team.Team_Admins);
        }

        public async Task<Team> IsTeamValid(Team team, Admin admin) // returns Team if it exists in database + password matches + admin user and password matches, otherwise returns null
        {
            Team team2 = await GetTeamAsync(team.Team_Username);
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
            Team team2 = await GetTeamAsync(team.Team_Username);
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
            Team team2 = await GetTeamAsync(team.Team_Username);
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

        public async Task<bool> TeamUserExists(Team team, string username) // returns if User username already exists within Team
        {
            Dictionary<string, User> allMentors = await GetMentors(team);
            Dictionary<string, User> allStudents = await GetStudents(team);
            return allMentors.ContainsKey(username) || allStudents.ContainsKey(username);
        }

        public async Task AddAnnouncement(Team team, string announcement)
        {
            Team team2 = await GetTeamAsync(team.Team_Username);
            int maxIndex = 0;
            foreach (KeyValuePair<string, string> entry in team2.Announcements)
            {
                int cur = Int32.Parse(entry.Key.Substring(0, entry.Key.Length-1));
                if (cur > maxIndex)
                {
                    maxIndex = cur;
                }
            }
            team2.Announcements.Add((maxIndex+1) + "_", announcement);
            await UpdateAnnouncements(team2);
        }

        public async Task RemoveAnnouncement(Team team, int index)
        {
            Team team2 = await GetTeamAsync(team.Team_Username);
            int curInd = 0;
            foreach (KeyValuePair<string, string> entry in team2.Announcements)
            {
                if (index == curInd)
                {
                    team2.Announcements.Remove(entry.Key);
                    await UpdateAnnouncements(team2);
                    return;
                }
                curInd++;
            }
        }

        public async Task<Dictionary<string, string>> GetAnnouncements(Team team)
        {
            Team team2 = await GetTeamAsync(team.Team_Username);
            return team2.Announcements;
        }

        public async Task UpdateAnnouncements(Team team)
        {
            await firebase.Child(Teams).Child(team.Team_Username).Child(Announcements).DeleteAsync();
            await firebase.Child(Teams).Child(team.Team_Username).Child(Announcements).PutAsync(team.Announcements);
        }

        public async Task AddUserRequest(Team team, string username)
        {
            await firebase.Child(Teams).Child(team.Team_Username).Child(User_Requests).PutAsync(username);
        }

        public async Task RemoveUserRequest(Team team, string username)
        {
            await firebase.Child(Teams).Child(team.Team_Username).Child(User_Requests).Child(username).DeleteAsync();
        }

        public async Task<Dictionary<string, User>> GetUserRequests(Team team)
        {
            List<string> usernames = (await firebase.Child(Teams).Child(team.Team_Username).Child(User_Requests).OnceAsync<string>()).Select(item => item.Key).ToList();
            Dictionary<string, User> users = new Dictionary<string, User>();
            for (int i = 0; i < usernames.Count; i++) 
            {
                string user = usernames[i];
                users.Add(user, await GetAccountAsync(user));
            }
            return users;
        }

        public async Task<bool> UserRequestExists(Team team, User user)
        {
            Dictionary<string, User> allUserRequests = await GetUserRequests(team);
            return allUserRequests.ContainsKey(user.Username);
        }



















        // User searching and filtering methods

        public async Task<List<User>> AccountSearch(string name) // returns list of Users that match the name (either username or nickname)
        {
            var allPersons = await GetAccountsAsync();
            name = name.ToLower();
            List<User> users = allPersons.Where(a => (a.Username != null && a.Username.ToLower() == name) || (a.Nickname != null && a.Nickname.ToLower() == name)).ToList();
            return users;
        }

        public List<User> FilterBestAccountResults(List<User> users, Team team) // cleans out users for best results that match with the team variable
        {

            users = FilterAccountPrivacy(users);

            Dictionary<User, int> matches = new Dictionary<User, int>();
            int max = 0;

            for (int i = 0; i < users.Count; i++)
            {
                int count = 0;

                // gender
                string userGender = users[i].Information.Preferences.Gender.ToLower();
                string gender = team.Team_Information.Gender.ToLower();
                if (userGender == gender || userGender == "all genders")
                {
                    count++;
                }

                // city
                string userCity = users[i].Information.City.ToLower();
                string city = team.Team_Information.City.ToLower();
                string userState = users[i].Information.State.ToLower();
                string state = team.Team_Information.State.ToLower();
                if (userCity == city || userState == state)
                {
                    count++;
                }

                // age
                string teamMinAge = team.Team_Information.Min_Age;
                string teamMaxAge = team.Team_Information.Max_Age;
                if (!(string.IsNullOrWhiteSpace(teamMinAge) || string.IsNullOrWhiteSpace(teamMaxAge)))
                {
                    int minAge = Int32.Parse(teamMinAge);
                    int maxAge = Int32.Parse(teamMaxAge);
                    int userAge = Int32.Parse(users[i].Information.Age);
                    if (userAge >= minAge && userAge <= maxAge)
                    {
                        count++;
                    }
                }

                max = Math.Max(count, max);
                matches.Add(users[i], count);
            }

            List<User> newUsers = new List<User>();
            for (int i = max; i > 0; i--)
            {
                for (int j = 0; j < users.Count; j++)
                {
                    if (matches[users[j]] == i)
                    {
                        newUsers.Add(users[j]);
                    }
                }
            }

            users = newUsers;
            return users;
        }

        public List<User> FilterAccountGender(List<User> users, string gender) // cleans out users based on gender
        {
            for (int i = 0; i < users.Count; i++)
            {
                string userGender = users[i].Information.Preferences.Gender.ToLower();
                gender = gender.ToLower();
                if (!(userGender == gender || userGender == "all genders"))
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
                string userCity = users[i].Information.City.ToLower();
                city = city.ToLower();
                string userState = users[i].Information.State.ToLower();
                state = state.ToLower();
                if (userCity != city || userState != state)
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
                if (users[i].Information.State.ToLower() != state.ToLower())
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
                string privacy = users[i].Information.Preferences.Privacy.ToLower();
                if (privacy == "private")
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
        public List<User> FilterAccountRole(List<User> users, string role) // cleans out users based on role (student / mentor)
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








        // Team searching and filtering methods

        public async Task<List<Team>> TeamSearch(string name) // returns list of Teams that match the name (either username or nickname)
        {
            var allPersons = await GetTeamsAsync();
            name = name.ToLower();
            List<Team> teams = allPersons.Where(a => (a.Team_Nickname != null && a.Team_Nickname.ToLower() == name) || a.Team_Username.ToLower() == name).ToList();
            return teams;
        }

        public List<Team> FilterBestTeamResults(List<Team> teams, User user) // cleans out teams for best results that match with the user variable
        {
            teams = FilterTeamPrivacy(teams);

            Dictionary<Team, int> matches = new Dictionary<Team, int>();
            int max = 0;
            for (int i = 0; i < teams.Count; i++)
            {
                int count = 0;

                // gender
                string teamGender = teams[i].Team_Information.Gender.ToLower();
                string gender = user.Information.Preferences.Gender.ToLower();
                if (teamGender == gender || teamGender == "all genders")
                {
                    count++;
                }


                // city
                string teamCity = teams[i].Team_Information.City.ToLower();
                string city = user.Information.City.ToLower();
                string teamState = teams[i].Team_Information.State.ToLower();
                string state = user.Information.State.ToLower();
                if (teamCity == city || teamState == state)
                {
                    count++;
                }


                // age
                string teamMinAge = teams[i].Team_Information.Min_Age;
                string teamMaxAge = teams[i].Team_Information.Max_Age;
                if (!(string.IsNullOrWhiteSpace(teamMinAge) || string.IsNullOrWhiteSpace(teamMaxAge)))
                {
                    int minAge = Int32.Parse(teamMinAge);
                    int maxAge = Int32.Parse(teamMaxAge);
                    int userAge = Int32.Parse(user.Information.Age);
                    if (userAge >= minAge && userAge <= maxAge)
                    {
                        count++;
                    }
                }


                max = Math.Max(count, max);
                matches.Add(teams[i], count);
            }

            List<Team> newTeams = new List<Team>();
            for (int i = max; i > 0; i--)
            {
                for (int j = 0; j < teams.Count; j++)
                {
                    if (matches[teams[j]] == i)
                    {
                        newTeams.Add(teams[j]);
                    }
                }
            }

            teams = newTeams;
            return teams;
        }

        public List<Team> FilterTeamGender(List<Team> teams, string gender) // cleans out teams based on gender
        {
            for (int i = 0; i < teams.Count; i++)
            {
                string teamGender = teams[i].Team_Information.Gender.ToLower();
                gender = gender.ToLower();
                if (!(teamGender == gender || teamGender == "all genders"))
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
                string teamCity = teams[i].Team_Information.City.ToLower();
                city = city.ToLower();
                string teamState = teams[i].Team_Information.State.ToLower();
                state = state.ToLower();
                if (teamCity != city || teamState != state)
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
                if (teams[i].Team_Information.State.ToLower() != state.ToLower())
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
                if (teams[i].Team_Information.Privacy.ToLower() == "private")
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
                string teamMinAge = teams[i].Team_Information.Min_Age;
                string teamMaxAge = teams[i].Team_Information.Max_Age;
                if (!(string.IsNullOrWhiteSpace(teamMinAge) || string.IsNullOrWhiteSpace(teamMaxAge)))
                {
                    minAge = Int32.Parse(teamMinAge);
                    maxAge = Int32.Parse(teamMaxAge);
                    if (userAge < minAge || userAge > maxAge)
                    {
                        teams.RemoveAt(i);
                        i--;
                    }
                }
            }
            return teams;
        }












    }
}