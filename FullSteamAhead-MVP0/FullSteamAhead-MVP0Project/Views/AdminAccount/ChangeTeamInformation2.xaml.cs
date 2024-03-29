﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using FullSteamAheadMVP0Project.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FullSteamAheadMVP0Project.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangeTeamInformation2 : ContentPage
    {
        public CreateTeamAccountViewModel createTeamAccountViewModel { get; set; }

        public ChangeTeamInformation2()
        {
            InitializeComponent();

            createTeamAccountViewModel = new CreateTeamAccountViewModel(this.Navigation);
            createTeamAccountViewModel.PropertyChanged += CreateTeamAccountViewModel_PropertyChanged;
            this.BindingContext = createTeamAccountViewModel;
        }

        private async void Back2(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChangeTeamInformation1());
        }

        async void OnDisplayAlertButtonClicked4(object sender, EventArgs e)
        {
            await DisplayAlert("Online Team", "If your team is an online team, please write N/A for city and zip code", "OK");
        }

        async void OnDisplayAlertButtonClicked5(object sender, EventArgs e)
        {
            await DisplayAlert("Privacy", "Public teams are available to all searching members. Private teams will be invite only and will not be searchable.", "OK");
        }

        private async void CreateTeamAccountViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            if (e.PropertyName == "Unfilled")
            {
                if (createTeamAccountViewModel.Unfilled)
                {
                    await DisplayAlert("Error", "One or more questions unanswered.", "OK");
                }
            }

            if (e.PropertyName == "NoIntAge")
            {
                if (createTeamAccountViewModel.NoIntAge)
                {
                    await DisplayAlert("Error", "Age must be a number", "OK");
                }
            }
        }

    }
}