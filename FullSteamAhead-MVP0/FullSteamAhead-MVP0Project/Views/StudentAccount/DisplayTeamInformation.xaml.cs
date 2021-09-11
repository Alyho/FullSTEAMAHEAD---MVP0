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
    public partial class DisplayTeamInformation : ContentPage
    {
        public DisplayTeamInformationViewModel displayTeamInformation { get; set; }
        public DisplayTeamInformation(Team team)
        {
            InitializeComponent();
            displayTeamInformation = new DisplayTeamInformationViewModel(team);
            displayTeamInformation.PropertyChanged += DisplayTeamInformationViewModel_PropertyChanged;
            this.BindingContext = displayTeamInformation;
        }

        private async void DisplayTeamInformationViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "UserRequestExists")
            {
                if (displayTeamInformation.UserRequestExists)
                {
                    await DisplayAlert("Sorry", "You have already requested to join this team", "OK");
                }

                else
                {
                    await DisplayAlert("Request was successful", "You have requested to join this team", "OK");
                }

            }

            if (e.PropertyName == "NoEmail")
            {
                if (displayTeamInformation.NoEmail)
                {
                    await DisplayAlert("Email Not Supported", "In order to notify the team you requested, send a direct email manually.", "OK");
                }
            }
            
        }

        private async void HomePage_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Homepage());
        }

        private async void Settings_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Settingspage());
        }

        private async void BackToSearch_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Homepage());
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