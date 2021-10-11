using FullSteamAheadMVP0Project.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

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



        public AdminDisplayViewModel(Admin admin)
        {
            _admin = admin;
        }
    }
}
