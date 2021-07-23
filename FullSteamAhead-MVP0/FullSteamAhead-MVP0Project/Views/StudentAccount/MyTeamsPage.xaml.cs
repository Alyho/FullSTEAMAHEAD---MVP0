﻿using FullSteamAheadMVP0Project.Models;
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
    public partial class MyTeamsPage : ContentPage
    {
        public MyTeamsPage()
        {
            InitializeComponent();
            BindingContext = new MyTeamsPageViewModel();
        }

        private async void MyTeams_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedItem = e.Item as Team;
            Global.SelectedTeam = selectedItem;
            await Navigation.PushAsync(new MyTeams_Chat());

        }

        private async void HomePage_Button_Clicked(object sender, EventArgs e)
        {
                await Navigation.PushAsync(new Homepage());
        }

        private async void Settings_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Settingspage());
        }

        private async void Notifications_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Notifications());
        }

        public void OnDelete(object sender, EventArgs e)
        {

            if (Global.UserSignedIn != null)
            {
                var mi = ((MenuItem)sender);
                var team = mi.CommandParameter as Team;

                if (Global.UserSignedIn != null)
                {
                    Task.Run(new System.Action(async () =>
                    {
                        await App.Database.RemoveUser(team, Global.UserSignedIn);
                    }));
                }

                Task.Run(new System.Action(async () =>
                {
                    await Navigation.PushAsync(new MyTeamsPage());
                }));
            }
        }

    }


}