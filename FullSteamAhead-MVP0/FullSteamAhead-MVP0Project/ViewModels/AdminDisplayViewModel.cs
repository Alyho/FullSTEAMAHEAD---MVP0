using FullSteamAheadMVP0Project.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace FullSteamAheadMVP0Project.ViewModels
{
    public class AdminDisplayViewModel : INotifyPropertyChanged
    {
        private Admin _admin;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Admin_Username
        {
            get => _admin.Username;
            set { }
        }

        public string Admin_Nickname
        {
            get => _admin.Nickname;
            set { }
        }

        public string Admin_Email
        {
            get => _admin.Email;
            set { }
        }


        private string _profile;


        public string Admin_Profile
        {
            get
            {
                return _profile;
            }
            set
            {
                _profile = value;
                var args = new PropertyChangedEventArgs(nameof(Admin_Profile));
                PropertyChanged?.Invoke(this, args);

            }
        }



        public AdminDisplayViewModel(Admin admin)
        {
            _admin = admin;

            Task.Run(new System.Action(async () =>
            {

                string path = await App.Database.GetTeamAdminFile(Global.TeamSignedIn.Team_Username, admin.Username);
                if (path != null)
                {
                    Admin_Profile = path;
                }

            }));
        }
    }
}
