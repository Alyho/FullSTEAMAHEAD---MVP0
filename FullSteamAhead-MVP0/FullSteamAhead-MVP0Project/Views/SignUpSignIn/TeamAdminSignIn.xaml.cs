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
public partial class TeamAdminSignIn : ContentPage
{
        public TeamAdminSignInViewModel teamAdminSignInViewModel { get; set; }
        public TeamAdminSignIn()
        {
            InitializeComponent();

            teamAdminSignInViewModel = new TeamAdminSignInViewModel();
            teamAdminSignInViewModel.PropertyChanged += TeamAdminSignInViewModel_PropertyChanged;
            this.BindingContext = teamAdminSignInViewModel;
        }

        private async void TeamAdminSignInViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "TeamExists")
            {
                if (teamAdminSignInViewModel.TeamExists)
                    await Navigation.PushAsync(new DisplayUser_Homepage());
                else
                    await DisplayAlert("Incorrect", "Username or password is not correct", "OK");

            }
        }

    }
}