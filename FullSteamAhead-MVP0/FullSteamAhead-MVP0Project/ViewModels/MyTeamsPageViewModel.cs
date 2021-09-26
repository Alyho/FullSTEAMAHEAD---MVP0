using FullSteamAheadMVP0Project.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FullSteamAheadMVP0Project.ViewModels
{
    public class MyTeamsPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _loading = true;

        public bool Loading
        {
            get { return _loading; }
            set
            {
                if (_loading != value)
                {
                    _loading = value;
                    var args = new PropertyChangedEventArgs(nameof(Loading));
                    PropertyChanged?.Invoke(this, args);
                }
            }
        }

        private ObservableCollection<Team> _TeamListView;
        public ObservableCollection<Team> MyTeamsListView
        {
            get
            {
                return _TeamListView;
            }
            set
            {
                if (_TeamListView != value)
                {
                    _TeamListView = value;
                    var args = new PropertyChangedEventArgs(nameof(MyTeamsListView));
                    PropertyChanged?.Invoke(this, args);
                }
            }
        }

        public MyTeamsPageViewModel()
        {
            ObservableCollection<Team> myList = new ObservableCollection<Team>();

            Task.Run(new System.Action(async () =>
            {
                var myTeams = await App.Database.GetTeamsAsync();

                foreach (var team in myTeams)
                {
                    if (await App.Database.TeamUserExists(team, Global.UserSignedIn.Username))
                    {
                        myList.Add(team); 
                    }
                }

                Loading = false;

                MyTeamsListView = myList;
            }));

        }
    }
}
