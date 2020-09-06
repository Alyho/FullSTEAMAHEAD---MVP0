using System;
namespace FullSteamAheadMVP0Project.Models
{
    public class Preferences
    {
        public Preferences()
        {
            Gender = "";
            Privacy = "";
            Role = "";
        }

        public int Distance { get; set; }
        public string Gender { get; set; }
        public string Privacy { get; set; }
        public string Role { get; set; }
    }
}
