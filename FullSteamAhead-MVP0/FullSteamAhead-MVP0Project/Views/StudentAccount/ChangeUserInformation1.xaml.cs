using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using FullSteamAheadMVP0Project.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Media.Abstractions;
using Plugin.Media;
using System.Diagnostics;

namespace FullSteamAheadMVP0Project.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangeUserInformation1 : ContentPage
    {

        public Xamarin.Forms.EditorAutoSizeOption AutoSize { get; set; }
        MediaFile file;

        public event PropertyChangedEventHandler PropertyChanged;

        protected async override void OnAppearing()
        {
            base.OnAppearing();
        }


        public MainPageViewModel mainPageViewModel { get; set; }

        public ChangeUserInformation1()
        {
            InitializeComponent();

            mainPageViewModel = new MainPageViewModel(this.Navigation);
            mainPageViewModel.PropertyChanged += MainPageViewModel_PropertyChanged;
            this.BindingContext = mainPageViewModel;
        }

        private async void Back(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Settingspage());
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

        private async void MainPageViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "UserCreated")
            {
                if (mainPageViewModel.UserCreated)
                {
                    await Navigation.PushAsync(new ChangeUserInformation2());
                }

                else
                {
                    await DisplayAlert("Error", "This Username is already taken", "OK");
                }

            }

            if (e.PropertyName == "Unfilled")
            {
                if (mainPageViewModel.Unfilled)
                {
                    await DisplayAlert("Error", "One or more questions unanswered", "OK");
                }
            }

            if (e.PropertyName == "NoIntAge")
            {
                if (mainPageViewModel.NoIntAge)
                {
                    await DisplayAlert("Error", "Age must be a number", "OK");
                }
            }

            if (e.PropertyName == "Dot")
            {
                if (mainPageViewModel.Dot)
                {
                    await DisplayAlert("Error", "Username cannot contain periods", "OK");
                }
            }

        }

    }

}