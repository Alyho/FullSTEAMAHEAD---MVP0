using FullSteamAheadMVP0Project.Models;
using FullSteamAheadMVP0Project.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FullSteamAheadMVP0Project.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Notifications_DisplayTeamInformation : ContentPage
    {
        public DisplayTeamInformationViewModel displayTeamInformation { get; set; }
        public Notifications_DisplayTeamInformation(Team team)
        {
            InitializeComponent();
            displayTeamInformation = new DisplayTeamInformationViewModel(team);
            this.BindingContext = displayTeamInformation;
        }

        private async void HomePage_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Homepage());
        }

        private async void Settings_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Settingspage());
        }

        private async void Back_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Notifications());
        }

        private async void Notifications_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Notifications());

        }

        private async void MyTeams_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyTeamsPage());

        }

    }

}