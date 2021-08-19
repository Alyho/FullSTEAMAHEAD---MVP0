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
        private string Announcement_;
        private string Item_;
        public Command SendCommand { get; }

        private ObservableCollection<string> _AnnouncementsListView;
        public ObservableCollection<string> AnnouncementsListView
        {
            get
            {
                return _AnnouncementsListView;
            }
            set
            {
                if (_AnnouncementsListView != value)
                {
                    _AnnouncementsListView = value;
                    var args = new PropertyChangedEventArgs(nameof(AnnouncementsListView));
                    PropertyChanged?.Invoke(this, args);
                }
            }
        }

        public AnnouncementsViewModel()
        {
            Dictionary<string, string> AnnouncementDict = new Dictionary<string, string>();
            ObservableCollection<string> AnnouncementList = new ObservableCollection<string>();

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
                }
                AnnouncementsListView = AnnouncementList;
            }

            SendCommand = new Command(async () =>
            {
                await App.Database.AddAnnouncement(Global.TeamSignedIn, Announcement_);
                AnnouncementList.Add(Announcement_);
                //AnnouncementsListView = AnnouncementList;
                
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
