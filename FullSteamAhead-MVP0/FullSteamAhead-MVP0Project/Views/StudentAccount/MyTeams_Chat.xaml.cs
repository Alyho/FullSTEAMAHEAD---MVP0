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
    public partial class MyTeams_Chat : ContentPage
    {
        public MyTeams_Chat()
        {
            InitializeComponent();
        }

        private async void Calendar_Button_CLicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyTeams_Calendar());
        }

        private async void Announcements_Button_CLicked(object sender, EventArgs e)
        {
            if (Global.TeamSignedIn != null)
            {
                await Navigation.PushAsync(new Team_Announcements());
            }

            else if (Global.UserSignedIn != null)
            {
                await Navigation.PushAsync(new MyTeams_Announcements());

            }
        }

        private async void Members_Button_CLicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyTeams_Members());
        }

    }


}