using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FullSteamAheadMVP0Project.ViewModels;
using Xamarin.Forms;

namespace FullSteamAheadMVP0Project.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

        }

        private async void CreateAnAccount_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPage());
        }
        private async void TeamMember_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TeamMemberSignIn());
        }
        private async void TeamAdmin_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TeamAdminSignIn());
        }


    }
}