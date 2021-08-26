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
    public partial class ChangeTeamInformation1 : ContentPage
    {
        public CreateTeamAccountViewModel createTeamAccountViewModel { get; set; }

        public ChangeTeamInformation1()
        {
            InitializeComponent();

            createTeamAccountViewModel = new CreateTeamAccountViewModel(this.Navigation);
            createTeamAccountViewModel.PropertyChanged += CreateTeamAccountViewModel_PropertyChanged;
            this.BindingContext = createTeamAccountViewModel;
        }

        private async void Back1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Settingspage());
        }

        private async void CreateTeamAccountViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            if (e.PropertyName == "TeamCreated")
            {

                if (createTeamAccountViewModel.TeamCreated == 1)
                {
                    await DisplayAlert("Try Again", "This Username is already taken", "OK");
                }

                else if (createTeamAccountViewModel.TeamCreated == 2)
                {
                    await DisplayAlert("New Team", "Team account information changed", "OK");
                    await Navigation.PushAsync(new ChangeTeamInformation2());
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


    }

}