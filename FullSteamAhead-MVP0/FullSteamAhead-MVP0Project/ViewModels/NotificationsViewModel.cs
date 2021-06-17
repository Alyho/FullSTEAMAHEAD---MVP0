using FullSteamAheadMVP0Project.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace FullSteamAheadMVP0Project.ViewModels
{
    public class NotificationsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private IList<Container> _NotificationsListView;

        public Command DeclineCommand { get; }
        public IList<Container> NotificationsListView
        {
            get
            {
                return _NotificationsListView;
            }
            set
            {
                _NotificationsListView = value;
                NotifyPropertyChanged();
            }
        }

        public NotificationsViewModel()
        {
            Dictionary<string, User> UserRequestsDict = new Dictionary<string, User>();
            List<Container> UserRequestList = new List<Container>();

            Dictionary<string, Team> TeamRequestsDict = new Dictionary<string, Team>();
            List<Container> TeamRequestList = new List<Container>();

            if (Global.TeamSignedIn != null)
            {

                UserRequestsDict = Global.TeamSignedIn.User_Requests;

                foreach (KeyValuePair<string, User> entry in UserRequestsDict)
                {
                    UserRequestList.Add(new Container()
                    {
                        Username = entry.Value.Username
                    });

                }

                NotificationsListView = UserRequestList;

            }
            else
            {
                TeamRequestsDict = Global.UserSignedIn.Team_Requests;

                foreach (KeyValuePair<string, Team> entry in TeamRequestsDict)
                {
                    TeamRequestList.Add(new Container()
                    {
                        Username = entry.Value.Team_Username
                    });

                }

                NotificationsListView = TeamRequestList;
            }

        }
    }

    public class Container
    {
        public string Username { get; set; }
        public string Date { get; set; }

        public Command AcceptCommand { get; set; }
        public Command DeclineCommand { get; set; }

        public Container()
        {
            AcceptCommand = new Command(async () =>
            { 
                if (Global.TeamSignedIn != null)
                {
                    var user = await App.Database.GetAccountAsync(Username);
                    await App.Database.AddUser(Global.TeamSignedIn, user);
                    await App.Database.RemoveUserRequest(Global.TeamSignedIn, user.Username);
                    await App.Database.RemoveTeamRequest(user, Global.TeamSignedIn.Team_Username);
                }

                else
                {
                    var team = await App.Database.GetTeamAsync(Username);
                    await App.Database.AddUser(team, Global.UserSignedIn);
                    await App.Database.RemoveTeamRequest(Global.UserSignedIn, team.Team_Username);
                    await App.Database.RemoveUserRequest(team, Global.UserSignedIn.Username);
                }    
                
            });
        }
        
    }

}
