﻿using System.Collections.Generic;
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

        Task<Team> GetTeamAsync(string teamUserName);
        Task<List<Team>> GetTeamsAsync();
        Task SaveTeamAsync(Team team);
        Task<bool> IsTeamValid(Team team);
    }
}
