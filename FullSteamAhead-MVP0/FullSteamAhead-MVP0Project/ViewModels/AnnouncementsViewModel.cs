using FullSteamAheadMVP0Project.Models;
using FullSteamAheadMVP0Project.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace FullSteamAheadMVP0Project.ViewModels
{
    public class AnnouncementsViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private INavigation _navigation;
        private string Announcement_;
        private string Item_;
        public Command SendCommand { get; }

        private IList<string> _AnnouncementsListView;
        public IList<string> AnnouncementsListView
        {
            get
            {
                return _AnnouncementsListView;
            }
            set
            {
                _AnnouncementsListView = value;
            }
        }

        public AnnouncementsViewModel()
        {
            Dictionary<string, string> AnnouncementDict = new Dictionary<string, string>();
            List<string> AnnouncementList = new List<string>();

            if (Global.UserSignedIn != null)
            {
                AnnouncementDict = Global.SelectedTeam.Announcements;

                foreach (KeyValuePair<string, string> entry in AnnouncementDict)
                {
                    AnnouncementList.Add(entry.Value);
                    AnnouncementsListView = AnnouncementList;
                }
                
            }
            else
            {
                AnnouncementDict = Global.TeamSignedIn.Announcements;

                foreach (KeyValuePair<string, string> entry in AnnouncementDict)
                {
                    AnnouncementList.Add(entry.Value);
                    AnnouncementsListView = AnnouncementList;
                }
            }

            SendCommand = new Command(async () =>
            {
                await App.Database.AddAnnouncement(Global.TeamSignedIn, Announcement_);
                AnnouncementList.Add(Announcement_);
                AnnouncementsListView = AnnouncementList;
                await _navigation.PushAsync(new Team_Announcements());

            });
        }

        public string Announcements
        {
            get => Announcement_;
            set
            {
                if (Announcement_ != value)
                {
                    Announcement_ = value;
                    var args = new PropertyChangedEventArgs(nameof(Announcements));
                    PropertyChanged?.Invoke(this, args);
                }
            }
        }

        public string Item
        {
            get => Item_;
            set
            {
                if (Item_ != value)
                {
                    Item_ = value;
                    var args = new PropertyChangedEventArgs(nameof(Item));
                    PropertyChanged?.Invoke(this, args);
                }
            }
        }

    }
}
