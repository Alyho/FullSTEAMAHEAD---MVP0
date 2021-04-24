using FullSteamAheadMVP0Project.Models;
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
    public partial class Homepage : ContentPage
    {
        public Homepage()
        {
            InitializeComponent();

            BindingContext = new HomePageViewModel(this.Navigation);
        }

        private async void searchResults_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedItem = e.Item as Team;
            await Navigation.PushAsync(new DisplayTeamInformation(selectedItem));

            
        }

        private async void HomePage_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Homepage());
        }

        private async void Settings_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Settingspage());
        }

        private async void Notifications_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Notifications());
        }

    }
}