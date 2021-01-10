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

        public event PropertyChangedEventHandler PropertyChanged;

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
                await App.Database.AddUser(Global.TeamSignedIn, _user);
            });
        }
    }
}
