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
        public MainPageViewModel mainPageViewModel { get; set; }

        public CreateTeamAccount()
        {
            InitializeComponent();

            mainPageViewModel = new MainPageViewModel();
            mainPageViewModel.PropertyChanged += MainPageViewModel_PropertyChanged;
            this.BindingContext = mainPageViewModel;
        }

        private async void MainPageViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "UserCreated")
            {
                if (mainPageViewModel.UserCreated)
                    await DisplayAlert("Account Created", "", "OK");
                else
                    await DisplayAlert("Unavailable", "This username is already taken", "OK");

            }
        }

        private async void SignUpHomepage_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Homepage());
        }

    }

}