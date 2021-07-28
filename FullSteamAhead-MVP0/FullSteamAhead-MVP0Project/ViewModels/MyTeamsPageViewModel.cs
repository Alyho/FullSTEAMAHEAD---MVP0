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
       
        private IList<Team> _TeamListView;
        public IList <Team> MyTeamsListView
        {
            get
            {
                return _TeamListView;
            }
            set
            {
                _TeamListView = value;
            }
        }

        public MyTeamsPageViewModel()
        {
            List<Team> myList = new List<Team>();

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

                _TeamListView = myList;
            }));

        }
    }
}
