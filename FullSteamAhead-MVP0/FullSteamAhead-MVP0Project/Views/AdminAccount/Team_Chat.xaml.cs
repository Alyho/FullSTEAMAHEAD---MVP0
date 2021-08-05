using FullSteamAheadMVP0Project.Models;
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
    public partial class Team_Chat : ContentPage
    {
        public Team_Chat()
        {
            InitializeComponent();
        }

        private async void TeamCalendar_Button_CLicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Team_Calendar());
        }

        private async void TeamAnnouncements_Button_CLicked(object sender, EventArgs e)
        {
                await Navigation.PushAsync(new Team_Announcements());
        }

        private async void TeamMembers_Button_CLicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Team_Members());
        }


        private async void Back_Button_Team(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DisplayUser_Homepage());
        }

    }


}