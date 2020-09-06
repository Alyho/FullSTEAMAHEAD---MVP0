using System;
namespace FullSteamAheadMVP0Project.Models
{
    public class Information
    {
        public Information()
        {
            City = "";
            Email = "";
            State = "";
            Preferences = new Preferences();
        }

        public int Age { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public int Grade { get; set;}
        public int Phone_Number { get; set; }
        public string State { get; set; }
        public int Birth_Date { get; set; }
        public int Zip_Code { get; set; }
        public Preferences Preferences { get; set; }
    }
}
