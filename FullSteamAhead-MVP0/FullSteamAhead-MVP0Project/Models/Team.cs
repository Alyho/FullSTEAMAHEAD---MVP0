using System.Collections.Generic;

namespace FullSteamAheadMVP0Project.Models
{
    public class Team
    {
        public Team()
        {
            Announcements = new Dictionary<string, string>();
            Team_Admins = new Dictionary<string, Admin>();
            Team_Username = "";
            Team_Nickname = "";
            Team_Password = "";
            Team_Information = new TeamInformation();
        }

        public Dictionary<string, string> Announcements { get; set; }
        public Dictionary<string, Admin> Team_Admins { get; set; }
        public string Team_Username { get; set; }
        public string Team_Nickname { get; set; }
        public string Team_Password { get; set; }
        public TeamInformation Team_Information { get; set; }
    }
}
