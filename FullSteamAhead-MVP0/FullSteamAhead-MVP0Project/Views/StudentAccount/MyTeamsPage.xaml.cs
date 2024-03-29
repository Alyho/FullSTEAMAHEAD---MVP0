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
    public partial class MyTeamsPage : ContentPage
    {
        public MyTeamsPageViewModel _viewModel { get; set; }

        public MyTeamsPage()
        {
            InitializeComponent();
            _viewModel = new MyTeamsPageViewModel();
            BindingContext = _viewModel;
        }

        MediaFile file;

        public event PropertyChangedEventHandler PropertyChanged;


        protected async override void OnAppearing()
        {
            base.OnAppearing();
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

                var mi = ((MenuItem)sender);
                var team = mi.CommandParameter as Team;

                    Task.Run(new System.Action(async () =>
                    {
                        await App.Database.RemoveUser(team, Global.UserSignedIn);
                    }));

                    _viewModel.MyTeamsListView.Remove(team);

                //Task.Run(new System.Action(async () =>
                //{
                //    await Navigation.PushAsync(new MyTeamsPage());
                //}));
            
        }

    }


}