using FullSteamAheadMVP0Project.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FullSteamAheadMVP0Project.ViewModels
{
    public class NotificationsViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private IList<string> _NotificationsListView;
        public IList<string> NotificationsListView
        {
            get
            {
                return _NotificationsListView;
            }
            set
            {
                _NotificationsListView = value;
            }
        }

        public NotificationsViewModel()
        {
            Dictionary<string, User> UserRequestsDict = new Dictionary<string, User>();
            List<string> UserRequestList = new List<string>();

            Dictionary<string, Team> TeamRequestsDict = new Dictionary<string, Team>();
            List<string> TeamRequestList = new List<string>();

            if (Global.TeamSignedIn != null)
            {
             
                UserRequestsDict = Global.TeamSignedIn.User_Requests;

                foreach (KeyValuePair<string, User> entry in UserRequestsDict)
                {
                    UserRequestList.Add(entry.Value.Username);
                    NotificationsListView = UserRequestList;
                }

            }
            else
            {
                TeamRequestsDict = Global.UserSignedIn.Team_Requests;

                foreach (KeyValuePair<string, Team> entry in TeamRequestsDict)
                {
                    TeamRequestList.Add(entry.Value.Team_Username);
                    NotificationsListView = TeamRequestList;
                }
            }


        }
    }
}
