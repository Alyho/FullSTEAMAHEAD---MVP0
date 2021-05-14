using FullSteamAheadMVP0Project.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FullSteamAheadMVP0Project.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyTeams_Announcements : ContentPage
    {
        public MyTeams_Announcements()
        {
            InitializeComponent();
            BindingContext = new AnnouncementsViewModel();
        }
        private async void Chat_Button_CLicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyTeams_Chat());
        }

        private async void Calendar_Button_CLicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyTeams_Calendar());
        }

        private async void Members_Button_CLicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyTeams_Members());
        }

        private async void Back_Button_MyTeams(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyTeamsPage());
        }

        private async void DeleteAnnouncement_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var delete = await this.DisplayAlert("Would you like to delete this announcement?", "{Binding Announcements}", "Yes", "No");

            if (delete == true)
            {

            }

        }
    }
}