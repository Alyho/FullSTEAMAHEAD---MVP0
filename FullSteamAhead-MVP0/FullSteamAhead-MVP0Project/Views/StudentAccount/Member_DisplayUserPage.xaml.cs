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
    public partial class Member_DisplayUserPage : ContentPage
    {
        public Member_DisplayUserPage()
        {
        }

        public Member_DisplayUserPage(User user)
        {
            InitializeComponent();
            BindingContext = new DisplayUserPageViewModel(user);
        }

        private async void Back(object sender, EventArgs e)
        {
            if (Global.TeamSignedIn != null)
            {
                await Navigation.PushAsync(new MyTeams_Members());
            }

            else if (Global.UserSignedIn != null)
            {
                await Navigation.PushAsync(new Team_Members());
            }
        }

    }

}

