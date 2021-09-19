using FullSteamAheadMVP0Project.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FullSteamAheadMVP0Project.ViewModels
{
    public class MembersViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<Admin> _AdminsListView; 
        private ObservableCollection<User> _UserListView;
        public ObservableCollection<Admin> AdminsListView
        {
            get
            {
                return _AdminsListView;
            }
            set 
            {
               _AdminsListView = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<User> UserListView
        {
            get
            {
                return _UserListView;
            }
            set 
            {
                _UserListView = value;
                NotifyPropertyChanged();
            }
        }

        public MembersViewModel()
        {
            Dictionary<string, Admin> AdminDict = new Dictionary<string, Admin>();
            Dictionary<string, User> StudentDict = new Dictionary<string, User>();
            Dictionary<string, User> MentorDict = new Dictionary<string, User>();

            ObservableCollection<Admin> AdminList = new ObservableCollection<Admin>();
            ObservableCollection<User> UserList = new ObservableCollection<User>();

            Task.Run(new System.Action(async () =>
            {

            
                if (Global.SelectedTeam != null)
                {

                    AdminDict = Global.SelectedTeam.Team_Admins;
                    StudentDict = await App.Database.GetTeamStudents(Global.SelectedTeam);
                    MentorDict = await App.Database.GetTeamMentors(Global.SelectedTeam);

                }

                if (Global.TeamSignedIn != null)
                {
                    AdminDict = Global.TeamSignedIn.Team_Admins;
                    StudentDict = await App.Database.GetTeamStudents(Global.TeamSignedIn);
                    MentorDict = await App.Database.GetTeamMentors(Global.TeamSignedIn);
                }
            
                int i = 0;

                foreach (KeyValuePair<string, Admin> entry in AdminDict)
                {
                
                    AdminList.Add(entry.Value);
                    AdminList[i].Username = entry.Key;
                    i++;
                }

                i = 0;

                foreach (KeyValuePair<string, User> entry in StudentDict)
                {
                    UserList.Add(entry.Value);
                    UserList[i].Username = entry.Key;
                    i++;
                }

                foreach (KeyValuePair<string, User> entry in MentorDict)
                {
                    UserList.Add(entry.Value);
                    UserList[i].Username = entry.Key;
                    i++;
                    
                }

                AdminsListView = AdminList;
                UserListView = UserList;

            }));

        }
    }
}
