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
    public partial class Notifications : ContentPage
    {
        public Notifications()
        {
            InitializeComponent();
        
            BindingContext = new NotificationsViewModel(this.Navigation);

        }

        private async void HomePage_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Homepage());
        }

        private async void Settings_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Settingspage());
        }

        private async void MyTeams_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyTeamsPage());

        }

        private async void Notifications_ItemTapped(object sender, ItemTappedEventArgs e)
        {
             
             var selectedItem = e.Item as FullSteamAheadMVP0Project.ViewModels.Container;
             var _selectedItem = await App.Database.GetTeamAsync(selectedItem.Username);
            await Navigation.PushAsync(new Notifications_DisplayTeamInformation(_selectedItem));

        }

        }
    
}