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
            Global.TeamSignedIn = null;
            await Navigation.PushAsync(new MainPage());
        }

        private async void MyTeam_Calendar_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyTeams_Chat());
        }

        private async void ChangeTeamAccountInfo_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChangeTeamInformation1());
        }

    }

}