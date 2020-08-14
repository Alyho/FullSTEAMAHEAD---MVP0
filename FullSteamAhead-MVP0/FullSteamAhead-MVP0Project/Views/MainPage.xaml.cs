using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsernamePasswordProject.ViewModels;
using Xamarin.Forms;

namespace UsernamePasswordProject.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {

        public MainPageViewModel mainPageViewModel { get; set; }

        public MainPage()
        {
            InitializeComponent();

            mainPageViewModel=new MainPageViewModel();
            this.BindingContext = mainPageViewModel;

            mainPageViewModel.PropertyChanged += MainPageViewModel_PropertyChanged;
        }

        private async void MainPageViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName=="UserCreated")
            {
                //if (mainPageViewModel.UserCreated)
                //    await Navigation.PushAsync(new ShowDataPage(mainPageViewModel));
                //else
                //    await DisplayAlert("Unavailable", "This username is already taken", "OK");

            }
        }

    }
}