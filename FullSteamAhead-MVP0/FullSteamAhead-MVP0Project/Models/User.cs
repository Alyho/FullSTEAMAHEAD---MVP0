namespace FullSteamAheadMVP0Project.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Nickname { get; set; }
        public string Bio { get; set; }
        public Information Information { get; set; }
    }
}