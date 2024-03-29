﻿using FullSteamAheadMVP0Project.Models;
using FullSteamAheadMVP0Project.ViewModels;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FullSteamAheadMVP0Project.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DisplayUserPage : ContentPage
    {

        MediaFile file;

        public event PropertyChangedEventHandler PropertyChanged;


        protected async override void OnAppearing()
        {
            base.OnAppearing();
        }

        public DisplayUserPageViewModel displayUserPage { get; set; }

        public DisplayUserPage(User user)
        {
            InitializeComponent();
            displayUserPage = new DisplayUserPageViewModel(user);
            displayUserPage.PropertyChanged += DisplayUserPageViewModel_PropertyChanged;
            this.BindingContext = displayUserPage;

          
        }

        private async void DisplayUserPageViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "TeamRequestExists")
            {
                if (displayUserPage.TeamRequestExists)
                {
                    await DisplayAlert("Sorry", "You have already invited this user", "OK");
                }
                
                else if (displayUserPage.UserOnTeam)
                {
                    await DisplayAlert("Sorry", "This user is already on your team", "OK");
                }
                
                else
                {
                    await DisplayAlert("Invite was successful", "You have invited this user to join this team", "OK");
                }

            }

            if (e.PropertyName == "NoEmail")
            {
                if (displayUserPage.NoEmail)
                {
                    await DisplayAlert("Email Not Supported", "In order to notify the team you requested, send a direct email manually.", "OK");
                }
            }

        }

        private async void TeamSettings_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TeamSettingspage());
        }

        private async void TeamBackToSearch_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DisplayUser_Homepage());
        }

        private async void MyTeam_Calendar_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Team_Chat());
        }

        private async void Notifications_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TeamNotifications());
        }

        private async void Search_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DisplayUser_Homepage());
        }

    }

}

