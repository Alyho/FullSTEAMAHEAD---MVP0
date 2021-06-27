using FullSteamAheadMVP0Project.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace FullSteamAheadMVP0Project.ViewModels
{
    public class MyTeamsPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public IList <Team> MyTeamsListView
        {
            get => Global.MyTeams;
            set { }
        }

        public MyTeamsPageViewModel()
        {

        }
    }
}
