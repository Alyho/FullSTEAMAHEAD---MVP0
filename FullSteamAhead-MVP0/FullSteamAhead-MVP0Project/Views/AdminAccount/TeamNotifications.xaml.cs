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
    public partial class TeamNotifications : ContentPage
    {
        public TeamNotifications()
        {
            InitializeComponent();
            /*BindingContext = new NotificationsViewModel();*/
        }

        private async void TeamChat_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Team_Chat());
        }

        private async void TeamSettings_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TeamSettingspage());
        }


        private async void Search_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DisplayUser_Homepage());
        }

        private async void Notifications_ItemTapped(object sender, ItemTappedEventArgs e)
        {

            var selectedItem2 = e.Item as FullSteamAheadMVP0Project.ViewModels.Container;
            var _selectedItem2 = await App.Database.GetAccountAsync(selectedItem2.Username);
            await Navigation.PushAsync(new DisplayUserPage(_selectedItem2));


        }
    }



}