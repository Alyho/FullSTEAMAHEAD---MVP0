using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace FullSteamAheadMVP0Project.Models
{
    public interface IDatabase
    {
        Task<User> GetAccountAsync(string userName);
        Task<List<User>> GetAccountsAsync();
        Task SaveAccountAsync(User account);
        Task<bool> IsAccountValid(User account);
        Task<List<User>> AccountSearch(string name);

        Task<List<Team>> TeamSearch(string name);
        Task<List<Team>> GetTeamsAsync();
        Task SaveTeamAsync(Team team);
        Task<Team> IsTeamValid(Team team, Admin admin);
        Task<bool> TeamExists(Team team);
        Task AddUser(Team team, User account);
        Task UpdateTeamMembers(Team team);
        Task UpdateTeamMentors(Team team);
        Task AddTeamAdmin(Team team, Admin admin);
        Task UpdateTeamAdmins(Team team);
    }
}
