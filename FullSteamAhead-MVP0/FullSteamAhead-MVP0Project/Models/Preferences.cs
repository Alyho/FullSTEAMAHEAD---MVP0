using System;
namespace FullSteamAheadMVP0Project.Models
{
    public class Preferences
    {
        public Preferences()
        {
            Distance = "";
            Gender = "";
            Privacy = "";
            Role = "";
        }

        public string Distance { get; set; }
        public string Gender { get; set; }
        public string Privacy { get; set; }
        public string Role { get; set; }
    }
}
