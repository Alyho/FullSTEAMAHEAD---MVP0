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
                    if (team.Students.ContainsKey(Global.UserSignedIn.Username))
                    {
                        myList.Add(team);
                    }

                    if (team.Mentors.ContainsKey(Global.UserSignedIn.Username))
                    {
                        myList.Add(team); 
                    }
                }

                MyTeamsListView = myList;
            }));

        }
    }
}
