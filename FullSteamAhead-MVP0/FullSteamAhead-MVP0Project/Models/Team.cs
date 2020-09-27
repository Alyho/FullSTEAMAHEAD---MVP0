using System.Collections.Generic;

namespace FullSteamAheadMVP0Project.Models
{
    public class Team
    {
        public List<User> Team_Members { get; set; }
        public Team()
        {
            Team_Members = new List<User>();
        }
        public string Team_Username { get; set; }
        public string Team_Nickname { get; set; }
        public string Team_Password { get; set; }
        public TeamInformation Team_Information { get; set; }
        
    }
}
