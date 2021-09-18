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
    public partial class TeamSettingspage : ContentPage
    {

        public TeamSettingspage()
        {
            InitializeComponent();
        }

        private async void MainPage2_Button_Clicked(object sender, EventArgs e)
        {
            var logout = await DisplayAlert("Log out", "Do you want to log out?", "Yes", "No");
            if (logout)
            {
                Global.TeamSignedIn = null;
                Global.AdminSignedIn = null;
                await Navigation.PushAsync(new MainPage());
            }

        }

        private async void TeamChat2_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Team_Chat());
        }

        private async void Search_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DisplayUser_Homepage());
        }

        private async void ChangeTeamAccountInfo_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChangeTeamInformation1());
        }

        private async void Help2_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TeamHelp_page());
        }

        private async void FAQs2_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TeamFAQ_page());
        }

        private async void Notifications2_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TeamNotifications());
        }

    }

}