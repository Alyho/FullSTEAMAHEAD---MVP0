using System.Collections.Generic;

namespace FullSteamAheadMVP0Project.Models
{
    public class User
    {
        public User()
        {
            Team_Requests = new Dictionary<string, Team>();
            Username = "";
            Password = "";
            Nickname = "";
            Email = "";
            Information = new Information();
        }

        public Dictionary<string, Team> Team_Requests { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public Information Information { get; set; }
    }
}