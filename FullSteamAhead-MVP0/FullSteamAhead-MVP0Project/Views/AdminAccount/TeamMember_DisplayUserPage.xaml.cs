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
    public partial class TeamMember_DisplayUserPage : ContentPage
    {
        public TeamMember_DisplayUserPage()
        {
        }

        public TeamMember_DisplayUserPage(User user)
        {
            InitializeComponent();
            BindingContext = new DisplayUserPageViewModel(user);
        }

        private async void Back(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Team_Members());
        }

    }

}
