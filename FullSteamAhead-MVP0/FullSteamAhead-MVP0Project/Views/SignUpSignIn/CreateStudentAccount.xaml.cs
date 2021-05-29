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
public partial class CreateStudentAccount : ContentPage
{
        public MainPageViewModel mainPageViewModel { get; set; }

        public CreateStudentAccount()
        {
            InitializeComponent();

            mainPageViewModel = new MainPageViewModel(this.Navigation);
            mainPageViewModel.PropertyChanged += MainPageViewModel_PropertyChanged;
            this.BindingContext = mainPageViewModel;
        }

        private async void MainPageViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "UserCreated")
            {
                if (mainPageViewModel.UserCreated)
                {
                    await Navigation.PushAsync(new Homepage());
                }

                else
                {
                    await DisplayAlert("Error", "This Username is already taken", "OK");
                }
                    
            }
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