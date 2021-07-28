using FullSteamAheadMVP0Project.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
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

        public string User_Bio
        {
            get => _user.Information.Bio;
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
