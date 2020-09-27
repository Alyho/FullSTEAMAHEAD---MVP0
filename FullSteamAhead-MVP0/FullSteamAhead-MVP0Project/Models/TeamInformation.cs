using System;
namespace FullSteamAheadMVP0Project.Models
{
    public class TeamInformation
    {
        public TeamInformation()
        {
            Team_Email = "";
            State = "";
            City = "";
            Zip_Code = "";
            Phone_Number = "";
            Gender = "";
            Privacy = "";
            Bio = "";
        }
        public string Team_Email { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Zip_Code { get; set; }
        public string Phone_Number { get; set; }
        public string Gender { get; set; }
        public string Privacy { get; set; }
        public string Bio { get; set; }
    }
}
