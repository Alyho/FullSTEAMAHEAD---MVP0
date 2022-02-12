using FullSteamAheadMVP0Project.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FullSteamAheadMVP0Project.ViewModels
{
    public class DisplayUserPageViewModel : INotifyPropertyChanged
    {
        private User _user;
        private bool _teamRequestExists;
        private bool _noEmail;
        private bool _userOnTeam;
        private string _InviteText;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool TeamRequestExists
        {
            get { return _teamRequestExists; }
        }

        public bool NoEmail
        {
            get { return _noEmail; }
        }

        public bool UserOnTeam
        {
            get { return _userOnTeam; }
        }

        public Command InviteUserCommand { get; }

        public string User_Username
        {
            get => _user.Username;
            set { }
        }

        public string User_Nickname
        {
            get => _user.Nickname;
            set { }
        }

        public string User_Email
        {
            get => _user.Email;
            set { }
        }

        public string User_Age
        {
            get => _user.Information.Age;
            set { }
        }

        public string User_Grade
        {
            get => _user.Information.Grade;
            set { }
        }

        public string User_Bio
        {
            get => _user.Information.Bio;
            set { }
        }

        public string User_State
        {
            get => _user.Information.State;
            set { }
        }

        public string User_City
        {
            get => _user.Information.City;
            set { }
        }

        public string User_Gender
        {
            get => _user.Information.Preferences.Gender;
            set { }
        }

        public string InviteText
        {
            get
            {
                return _InviteText;
            }
            set
            {
                _InviteText = value;
                NotifyPropertyChanged();
            }
        }

        public DisplayUserPageViewModel(User user)
        {
            _user = user;

            Task.Run(new System.Action(async () =>
            {
                if (await App.Database.TeamRequestExists(_user, Global.TeamSignedIn) == true || await App.Database.TeamUserExists(Global.TeamSignedIn, _user.Username) == true)
                {
                    InviteText = "Invite Sent";
                }
                else
                {
                    InviteText = "Invite";
                }
            }));

            InviteUserCommand = new Command(async () =>
            {
                if (await App.Database.TeamRequestExists(_user, Global.TeamSignedIn) ==false && await App.Database.TeamUserExists(Global.TeamSignedIn, _user.Username)==false)
                {
                    await App.Database.AddTeamRequest(_user, Global.TeamSignedIn.Team_Username);
                    _teamRequestExists = false;
                    _userOnTeam = false;

                    try
                    {
                        List<string> emails = new List<string>();
                        emails.Add(_user.Email);
                        var message = new EmailMessage
                        {
                            Subject = "Request to Join Team",
                            Body = "Hello! I found you on the app Full STEAM Ahead and you seem like a great addition to our STEM team. We would love for you to apply for our team." +
                            "Once you're on the team, you can accept our request on your Full STEAM Ahead app notifications page.",
                            To = emails
                        };

                        await Email.ComposeAsync(message);
                    }
                    catch (FeatureNotSupportedException fbsEx)
                    {
                        // Email is not supported on this device
                        _noEmail = true;


                    }
                    catch (Exception ex)
                    {
                        // Some other exception occurred
                        _noEmail = true;
                    }
                }
                else if (await App.Database.TeamRequestExists(_user, Global.TeamSignedIn) == true)
                {
                    _teamRequestExists = true;
                    
                }

                else
                {
                    _userOnTeam = true;
                }

                var ar = new PropertyChangedEventArgs(nameof(TeamRequestExists));
                PropertyChanged?.Invoke(this, ar);
                var br = new PropertyChangedEventArgs(nameof(NoEmail));
                PropertyChanged?.Invoke(this, br);
                var cr = new PropertyChangedEventArgs(nameof(UserOnTeam));
                PropertyChanged?.Invoke(this, cr);

            });
        }
    }
}
