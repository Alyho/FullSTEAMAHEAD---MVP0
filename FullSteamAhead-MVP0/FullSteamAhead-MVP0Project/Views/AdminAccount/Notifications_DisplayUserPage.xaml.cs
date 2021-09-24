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
    public partial class Notifications_DisplayUserPage : ContentPage
    {
        public DisplayUserPageViewModel displayUserPage { get; set; }

        public Notifications_DisplayUserPage(User user)
        {
            InitializeComponent();
            displayUserPage = new DisplayUserPageViewModel(user);
            this.BindingContext = displayUserPage;
        }

        private async void TeamSettings_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TeamSettingspage());
        }

        private async void TeamBack_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TeamNotifications());
        }

        private async void MyTeam_Calendar_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Team_Chat());
        }

        private async void Search_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DisplayUser_Homepage());
        }

        private async void Notifications_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TeamNotifications());
        }

    }

}

