using FullSteamAheadMVP0Project.Models;
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
        public Command DeleteCommand { get;  }

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
            if (Global.UserSignedIn != null)
            {
                _AnnouncementsListView = Global.SelectedTeam.Announcements;
            }
            else
            {
                _AnnouncementsListView = Global.TeamSignedIn.Announcements;
            }

            AnnouncementsListView = _AnnouncementsListView;

            SendCommand = new Command(async () =>
            {
                await App.Database.AddAnnouncement(Global.TeamSignedIn, Announcement_);
                _AnnouncementsListView = Global.TeamSignedIn.Announcements;
                AnnouncementsListView = _AnnouncementsListView;

            });

            DeleteCommand = new Command(async () =>
            {
                int i = 0; 
                foreach (var announcement in Global.TeamSignedIn.Announcements)
                {
                    if (Global.TeamSignedIn.Announcements[i] == Item_)
                    {
                        await App.Database.RemoveAnnouncement(Global.TeamSignedIn, i);
                        _AnnouncementsListView = Global.TeamSignedIn.Announcements;
                        AnnouncementsListView = _AnnouncementsListView;
                        break;
                    }

                    i++; 
                }
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
