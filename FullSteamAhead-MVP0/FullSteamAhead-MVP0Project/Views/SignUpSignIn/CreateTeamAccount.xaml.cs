using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using FullSteamAheadMVP0Project.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FullSteamAheadMVP0Project.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateTeamAccount : ContentPage
    {
        public CreateTeamAccountViewModel createTeamAccountViewModel { get; set; }

        public CreateTeamAccount()
        {
            InitializeComponent();

            createTeamAccountViewModel = new CreateTeamAccountViewModel();
            createTeamAccountViewModel.PropertyChanged += CreateTeamAccountViewModel_PropertyChanged;
            this.BindingContext = createTeamAccountViewModel;
        }

        private async void CreateTeamAccountViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "TeamCreated")
            {
                if (createTeamAccountViewModel.TeamCreated == 0)
                {
                    await DisplayAlert("New Admin", "You are creating a new admin account for an existing team", "OK");
                    //await Navigation.PushAsync(new CreateAdminAccount());
                }
                
                else if (createTeamAccountViewModel.TeamCreated == 1)
                {
                    await DisplayAlert("Try Again", "This Username is already taken", "OK");
                }

                else if (createTeamAccountViewModel.TeamCreated == 2)
                {
                    await DisplayAlert("New Team", "New team account created", "OK");
                    //await Navigation.PushAsync(new CreateAdminAccount());
                }

            }

        }

        private void SignUpHomepage_Button_Clicked(object sender, EventArgs e)
        {

        }

        /*
         * Put this code into CreateAdminAccount.xaml.cs:
         * private async void CreateTeamAccountViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "AdminExists")
            {
                if (createTeamAccountViewModel.AdminExists)
                {
                    await DisplayAlert("Error", "This Username is already taken", "OK");
                }
                
                else 
                {
                    await DisplayAlert("Success", "New Admin Created", "OK");
                    await Navigation.PushAsync(new DisplayUser_Homepage());
               }
            }

        }

         */

    }

}