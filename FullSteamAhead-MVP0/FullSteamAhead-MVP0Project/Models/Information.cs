using System;
namespace FullSteamAheadMVP0Project.Models
{
    public class Information
    {
        public Information()
        {
            Age = "";
            City = "";
            Email = "";
            Grade = "";
            Phone_Number = "";
            State = "";
            Birth_Date = "";
            Zip_Code = "";
            Preferences = new Preferences();
        }

        public string Age { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string Grade { get; set;}
        public string Phone_Number { get; set; }
        public string State { get; set; }
        public string Birth_Date { get; set; }
        public string Zip_Code { get; set; }
        public Preferences Preferences { get; set; }
    }
}
