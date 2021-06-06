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
    public partial class Notifications : ContentPage
    {
        public Notifications()
        {
            InitializeComponent();
            BindingContext = new NotificationsViewModel();

        }

        private async void HomePage_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Homepage());
        }

        private async void Settings_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Settingspage());
        }

        private async void Notifications_ItemTapped(object sender, ItemTappedEventArgs e)
        {
                if (Global.UserSignedIn != null)
                {
                    var selectedItem = e.Item as Team;
                    await Navigation.PushAsync(new DisplayTeamInformation(selectedItem));
                }

                else if (Global.TeamSignedIn != null)
                {
                    var selectedItem2 = e.Item as User;
                    await Navigation.PushAsync(new DisplayUserPage(selectedItem2));
                }

        }

        }
    
}