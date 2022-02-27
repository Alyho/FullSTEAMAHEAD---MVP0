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
using FullSteamAheadMVP0Project.Models;
using Plugin.Media.Abstractions;
using Plugin.Media;
using System.Diagnostics;

namespace FullSteamAheadMVP0Project.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateAdminAccount : ContentPage
    {
        public CreateTeamAccountViewModel createTeamAccountViewModel { get; set; }

        public Xamarin.Forms.EditorAutoSizeOption AutoSize { get; set; }
        MediaFile file;

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
                    await App.Database.UploadTeamAdminFile(file.GetStream(), Global.TeamSignedIn.Team_Username, Global.AdminSignedIn.Username);
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

            if (e.PropertyName == "Dot")
            {
                if (createTeamAccountViewModel.Dot)
                {
                    await DisplayAlert("Error", "Username cannot contain periods", "OK");
                    Global.AdminSignedIn = null;
                }
            }
        }

        private async void BtnUpload_Clicked(object sender, EventArgs e)
        {

            await CrossMedia.Current.Initialize();
            try
            {
                file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
                });
                if (file == null)
                    return;
                imgChoosed.Source = ImageSource.FromStream(() =>
                {
                    var imageStram = file.GetStream();
                    return imageStram;
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }

        private async void Back_Button_Clicked(object sender, EventArgs e)
        {
            if (Global.TeamSignedIn.Team_Admins.Count != 0)
            {
                await Navigation.PushAsync(new CreateTeamAccount());
                Global.TeamSignedIn = null;
            }
            else
            {
                await Navigation.PushAsync(new CreateTeamAccount2());
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