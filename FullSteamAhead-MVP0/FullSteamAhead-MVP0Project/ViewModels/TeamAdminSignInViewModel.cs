using FullSteamAheadMVP0Project.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace FullSteamAheadMVP0Project.ViewModels
{
    public class TeamAdminSignInViewModel : INotifyPropertyChanged
    {
        public new event PropertyChangedEventHandler PropertyChanged;
        public Command CheckUserCommand { get; }

        private string Username_;
        private string Password_;
        private string PersonalUsername_;
        private string AdminPassword_;

        private bool _teamExists;
        public bool TeamExists
        {
            get { return _teamExists; }
        }

        public TeamAdminSignInViewModel()
        {
            CheckUserCommand = new Command(async () =>
            {
                var _admin = new Admin
                {
                    Username = PersonalUsername_,
                    Password = AdminPassword_
                };
                var _team = new Team
                {
                    Team_Username = Username_,
                    Team_Password = Password_,
                };

            //call the database to find any teams
            var found = await App.Database.IsTeamValid(_team, _admin);

                if (found == null)
                {
                //team doesn't exist
                _teamExists = false;
                }
                else
                {
                    _teamExists = true;
                    Global.TeamSignedIn = await App.Database.GetTeamAsync(_team.Team_Username);
                    Global.AdminSignedIn = await App.Database.GetAdminAsync(_admin.Username);
                }

            //Raise the Property Changed Event to notify the MainPage
            var ar = new PropertyChangedEventArgs(nameof(TeamExists));
                PropertyChanged?.Invoke(this, ar);

            //clear the textboxes
                TeamUsername = string.Empty;
                TeamPassword = string.Empty;
                TeamPersonalUsername = string.Empty;
                TeamAdminPassword = string.Empty;


            });
        }

        public string TeamUsername
        {
            get => Username_;
            set
            {
                if (Username_ != value)
                {
                    Username_ = value;
                    var args = new PropertyChangedEventArgs(nameof(TeamUsername));
                    PropertyChanged?.Invoke(this, args);
                }
            }
        }

        public string TeamPassword
        {
            get => Password_;
            set
            {
                if (Password_ != value)
                {
                    Password_ = value;
                    var args = new PropertyChangedEventArgs(nameof(TeamPassword));
                    PropertyChanged?.Invoke(this, args);
                }
            }
        }

        public string TeamPersonalUsername
        {
            get => PersonalUsername_;
            set
            {
                if (PersonalUsername_ != value)
                {
                    PersonalUsername_ = value;
                    var args = new PropertyChangedEventArgs(nameof(TeamPersonalUsername));
                    PropertyChanged?.Invoke(this, args);
                }
            }
        }

        public string TeamAdminPassword
        {
            get => AdminPassword_;
            set
            {
                if (AdminPassword_ != value)
                {
                    AdminPassword_ = value;
                    var args = new PropertyChangedEventArgs(nameof(TeamAdminPassword));
                    PropertyChanged?.Invoke(this, args);
                }
            }
        }

    }
}

