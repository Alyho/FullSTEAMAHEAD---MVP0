using System.Collections.Generic;

namespace FullSteamAheadMVP0Project.Models
{
    public class User
    {
        public User()
        {
            Username = "";
            Password = "";
            Nickname = "";
            Email = "";
            Information = new Information();
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public Information Information { get; set; }
    }
}