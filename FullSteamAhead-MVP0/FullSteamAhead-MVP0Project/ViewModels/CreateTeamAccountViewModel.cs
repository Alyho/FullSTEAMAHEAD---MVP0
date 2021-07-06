using FullSteamAheadMVP0Project.Models;
using FullSteamAheadMVP0Project.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace FullSteamAheadMVP0Project.ViewModels
{
    public class CreateTeamAccountViewModel : ContentPage, INotifyPropertyChanged
    {
        private INavigation _navigation; 

        public Command SaveTeamCommand { get; }
        public Command SaveAdminCommand { get; }

        public Command CreateTeamCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        private string TeamUsername_;
        private string TeamPassword_;
        private string TeamNickname_;
        private string TeamEmail_;

        private string TeamMinAge_;
        private string TeamMaxAge_;

        private string TeamAdminUsername_;
        private string TeamAdminPassword_;

        private string TeamState_;
        private string TeamCity_;
        private string TeamZipcode_;
        private string TeamGender_;
        private string TeamPrivacy_;
        private string TeamBio_;
        private string TeamSchedule_;

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



        public CreateTeamAccountViewModel(INavigation Navigation)
        {

            _navigation = Navigation;

            CreateTeamCommand = new Command(async () =>
            {
                var _team = new Team
                {
                    Team_Username = TeamUsername_,
                    Team_Password = TeamPassword_,
                    Team_Information = new TeamInformation()
                };

                //call the database to find teams
                _teamCreated = await App.Database.TeamExists(_team);

                if (_teamCreated == 0)
                {
                    Global.TeamSignedIn = _team;
                }

                else if (_teamCreated == 2)
                {
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

            SaveTeamCommand = new Command(async () =>
            {
                Global.TeamSignedIn.Team_Nickname = TeamNickname_;
                Global.TeamSignedIn.Team_Information.Team_Email = TeamEmail_;
                Global.TeamSignedIn.Team_Information.State = TeamState_;
                Global.TeamSignedIn.Team_Information.City = TeamCity_;
                Global.TeamSignedIn.Team_Information.Zip_Code= TeamZipcode_;
                Global.TeamSignedIn.Team_Information.Gender = TeamGender_;
                Global.TeamSignedIn.Team_Information.Privacy = TeamPrivacy_;
              
                

                if (Global.AdminSignedIn != null)
                { 
                    Global.TeamSignedIn.Team_Information.Min_Age = TeamMinAge_;
                    Global.TeamSignedIn.Team_Information.Max_Age = TeamMaxAge_;
                    Global.TeamSignedIn.Team_Information.Bio = TeamBio_;
                    Global.TeamSignedIn.Team_Information.Schedule = TeamSchedule_;

                    await App.Database.SaveTeamAsync(Global.TeamSignedIn);

                    await _navigation.PushAsync(new TeamSettingspage());
                }
                else
                {
                    await App.Database.SaveTeamAsync(Global.TeamSignedIn);

                    await _navigation.PushAsync(new CreateAdminAccount());
                } 
                
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

        public string TeamNickname
        {
            get => TeamNickname_;
            set
            {
                if (TeamNickname_ != value)
                {
                    TeamNickname_ = value;
                    var args = new PropertyChangedEventArgs(nameof(TeamNickname));
                    PropertyChanged?.Invoke(this, args);
                }
            }
        }

        public string TeamEmail
        {
            get => TeamEmail_;
            set
            {
                if (TeamEmail_ != value)
                {
                    TeamEmail_ = value;
                    var args = new PropertyChangedEventArgs(nameof(TeamEmail));
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

        public string State
        {
            get => TeamState_;
            set
            {
                if (TeamState_ != value)
                {
                    TeamState_ = value;
                    var args = new PropertyChangedEventArgs(nameof(State));
                    PropertyChanged?.Invoke(this, args);
                }
            }
        }

        public string City
        {
            get => TeamCity_;
            set
            {
                if (TeamCity_ != value)
                {
                    TeamCity_ = value;
                    var args = new PropertyChangedEventArgs(nameof(City));
                    PropertyChanged?.Invoke(this, args);
                }
            }
        }

        public string Zipcode
        {
            get => TeamZipcode_;
            set
            {
                if (TeamZipcode_ != value)
                {
                    TeamZipcode_ = value;
                    var args = new PropertyChangedEventArgs(nameof(Zipcode));
                    PropertyChanged?.Invoke(this, args);
                }
            }
        }

        public string Gender
        {
            get => TeamGender_;
            set
            {
                if (TeamGender_ != value)
                {
                    TeamGender_ = value;
                    var args = new PropertyChangedEventArgs(nameof(Gender));
                    PropertyChanged?.Invoke(this, args);
                }
            }
        }

        public string Privacy
        {
            get => TeamPrivacy_;
            set
            {
                if (TeamPrivacy_ != value)
                {
                    TeamPrivacy_ = value;
                    var args = new PropertyChangedEventArgs(nameof(Privacy));
                    PropertyChanged?.Invoke(this, args);
                }
            }
        }

        public string Bio
        {
            get => TeamBio_;
            set
            {
                if (TeamBio_ != value)
                {
                    TeamBio_ = value;
                    var args = new PropertyChangedEventArgs(nameof(Bio));
                    PropertyChanged?.Invoke(this, args);
                }
            }
        }

        public string Schedule
        {
            get => TeamSchedule_;
            set
            {
                if (TeamSchedule_ != value)
                {
                    TeamSchedule_ = value;
                    var args = new PropertyChangedEventArgs(nameof(Schedule));
                    PropertyChanged?.Invoke(this, args);
                }
            }
        }

        public string MinAge
        {
            get => TeamMinAge_;
            set
            {
                if (TeamMinAge_ != value)
                {
                    TeamMinAge_ = value;
                    var args = new PropertyChangedEventArgs(nameof(MinAge));
                    PropertyChanged?.Invoke(this, args);
                }
            }
        }

        public string MaxAge
        {
            get => TeamMaxAge_;
            set
            {
                if (TeamMaxAge_ != value)
                {
                    TeamMaxAge_ = value;
                    var args = new PropertyChangedEventArgs(nameof(MaxAge));
                    PropertyChanged?.Invoke(this, args);
                }
            }
        }
    }
}
