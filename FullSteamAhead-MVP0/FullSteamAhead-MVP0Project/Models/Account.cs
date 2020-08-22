using SQLite;

namespace FullSteamAheadMVP0Project.Models
{
    public class Account
    {
        [PrimaryKey]
        public string Username { get; set; }
        public string Password { get; set; }
    }
}