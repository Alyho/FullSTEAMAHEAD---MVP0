using FullSteamAheadMVP0Project.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FullSteamAheadMVP0Project.ViewModels
{
    public class DisplayTeamInformationViewModel : INotifyPropertyChanged
    {
        private Team _team;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Team_Username
        {
            get => _team.Team_Username;
            set { }
        }

        public string Team_Location
        {
            get => _team.Team_Information.City + ", " + _team.Team_Information.State + " " + _team.Team_Information.Zip_Code;
            set { }
        }

        public string Team_Gender
        {
            get => _team.Team_Information.Gender;
            set { }
        }

        public string Team_Email
        {
            get => _team.Team_Information.Team_Email;
            set { }
        }

        public string Team_Bio
        {
            get => _team.Team_Information.Bio;
            set { }
        }



        public DisplayTeamInformationViewModel(Team team)
        {
            _team = team;
        }
    }
}
