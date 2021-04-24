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
        private IList<User> _MembersListView;
        private IList<User> _MentorsListView;
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

        public IList<User> MembersListView
        {
            get
            {
                return _MembersListView;
            }
            set 
            {
                _MembersListView = value;
            }
        }

        public IList<User> MentorsListView
        {
            get
            {
                return _MentorsListView;
            }
            set 
            {
                _MentorsListView = value;
            }
        }


        public MembersViewModel()
        {
            Dictionary<string, Admin> AdminDict = new Dictionary<string, Admin>();
            Dictionary<string, User> MemberDict = new Dictionary<string, User>();
            Dictionary<string, User> MentorDict = new Dictionary<string, User>();

            List<Admin> AdminList = new List<Admin>();
            List<User> MemberList = new List<User>();
            List<User> MentorList = new List<User>();


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

            foreach (KeyValuePair<string, Admin> entry in AdminDict)
            {
                AdminList.Add(entry.Value);
                AdminsListView = AdminList;
            }

            foreach (KeyValuePair<string, User> entry in MemberDict)
            {
                MemberList.Add(entry.Value);
                MembersListView = MemberList;
            }


            foreach (KeyValuePair<string, User> entry in MentorDict)
            {
                MentorList.Add(entry.Value);
                MentorsListView = MentorList;
            }

        }
    }
}
