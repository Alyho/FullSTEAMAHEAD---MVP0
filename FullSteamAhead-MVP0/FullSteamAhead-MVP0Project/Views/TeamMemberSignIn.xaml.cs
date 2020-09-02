using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FullSteamAheadMVP0Project.ViewModels;

namespace FullSteamAheadMVP0Project.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeamMemberSignIn : ContentPage
    {
        public MainPageViewModel mainPageViewModel { get; set; }

        public TeamMemberSignIn()
        {
            InitializeComponent();

            mainPageViewModel = new MainPageViewModel();
            mainPageViewModel.PropertyChanged += MainPageViewModel_PropertyChanged;
            this.BindingContext = mainPageViewModel;
        }

        private async void MainPageViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "UserExists")
            {
                if (mainPageViewModel.UserExists)
                    await DisplayAlert("Correct", "", "OK");
                else
                    await DisplayAlert("Incorrect", "Username or password is not correct", "OK");

            }
        }

        private async void TeamMemberSignIn_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Homepage());
        }

    }
}