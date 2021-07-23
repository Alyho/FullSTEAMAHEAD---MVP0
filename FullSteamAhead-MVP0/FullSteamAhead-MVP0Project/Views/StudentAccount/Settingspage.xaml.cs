using FullSteamAheadMVP0Project.Models;
using FullSteamAheadMVP0Project.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FullSteamAheadMVP0Project.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settingspage : ContentPage
    {

        public Settingspage()
        {
            InitializeComponent();
        }

        private async void MainPage_Button_Clicked(object sender, EventArgs e)
        {
            Global.UserSignedIn = null;
            await Navigation.PushAsync(new MainPage());
        }
        private async void HomePage_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Homepage());
        }

        private async void Settings_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Settingspage());
        }

        private async void ChangeAccountInfo_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChangeUserInformation1());
        }

        private async void Notifications_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Notifications());
        }


    }

}