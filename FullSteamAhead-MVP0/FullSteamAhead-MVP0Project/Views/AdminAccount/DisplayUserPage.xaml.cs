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
    public partial class DisplayUserPage : ContentPage
    {
        public DisplayUserPageViewModel displayUserPage { get; set; }

        public DisplayUserPage(User user)
        {
            InitializeComponent();
            displayUserPage = new DisplayUserPageViewModel(user);
            displayUserPage.PropertyChanged += DisplayUserPageViewModel_PropertyChanged;
            this.BindingContext = displayUserPage;
        }

        private async void DisplayUserPageViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "TeamRequestExists")
            {
                if (displayUserPage.TeamRequestExists)
                {
                    await DisplayAlert("Sorry", "You have already invited this user", "OK");
                }

                else
                {
                    await DisplayAlert("Invite was successful", "You have invited this user to join this team", "OK");
                }
            }
            
            
        }

        private async void TeamSettings_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TeamSettingspage());
        }

    }

}

