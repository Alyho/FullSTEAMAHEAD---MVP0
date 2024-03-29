﻿using FullSteamAheadMVP0Project.Models;
using FullSteamAheadMVP0Project.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Media.Abstractions;
using System.ComponentModel;

namespace FullSteamAheadMVP0Project.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class MyTeams_Members : ContentPage
{
    public MyTeams_Members()
    {
        InitializeComponent();
        BindingContext = new MembersViewModel();
    }

        MediaFile file;

        public event PropertyChangedEventHandler PropertyChanged;

        protected async override void OnAppearing()
        {
            base.OnAppearing();
        }

        private async void Members_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedItem = e.Item as User;
            var _selectedItem = await App.Database.GetAccountAsync(selectedItem.Username);
            await Navigation.PushAsync(new Member_DisplayUserPage(_selectedItem));

        }

        private async void Admins_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedItem = e.Item as Admin;
            await Navigation.PushAsync(new Admin_DisplayUserPage(selectedItem));

        }


    private async void Chat_Button_CLicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyTeams_Chat());
        }

    private async void Announcements_Button_CLicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyTeams_Announcements());
        }

        private async void Calendar_Button_CLicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyTeams_Calendar());
        }

        private async void Back_Button_MyTeams(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyTeamsPage());
        }

    }

}