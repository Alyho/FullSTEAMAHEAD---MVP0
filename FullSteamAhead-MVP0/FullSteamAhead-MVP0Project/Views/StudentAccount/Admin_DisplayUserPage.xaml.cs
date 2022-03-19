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
    public partial class Admin_DisplayUserPage : ContentPage
    {
        public Admin_DisplayUserPage()
        {
        }

        MediaFile file;

        public event PropertyChangedEventHandler PropertyChanged;

        protected async override void OnAppearing()
        {
            base.OnAppearing();
        }

        public Admin_DisplayUserPage(Admin admin)
        {
            InitializeComponent();
            BindingContext = new AdminDisplayViewModel(admin);
        }

        private async void Back(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyTeams_Members());
        }

    }

}

