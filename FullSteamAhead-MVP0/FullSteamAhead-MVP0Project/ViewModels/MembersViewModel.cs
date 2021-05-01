using FullSteamAheadMVP0Project.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FullSteamAheadMVP0Project.ViewModels
{
    public class MembersViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private IList<Admin> _AdminsListView; 
        private IList<User> _UserListView;
        public IList<Admin> AdminsListView
        {
            get
            {
                return _AdminsListView;
            }
            set 
            {
               _AdminsListView = value;
            }
        }

        public IList<User> UserListView
        {
            get
            {
                return _UserListView;
            }
            set 
            {
                _UserListView = value;
            }
        }

        public MembersViewModel()
        {
            Dictionary<string, Admin> AdminDict = new Dictionary<string, Admin>();
            Dictionary<string, User> MemberDict = new Dictionary<string, User>();
            Dictionary<string, User> MentorDict = new Dictionary<string, User>();

            List<Admin> AdminList = new List<Admin>();
            List<User> UserList = new List<User>();


            if (Global.SelectedTeam != null)
            {
                AdminDict = Global.SelectedTeam.Team_Admins;
                MemberDict = Global.SelectedTeam.Members;               
                MentorDict = Global.SelectedTeam.Mentors;
            }

            if (Global.TeamSignedIn != null)
            {
                AdminDict = Global.TeamSignedIn.Team_Admins;
                MemberDict = Global.TeamSignedIn.Members;
                MentorDict = Global.TeamSignedIn.Mentors;
            }
            
            int i = 0;

            foreach (KeyValuePair<string, Admin> entry in AdminDict)
            {
                
                AdminList.Add(entry.Value);
                AdminList[i].Username = entry.Key;
                AdminsListView = AdminList;
                i++;
            }

            i = 0;

            foreach (KeyValuePair<string, User> entry in MemberDict)
            {
                UserList.Add(entry.Value);
                UserList[i].Username = entry.Key;
                i++;
                UserListView = UserList;
            }

            foreach (KeyValuePair<string, User> entry in MentorDict)
            {
                UserList.Add(entry.Value);
                UserList[i].Username = entry.Key;
                i++;
                UserListView = UserList;
            }

        }
    }
}
