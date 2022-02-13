using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SQLite;

namespace FullSteamAheadMVP0Project.Models
{
    public interface IDatabase
    {
        // User methods
        Task<User> GetAccountAsync(string username);                   // returns the User that matches with the username
        Task<List<User>> GetAccountsAsync();                           // returns list of all Users in database
        Task SaveAccountAsync(User account);                           // saves User to database
        Task UpdateAccount(User account);                              // updates User in database
        Task UpdateUsername(User user, string username);               // updates User's username in database, deleting the old copy
        Task<bool> IsAccountValid(User account);                       // returns if User has created an account and if the password matches
        Task AddTeamRequest(User user, string username);               // adds team request to user
        Task RemoveTeamRequest(User user, string username);            // removes team request from user
        Task<Dictionary<string, Team>> GetTeamRequests(User user);     // returns team requests
        Task UpdateTeamRequests(User user);                            // updates Team_Requests list in database
        Task<bool> TeamRequestExists(User user, Team team);            // returns if Team username already exists in Team_Requests for a specific User
        Task<string> UploadUserFile(Stream fileStream, string username);
        Task<string> GetUserFile(string username);
        Task DeleteUserFile(string username);



        // Team methods
        Task<Team> GetTeamAsync(string username);                         // returns the Team that matches with the username
        Task UpdateTeam(Team team);                                       // updates team in database
        Task<Admin> GetAdminAsync(string username);                       // returns the Admin that matches with the username
        Task<List<Team>> GetTeamsAsync();                                 // returns list of all Teams in database
        Task SaveTeamAsync(Team team);                                    // saves Team to database
        Task UpdateTeamUsername(Team team, string username);              // updates Team's username in database, deleting the old copy
        Task AddUser(Team team, User account);                            // adds User to Team, either in Mentors list or Students list
        Task RemoveUser(Team team, User account);                         // removes User from Team, either in Mentors list or Students list
        Task<Dictionary<string, User>> GetTeamMentors(Team team);         // gets mentors in the team
        Task<Dictionary<string, User>> GetTeamStudents(Team team);        // gets students in the team
        Task UpdateTeamStudents(Team team);                               // updates Students list in database
        Task UpdateTeamMentors(Team team);                                // updates Mentors list in database
        Task AddTeamAdmin(Team team, Admin admin);                        // adds Admin to Team, through the Team_Admins list
        Task RemoveTeamAdmin(Team team, Admin admin);                     // removes Admin from Team, through the Team_Admins list
        Task UpdateTeamAdmins(Team team);                                 // updates Team_Admins list in database
        Task<Team> IsTeamValid(Team team, Admin admin);                   // returns Team if it exists in database + password matches + admin user and password matches, otherwise returns null
        Task<int> TeamExists(Team team);                                  // returns an integer - given a Team, checks with database: (0) username & password matches / (1) username matches / (2) neither
        Task<bool> TeamAdminExists(Team team, Admin admin);               // returns if Admin username already exists within Team
        Task<bool> TeamUserExists(Team team, string username);            // returns if User username already exists within Team
        Task AddAnnouncement(Team team, string announcement);             // adds announcement to beginning of list
        Task RemoveAnnouncement(Team team, int index);                    // removes announcement based on index
        Task<Dictionary<string, string>> GetAnnouncements(Team team);     // returns team announcements
        Task UpdateAnnouncements(Team team);                              // updates Announcements list in database
        Task AddUserRequest(Team team, string username);                  // adds user request to team
        Task RemoveUserRequest(Team team, string username);               // removes user request from team
        Task<Dictionary<string, User>> GetUserRequests(Team team);        // returns user requests
        Task UpdateUserRequests(Team team);                               // updates User_Requests list in database
        Task<bool> UserRequestExists(Team team, User user);               // returns if User username already exists in User_Requests for a specific Team
        Task<string> UploadTeamFile(Stream fileStream, string username);
        Task<string> GetTeamFile(string username);
        Task DeleteTeamFile(string username);
        Task<string> UploadTeamAdminFile(Stream fileStream, string teamusername, string adminusername);
        Task<string> GetTeamAdminFile(string teamusername, string adminusername);
        Task DeleteTeamAdminFile(string teamusername, string adminusername);



        // User searching and filtering methods
        Task<List<User>> AccountSearch(string name, Team team);                               // returns list of Users that match the name (either username or nickname)
        List<User> FilterBestAccountResults(List<User> users, Team team);                     // cleans out users for best results that match with the team variable
        List<User> FilterAccountGender(List<User> users, string gender);                      // cleans out users based on gender
        List<User> FilterAccountCity(List<User> users, string city, string state);            // cleans out users based on city AND state
        List<User> FilterAccountState(List<User> users, string state);                        // cleans out users based on state
        List<User> FilterAccountPrivacy(List<User> users);                                    // cleans out private users
        List<User> FilterAccountAge(List<User> users, string teamMinAge, string teamMaxAge);  // cleans out users based on age
        // Note: this is the only method not called in FilterBestAccountResults()
        List<User> FilterAccountRole(List<User> users, string role);                          // cleans out users based on role (student / mentor)



        // Team searching and filtering methods
        Task<List<Team>> TeamSearch(string name, User user);                     // returns list of Teams that match the name (either username or nickname)
        List<Team> FilterBestTeamResults(List<Team> teams, User user);           // cleans out teams for best results that match with the user variable
        List<Team> GetOnlineTeams(List<Team> teams);                             // gets all online teams
        List<Team> FilterTeamGender(List<Team> teams, string gender);            // cleans out teams based on gender
        List<Team> FilterTeamCity(List<Team> teams, string city, string state);  // cleans out teams based on city AND state
        List<Team> FilterTeamState(List<Team> teams, string state);              // cleans out teams based on state
        List<Team> FilterTeamPrivacy(List<Team> teams);                          // cleans out private teams
        List<Team> FilterTeamAge(List<Team> teams, string age);                  // cleans out teams based on age
    }
}
