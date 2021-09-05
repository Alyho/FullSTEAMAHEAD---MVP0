using FullSteamAheadMVP0Project.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace FullSteamAheadMVP0Project.ViewModels
{
    public class DisplayTeamInformationViewModel : INotifyPropertyChanged
    {
        private Team _team;
        private bool _userRequestExists;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool UserRequestExists
        {
            get { return _userRequestExists; }
        }

        public string Team_Username
        {
            get => _team.Team_Username;
            set { }
        }

        public string Team_Nickname
        {
            get => _team.Team_Nickname;
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

        public string Team_MinAge
        {
            get => _team.Team_Information.Min_Age;
            set { }
        }

        public string Team_MaxAge
        {
            get => _team.Team_Information.Max_Age;
            set { }
        }

        public string Team_Bio
        {
            get => _team.Team_Information.Bio;
            set { }
        }

        public string Team_Schedule
        {
            get => _team.Team_Information.Schedule;
            set { }
        }

        public Command RequestToJoinTeamCommand { get; }

        public DisplayTeamInformationViewModel(Team team)
        {
            _team = team;
            
            RequestToJoinTeamCommand = new Command(async () =>
            {
                if (await App.Database.UserRequestExists(_team, Global.UserSignedIn) == false)
                {
                    await App.Database.AddUserRequest(_team, Global.UserSignedIn.Username);
                    _userRequestExists = false;
                    List<string> emails = new List<string>();
                    emails.Add(_team.Team_Information.Team_Email);
                    var message = new EmailMessage
                    {
                        Subject = "Request to Join Team",
                        Body = "Hello! I found you on the app Full STEAM Ahead and I am interested in joining your team. Is there an application process?" +
                        "If I qualify, you can accept my request on your Full STEAM Ahead app notifications page.",
                        To = emails
                    };

                    await Email.ComposeAsync(message);
                }
                else
                {
                    _userRequestExists = true;
                    
                }

                var ar = new PropertyChangedEventArgs(nameof(UserRequestExists));
                PropertyChanged?.Invoke(this, ar);

            });
        }
    }
}
