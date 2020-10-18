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

            BindingContext = new HomePageViewModel();
        }

        private async void DisplayTeam_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DisplayTeamInformation());
        }

        private void searchResults_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }

        private void searchResults_ItemTapped_1(object sender, ItemTappedEventArgs e)
        {

        }
    }
}