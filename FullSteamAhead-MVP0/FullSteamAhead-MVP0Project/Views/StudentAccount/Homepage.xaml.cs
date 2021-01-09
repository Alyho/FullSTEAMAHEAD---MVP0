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
            var homepageviewmodel = new HomePageViewModel();
            homepageviewmodel.Navigation = Navigation;
            BindingContext = homepageviewmodel;
        }

        /*
        private async void DisplayTeam_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DisplayTeamInformation());
        }
        */

        private async void searchResults_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedItem = e.Item as Team;
            await Navigation.PushAsync(new DisplayTeamInformation(selectedItem));

            
        }

        private async void HomePage_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Homepage());
        }
    }
}