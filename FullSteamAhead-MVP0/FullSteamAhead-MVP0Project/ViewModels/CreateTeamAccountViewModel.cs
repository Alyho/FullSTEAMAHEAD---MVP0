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
        private string TeamAdminEmail_;
        private string TeamAdminNickname_;
        private string TeamAdminPhoneNumber_;

        private string TeamState_;
        private string TeamCity_;
        private string TeamZipcode_;
        private string TeamGender_;
        private string TeamPrivacy_;
        private string TeamBio_;
        private string TeamSchedule_;

        private int _teamCreated;
        private bool _adminExists;
        private bool _unfilled;
        private bool _noIntAge;

        public int TeamCreated
        {
            get { return _teamCreated; }
        }

        public bool AdminExists
        {
            get { return _adminExists; }
        }

        public bool Unfilled
        {
            get { return _unfilled; }
        }

        public bool NoIntAge
        {
            get { return _noIntAge; }
        }


        public CreateTeamAccountViewModel(INavigation Navigation)
        {

            _navigation = Navigation;

            if (Global.TeamSignedIn != null && Global.AdminSignedIn != null)
            {
                TeamUsername = Global.TeamSignedIn.Team_Username;
                TeamPassword = Global.TeamSignedIn.Team_Password;
                TeamNickname = Global.TeamSignedIn.Team_Nickname;
                TeamEmail = Global.TeamSignedIn.Team_Information.Team_Email;
                MinAge = Global.TeamSignedIn.Team_Information.Min_Age;
                MaxAge = Global.TeamSignedIn.Team_Information.Max_Age;
                City = Global.TeamSignedIn.Team_Information.City;
                Zipcode = Global.TeamSignedIn.Team_Information.Zip_Code;
                State = Global.TeamSignedIn.Team_Information.State;
                Bio = Global.TeamSignedIn.Team_Information.Bio;
                Gender = Global.TeamSignedIn.Team_Information.Gender;
                Privacy = Global.TeamSignedIn.Team_Information.Privacy;
                Schedule = Global.TeamSignedIn.Team_Information.Schedule;
                TeamAdminPassword = Global.AdminSignedIn.Password;
                TeamAdminUsername = Global.AdminSignedIn.Username;
                TeamAdminEmail = Global.AdminSignedIn.Email;
                TeamAdminNickname = Global.AdminSignedIn.Nickname;
                TeamAdminPhoneNumber = Global.AdminSignedIn.Phone_Number;
            }

            CreateTeamCommand = new Command(async () =>
            {

                if (Global.TeamSignedIn != null)
                {
                    if (TeamUsername_ != Global.TeamSignedIn.Team_Username)
                    {

                        var _team = new Team
                        {
                            Team_Username = TeamUsername_,
                            Team_Password = TeamPassword_,
                            Team_Information = new TeamInformation()
                        };

                        if (_team.Team_Username != "" || _team.Team_Password != "")
                        {
                            //call the database to find teams
                            _teamCreated = await App.Database.TeamExists(_team);

                            if (_teamCreated == 2)
                            {
                                await App.Database.UpdateTeamUsername(Global.TeamSignedIn, _team.Team_Username);
                                Global.TeamSignedIn.Team_Username = _team.Team_Username;
                                Global.TeamSignedIn.Team_Password = _team.Team_Password;
                               
                            }

                            var ar = new PropertyChangedEventArgs(nameof(TeamCreated));
                            PropertyChanged?.Invoke(this, ar);
                        }

                        else
                        {
                            _unfilled = true;
                            var ar = new PropertyChangedEventArgs(nameof(Unfilled));
                            PropertyChanged?.Invoke(this, ar);
                        }

                        
                    }

                    Global.TeamSignedIn.Team_Password = TeamPassword_;
                    await App.Database.UpdateTeam(Global.TeamSignedIn);
                    await _navigation.PushAsync(new ChangeTeamInformation2());
                }

                else
                {

                    var _team = new Team
                    {
                        Team_Username = TeamUsername_,
                        Team_Password = TeamPassword_,
                        Team_Information = new TeamInformation()
                    };

                    if (_team.Team_Username != "" || _team.Team_Password != "")
                    {
                        //call the database to find teams
                        _teamCreated = await App.Database.TeamExists(_team);

                        if (_teamCreated == 0)
                        {
                            Global.TeamSignedIn = await App.Database.GetTeamAsync(_team.Team_Username);
                        }

                        else if (_teamCreated == 2)
                        {
                            Global.TeamSignedIn = _team;
                        }

                        
                        var ar = new PropertyChangedEventArgs(nameof(TeamCreated));
                        PropertyChanged?.Invoke(this, ar);
                    }

                    else
                    {
                        _unfilled = true;
                        var ar = new PropertyChangedEventArgs(nameof(Unfilled));
                        PropertyChanged?.Invoke(this, ar);
                    }
                }
            });

            SaveAdminCommand = new Command(async () =>
            {

                var _admin = new Admin
                {
                    Username = TeamAdminUsername_,
                    Password = TeamAdminPassword_
                };

                if (Global.AdminSignedIn != null)
                {
                    if (TeamAdminUsername_ != Global.AdminSignedIn.Username)
                    {

                        if (_admin.Username != "" || _admin.Password != "")
                        {
                            //call the database to find teams
                            _adminExists = await App.Database.TeamAdminExists(Global.TeamSignedIn, _admin);

                            if (_adminExists == false)
                            {

                                //await App.Database.UpdateTeamAdminUsername(Global.AdminSignedIn, _admin.Username);
                                Global.TeamSignedIn.Team_Admins.Remove(Global.AdminSignedIn.Username);
                                Global.AdminSignedIn.Username = TeamAdminUsername_;
                                Global.AdminSignedIn.Password = TeamAdminPassword_;
                                Global.AdminSignedIn.Email = TeamAdminEmail_;
                                Global.AdminSignedIn.Phone_Number = TeamAdminPhoneNumber_;
                                Global.AdminSignedIn.Nickname = TeamAdminNickname_;
                                Global.TeamSignedIn.Team_Admins.Add(Global.AdminSignedIn.Username, Global.AdminSignedIn);
                                //await App.Database.UpdateTeamAdmin(Global.AdminSignedIn);
                                await App.Database.UpdateTeamAdmins(Global.TeamSignedIn);
                                await _navigation.PushAsync(new TeamSettingspage());

                            }

                            var ar = new PropertyChangedEventArgs(nameof(AdminExists));
                            PropertyChanged?.Invoke(this, ar);
                        }

                        else
                        {
                            _unfilled = true;
                            var ar = new PropertyChangedEventArgs(nameof(Unfilled));
                            PropertyChanged?.Invoke(this, ar);
                        }
                    }

                    else
                    {
                        Global.TeamSignedIn.Team_Admins.Remove(Global.AdminSignedIn.Username);
                        Global.AdminSignedIn.Username = TeamAdminUsername_;
                        Global.AdminSignedIn.Password = TeamAdminPassword_;
                        Global.AdminSignedIn.Email = TeamAdminEmail_;
                        Global.AdminSignedIn.Phone_Number = TeamAdminPhoneNumber_;
                        Global.AdminSignedIn.Nickname = TeamAdminNickname_;
                        Global.TeamSignedIn.Team_Admins.Add(Global.AdminSignedIn.Username, Global.AdminSignedIn);
                        //await App.Database.UpdateTeamAdmin(Global.AdminSignedIn);
                        await App.Database.UpdateTeamAdmins(Global.TeamSignedIn);
                        await _navigation.PushAsync(new TeamSettingspage());
                    }

                }
                else
                {
                    if (_admin.Username != null || _admin.Password != null)
                    {
                        _adminExists = await App.Database.TeamAdminExists(Global.TeamSignedIn, _admin);

                        if (_adminExists == false)
                        {

                            await App.Database.AddTeamAdmin(Global.TeamSignedIn, _admin);
                            Global.AdminSignedIn = _admin;
                        }

                        var ar = new PropertyChangedEventArgs(nameof(AdminExists));
                        PropertyChanged?.Invoke(this, ar);
                    }

                    else
                    {
                        _unfilled = true;
                        var ar = new PropertyChangedEventArgs(nameof(Unfilled));
                        PropertyChanged?.Invoke(this, ar);
                    }
                }

                

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
                Global.TeamSignedIn.Team_Information.Min_Age = TeamMinAge_;
                Global.TeamSignedIn.Team_Information.Max_Age = TeamMaxAge_;

                

                if (Global.TeamSignedIn.Team_Nickname == "" || Global.TeamSignedIn.Team_Information.Team_Email == "" ||
                Global.TeamSignedIn.Team_Information.State == null || Global.TeamSignedIn.Team_Information.City == "" ||
                Global.TeamSignedIn.Team_Information.Zip_Code == "" || Global.TeamSignedIn.Team_Information.Gender == null ||
                Global.TeamSignedIn.Team_Information.Privacy == null || Global.TeamSignedIn.Team_Information.Min_Age == null || Global.TeamSignedIn.Team_Information.Max_Age == null)
                {
                    _unfilled = true;
                    var ar = new PropertyChangedEventArgs(nameof(Unfilled));
                    PropertyChanged?.Invoke(this, ar);
                }

                else
                {
                    int value;
                    if (int.TryParse(Global.TeamSignedIn.Team_Information.Min_Age, out value) && int.TryParse(Global.TeamSignedIn.Team_Information.Max_Age, out value))
                    {
                        if (Global.AdminSignedIn != null)
                        { 
                    
                            Global.TeamSignedIn.Team_Information.Bio = TeamBio_;
                            Global.TeamSignedIn.Team_Information.Schedule = TeamSchedule_;
                            await App.Database.UpdateTeam(Global.TeamSignedIn);
                            await _navigation.PushAsync(new ChangeAdminInformation());
                        }
                        else
                        {
                            await App.Database.SaveTeamAsync(Global.TeamSignedIn);
                            await _navigation.PushAsync(new CreateAdminAccount());
                        } 
                            
                    }
                    else
                    {
                        _noIntAge = true;
                        var ar = new PropertyChangedEventArgs(nameof(NoIntAge));
                        PropertyChanged?.Invoke(this, ar);
                    }

                    
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

        public string TeamAdminEmail
        { 
            get => TeamAdminEmail_;
            set
            {
                if (TeamAdminEmail_ != value)
                {
                    TeamAdminEmail_ = value;
                    var args = new PropertyChangedEventArgs(nameof(TeamAdminEmail));
                    PropertyChanged?.Invoke(this, args);
                }
            }
        }

        public string TeamAdminNickname
        {
            get => TeamAdminNickname_;
            set
            {
                if (TeamAdminNickname_ != value)
                {
                    TeamAdminNickname_ = value;
                    var args = new PropertyChangedEventArgs(nameof(TeamAdminNickname));
                    PropertyChanged?.Invoke(this, args);
                }
            }
        }

        public string TeamAdminPhoneNumber
        {
            get => TeamAdminPhoneNumber_;
            set
            {
                if (TeamAdminPhoneNumber_ != value)
                {
                    TeamAdminPhoneNumber_ = value;
                    var args = new PropertyChangedEventArgs(nameof(TeamAdminPhoneNumber));
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
