using System.Collections.Generic;

namespace FullSteamAheadMVP0Project.Models
{
    public class Team
    {
        public Dictionary<string, User> Members { get; set; }
        public Dictionary<string, User> Mentors { get; set; }
        public Dictionary<string, Admin> Team_Admins { get; set; }
        public Team()
        {
            Members = new Dictionary<string, User>();
            Mentors = new Dictionary<string, User>();
            Team_Admins = new Dictionary<string, Admin>();
            Team_Username = "";
            Team_Nickname = "";
            Team_Password = "";
            Team_Information = new TeamInformation();
        }
        public string Team_Username { get; set; }
        public string Team_Nickname { get; set; }
        public string Team_Password { get; set; }
        public TeamInformation Team_Information { get; set; }
        
    }
}
