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
            await Navigation.PushAsync(new TeamSettingspage());
        }

        //private async void BtnDelete_Clicked(object sender, EventArgs e)
        //{
        //    await firebaseStorageHelper.DeleteFile(Teamusername.Text);
        //    lblPath.Text = string.Empty;
        //    await DisplayAlert("Success", "Deleted", "OK");
        //}

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

        //    await firebaseStorageHelper.UploadFile(file.GetStream(), Teamusername.Text);

        //}


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
                    await DisplayAlert("Updated Team", "Team account information changed", "OK");
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

            if (e.PropertyName == "Dot")
            {
                if (createTeamAccountViewModel.Dot)
                {
                    await DisplayAlert("Error", "Username cannot contain periods", "OK");
                }
            }
        }


    }

}