using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace FullSteamAheadMVP0Project.Models
{
    public interface IDatabase
    {
        // Account methods
        Task<User> GetAccountAsync(string username);        // returns the User that matches with the username
        Task<List<User>> GetAccountsAsync();                // returns list of all Users in database
        Task SaveAccountAsync(User account);                // saves User to database
        Task UpdateAccount(User account);                   // updates User in database
        Task<bool> IsAccountValid(User account);            // returns if User has created an account and if the password matches
        Task AddTeamRequest(User user, string username);    // adds team request to user
        Task RemoveTeamRequest(User user, string username); // removes team request from user
        Task<List<string>> GetTeamRequests(User user);      // returns team requests
        Task UpdateTeamRequests(User user);                 // updates Team_Requests list in database

        // Account searching and filtering methods
        Task<List<User>> AccountSearch(string name);                                          // returns list of Users that match the name (either username or nickname)
        List<User> FilterBestAccountResults(List<User> users, Team team);                     // cleans out users for best results that match with the team variable
        List<User> FilterAccountGender(List<User> users, string gender);                      // cleans out users based on gender
        List<User> FilterAccountCity(List<User> users, string city, string state);            // cleans out users based on city AND state
        List<User> FilterAccountState(List<User> users, string state);                        // cleans out users based on state
        List<User> FilterAccountPrivacy(List<User> users);                                    // cleans out private users
        List<User> FilterAccountAge(List<User> users, string teamMinAge, string teamMaxAge);  // cleans out users based on age
        // Note: this is the only method not called in FilterBestAccountResults()
        List<User> FilterAccountRole(List<User> users, string role);                          // cleans out users based on role (member / mentor)

        // Team methods
        Task<Team> GetTeamAsync(string username);             // returns the Team that matches with the username
        Task<Admin> GetAdminAsync(string username);           // returns the Admin that matches with the username
        Task<List<Team>> GetTeamsAsync();                     // returns list of all Teams in database
        Task SaveTeamAsync(Team team);                        // saves Team to database
        Task AddUser(Team team, User account);                // adds User to Team, either in Mentors list or Members list
        Task AddTeamAdmin(Team team, Admin admin);            // adds Admin to Team, through the Team_Admins list
        Task UpdateTeamMembers(Team team);                    // updates Members list in database
        Task UpdateTeamMentors(Team team);                    // updates Mentors list in database
        Task UpdateTeamAdmins(Team team);                     // updates Team_Admins list in database
        Task<Team> IsTeamValid(Team team, Admin admin);       // returns Team if it exists in database + password matches + admin user and password matches, otherwise returns null
        Task<int> TeamExists(Team team);                      // returns an integer - given a Team, checks with database: (0) username & password matches / (1) username matches / (2) neither
        Task<bool> TeamAdminExists(Team team, Admin admin);   // returns if Admin username already exists within Team
        Task AddAnnouncement(Team team, string announcement); // adds announcement to beginning of list
        Task RemoveAnnouncement(Team team, int index);        // removes announcement based on index
        Task<List<string>> GetAnnouncements(Team team);       // returns team announcements
        Task UpdateAnnouncements(Team team);                  // updates Announcements list in database
        Task AddUserRequest(Team team, string username);      // adds user request to team
        Task RemoveUserRequest(Team team, string username);   // removes user request from team
        Task<List<string>> GetUserRequests(Team team);        // returns user requests
        Task UpdateUserRequests(Team team);                   // updates User_Requests list in database

        // Team searching and filtering methods
        Task<List<Team>> TeamSearch(string name);                                // returns list of Teams that match the name (either username or nickname)
        List<Team> FilterBestTeamResults(List<Team> teams, User user);           // cleans out teams for best results that match with the user variable
        List<Team> FilterTeamGender(List<Team> teams, string gender);            // cleans out teams based on gender
        List<Team> FilterTeamCity(List<Team> teams, string city, string state);  // cleans out teams based on city AND state
        List<Team> FilterTeamState(List<Team> teams, string state);              // cleans out teams based on state
        List<Team> FilterTeamPrivacy(List<Team> teams);                          // cleans out private teams
        List<Team> FilterTeamAge(List<Team> teams, string age);                  // cleans out teams based on age
    }
}
