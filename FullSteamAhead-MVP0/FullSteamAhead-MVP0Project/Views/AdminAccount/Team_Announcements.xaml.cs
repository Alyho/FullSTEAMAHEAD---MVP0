using FullSteamAheadMVP0Project.ViewModels;
using FullSteamAheadMVP0Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;

namespace FullSteamAheadMVP0Project.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Team_Announcements : ContentPage
    {
        private AnnouncementsViewModel _announcementViewModel;
        public Team_Announcements()
        {
            InitializeComponent();
            _announcementViewModel = new AnnouncementsViewModel();
            BindingContext = _announcementViewModel;
        }
        private async void Chat_Button_CLicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Team_Chat());
        }

        private async void Calendar_Button_CLicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Team_Calendar());
        }

        private async void Members_Button_CLicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Team_Members());
        }

        private async void Back_Button_MyTeam(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DisplayUser_Homepage());
        }

        private async void DeleteAnnouncement_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var delete = await this.DisplayAlert("Would you like to delete this announcement?", "", "Yes", "No");
            Dictionary<string, string> AnnouncementDict = new Dictionary<string, string>();

            if (delete == true)
            {

                AnnouncementDict = await App.Database.GetAnnouncements(Global.TeamSignedIn);

                foreach (KeyValuePair<string, string> entry in AnnouncementDict)
                {
                    if (entry.Value == _announcementViewModel.Item)
                    {
                        int Key = int.Parse(entry.Key.Substring(0, entry.Key.Length-1));
                        await App.Database.RemoveAnnouncement(Global.TeamSignedIn, Key);
                        //Teams.ItemsSource = await App.Database.GetAnnouncements(Global.TeamSignedIn); 
                        _announcementViewModel.AnnouncementsListView.Remove(_announcementViewModel.Item);
                        break;
                    }
                }
            }

        }

    }
}