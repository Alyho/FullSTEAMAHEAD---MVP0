using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FullSteamAheadMVP0Project.Models
{
    static class Global
    {
        public static Team TeamSignedIn
        {
            get;
            set;
        }

        public static User UserSignedIn
        {
            get;
            set;
        }

        public static Admin AdminSignedIn
        {
            get;
            set;
        }

        public static ObservableCollection<Team> MyTeams
        {
            get;
            set;
        }
    }
}
