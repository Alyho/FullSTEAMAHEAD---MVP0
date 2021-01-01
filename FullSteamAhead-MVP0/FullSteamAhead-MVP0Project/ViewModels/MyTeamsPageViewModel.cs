using FullSteamAheadMVP0Project.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace FullSteamAheadMVP0Project.ViewModels
{
    public class MyTeamsPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Team> MyTeamsListView
        {
            get => Global.MyTeams;
            set { }
        }
    }
}
