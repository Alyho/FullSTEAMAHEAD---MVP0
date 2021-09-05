using FullSteamAheadMVP0Project.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

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
            Dictionary<string, User> StudentDict = new Dictionary<string, User>();
            Dictionary<string, User> MentorDict = new Dictionary<string, User>();

            List<Admin> AdminList = new List<Admin>();
            List<User> UserList = new List<User>();

            Task.Run(new System.Action(async () =>
            {
                if (Global.SelectedTeam != null)
                {
                    AdminDict = Global.SelectedTeam.Team_Admins;
                    StudentDict = await App.Database.GetStudents(Global.SelectedTeam);
                    MentorDict = await App.Database.GetMentors(Global.SelectedTeam);
                }

                if (Global.TeamSignedIn != null)
                {
                    AdminDict = Global.TeamSignedIn.Team_Admins;
                    StudentDict = await App.Database.GetStudents(Global.TeamSignedIn);
                    MentorDict = await App.Database.GetMentors(Global.TeamSignedIn);
                }
            }));
            
            int i = 0;

            foreach (KeyValuePair<string, Admin> entry in AdminDict)
            {
                
                AdminList.Add(entry.Value);
                AdminList[i].Username = entry.Key;
                AdminsListView = AdminList;
                i++;
            }

            i = 0;

            foreach (KeyValuePair<string, User> entry in StudentDict)
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
