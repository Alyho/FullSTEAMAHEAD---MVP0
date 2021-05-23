using System;
namespace FullSteamAheadMVP0Project.Models
{
    public class Admin
    {
        public Admin()
        {
            Username = "";
            Password = "";
            Nickname = "";
            Email = "";
            Phone_Number = "";
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public string Phone_Number { get; set; }
    }
}
