using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using FullSteamAheadMVP0Project.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace FullSteamAheadMVP0Project.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateAdminAccount : ContentPage
    {
        public CreateTeamAccountViewModel createTeamAccountViewModel { get; set; }

        public CreateAdminAccount()
        {
            InitializeComponent();
            createTeamAccountViewModel = new CreateTeamAccountViewModel(this.Navigation);
            createTeamAccountViewModel.PropertyChanged += CreateTeamAccountViewModel_PropertyChanged;
            this.BindingContext = createTeamAccountViewModel;
        }

        private async void CreateTeamAccountViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
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

            if (e.PropertyName == "Unfilled")
            {
                if (createTeamAccountViewModel.Unfilled)
                {
                    await DisplayAlert("Error", "One or more questions unanswered.", "OK");
                }
            }
        }

        private async void Back_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateTeamAccount2());
        }

        protected void PrivacyPolicy(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri("https://www.privacypolicies.com/live/e9b6a4c7-b65e-462a-8a44-91d78e8b99c7"));
        }

        protected void TermsAndConditions(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri("https://www.privacypolicies.com/live/a7b55c2a-bd8b-45c1-9a62-86a16b1714b3"));
        }


    }

}