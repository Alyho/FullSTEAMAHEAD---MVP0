using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using FullSteamAheadMVP0Project.Models;
using FullSteamAheadMVP0Project.ViewModels;

namespace FullSteamAheadMVP0Project.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Team_Calendar : ContentPage
    {
        public Team_Calendar()
        {
            InitializeComponent();
        }
        private async void Chat_Button_CLicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Team_Chat());
        }

        private async void Announcements_Button_CLicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Team_Announcements());
        }

        private async void Members_Button_CLicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Team_Members());
        }

        private async void Back_Button_Team(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DisplayUser_Homepage());
        }

    }
}