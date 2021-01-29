using FullSteamAheadMVP0Project.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace FullSteamAheadMVP0Project.ViewModels
{
    public class CreateTeamAccountViewModel : INotifyPropertyChanged
    {

        public Command SaveTeamCommand { get; }
        public Command SaveAdminCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        private string TeamUsername_;
        private string TeamPassword_;
        private string TeamAdminUsername_;
        private string TeamAdminPassword_;

        private int _teamCreated;
        private bool _adminExists;

        public int TeamCreated
        {
            get { return _teamCreated; }
        }

        public bool AdminExists
        {
            get { return _adminExists; }
        }



        public CreateTeamAccountViewModel()
        {
            SaveTeamCommand = new Command(async () =>
            {
                var _team = new Team
                {
                    Team_Username = TeamUsername_,
                    Team_Password = TeamPassword_
                };

                //call the database to find teams
                _teamCreated = await App.Database.TeamExists(_team);

                if (_teamCreated == 0)
                {
                    Global.TeamSignedIn = _team;
                }

                else if (_teamCreated == 2)
                {
                    await App.Database.SaveTeamAsync(_team);
                    Global.TeamSignedIn = _team;
                }

             
                var ar = new PropertyChangedEventArgs(nameof(TeamCreated));
                PropertyChanged?.Invoke(this, ar);

                //clear the textboxes
                TeamUsername = string.Empty;
                TeamPassword = string.Empty;

            });

            SaveAdminCommand = new Command(async () =>
            {

                var _admin = new Admin
                {
                    Username = TeamAdminUsername_,
                    Password = TeamAdminPassword_
                };

                _adminExists = await App.Database.TeamAdminExists(Global.TeamSignedIn, _admin);

                if (_adminExists == false)
                {
                    await App.Database.AddTeamAdmin(Global.TeamSignedIn, _admin);
                    Global.AdminSignedIn = _admin;
                }

                var ar = new PropertyChangedEventArgs(nameof(AdminExists));
                PropertyChanged?.Invoke(this, ar);

                //clear the textboxes
                TeamAdminUsername = string.Empty;
                TeamAdminPassword = string.Empty;

            });

        }

        public string TeamUsername
        {
            get => TeamUsername_;
            set
            {
                if (TeamUsername_ != value)
                {
                    TeamUsername_ = value;
                    var args = new PropertyChangedEventArgs(nameof(TeamUsername));
                    PropertyChanged?.Invoke(this, args);
                }
            }
        }

        public string TeamPassword
        {
            get => TeamPassword_;
            set
            {
                if (TeamPassword_ != value)
                {
                    TeamPassword_ = value;
                    var args = new PropertyChangedEventArgs(nameof(TeamPassword));
                    PropertyChanged?.Invoke(this, args);
                }
            }
        }

        public string TeamAdminUsername
        {
            get => TeamAdminUsername_;
            set
            {
                if (TeamAdminUsername_ != value)
                {
                    TeamAdminUsername_ = value;
                    var args = new PropertyChangedEventArgs(nameof(TeamAdminUsername));
                    PropertyChanged?.Invoke(this, args);
                }
            }
        }

        public string TeamAdminPassword
        {
            get => TeamAdminPassword_;
            set
            {
                if (TeamAdminPassword_ != value)
                {
                    TeamAdminPassword_ = value;
                    var args = new PropertyChangedEventArgs(nameof(TeamAdminPassword));
                    PropertyChanged?.Invoke(this, args);
                }
            }
        }
    }
}
