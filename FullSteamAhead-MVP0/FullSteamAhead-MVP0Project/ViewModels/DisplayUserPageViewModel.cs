using FullSteamAheadMVP0Project.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FullSteamAheadMVP0Project.ViewModels
{
    public class DisplayUserPageViewModel : INotifyPropertyChanged
    {
        private User _user;
        private bool _teamRequestExists;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool TeamRequestExists
        {
            get { return _teamRequestExists; }
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
            get => _user.Username;
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

        public DisplayUserPageViewModel(User user)
        {
            _user = user;

            InviteUserCommand = new Command(async () =>
            {
                if (await App.Database.TeamRequestExists(_user, Global.TeamSignedIn) ==false)
                {
                    await App.Database.AddTeamRequest(_user, Global.TeamSignedIn.Team_Username);
                    _teamRequestExists = false;
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
                else
                {
                    _teamRequestExists = true;
                    
                }

                var ar = new PropertyChangedEventArgs(nameof(TeamRequestExists));
                PropertyChanged?.Invoke(this, ar);
            });
        }
    }
}
