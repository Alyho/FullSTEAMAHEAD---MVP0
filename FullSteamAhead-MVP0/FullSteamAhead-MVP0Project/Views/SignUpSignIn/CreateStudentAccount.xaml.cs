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
using System.Diagnostics;
using System.IO;
using Plugin.Media;

namespace FullSteamAheadMVP0Project.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class CreateStudentAccount : ContentPage
{
        public MainPageViewModel mainPageViewModel { get; set; }
        public Xamarin.Forms.EditorAutoSizeOption AutoSize { get; set; }


        public CreateStudentAccount()
        {

            //FirebaseStorageModel firebaseStorageHelper = new FirebaseStorageModel();
            MediaFile file;
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
                    
                    //if checkbox is checked:  
                    await Navigation.PushAsync(new Homepage());
                }

                else
                {
                    await DisplayAlert("Error", "This Username is already taken", "OK");
                    Global.UserSignedIn = null;
                }
                    
            }

            if (e.PropertyName == "Unfilled")
            {
                if (mainPageViewModel.Unfilled)
                {
                    await DisplayAlert("Error", "One or more questions unanswered", "OK");
                    Global.UserSignedIn = null;
                }
            }

            if (e.PropertyName == "NoIntAge")
            {
                if (mainPageViewModel.NoIntAge)
                {
                    await DisplayAlert("Error", "Age must be a number", "OK");
                    Global.UserSignedIn = null;
                }
            }

            if (e.PropertyName == "Dot")
            {
                if (mainPageViewModel.Dot)
                {
                    await DisplayAlert("Error", "Username cannot contain periods", "OK");
                    Global.UserSignedIn = null;
                }
            }
        }

        //private async void BtnUpload_Clicked(object sender, EventArgs e)
        //{

        //    await CrossMedia.Current.Initialize();
        //    try
        //    {
        //        file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
        //        {
        //            PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
        //        });
        //        if (file == null)
        //            return;
        //        imgChoosed.Source = ImageSource.FromStream(() =>
        //        {
        //            var imageStram = file.GetStream();
        //            return imageStram;
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex.Message);
        //    }

        //    await firebaseStorageHelper.UploadFile(file.GetStream(), Username.Text);

        //}

        void OnCheckBoxCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            
        }

        protected void PrivacyPolicy(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri("https://www.privacypolicies.com/live/e9b6a4c7-b65e-462a-8a44-91d78e8b99c7"));
        }

        protected void TermsAndConditions(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri("https://www.privacypolicies.com/live/a7b55c2a-bd8b-45c1-9a62-86a16b1714b3"));
        }

        async void OnDisplayAlertButtonClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Role", "Chose to be a student or mentor. Mentors are expected to already have more than adequate experience, but will have the same features as students. They're an experienced adult who voluntarily selects teams to be a mentor for, providing information and help when needed.", "OK");
        }

        private async void Back_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPage());
        }


    }

}