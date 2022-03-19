using FullSteamAheadMVP0Project.Models;
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
    public partial class Team_Members : ContentPage
    {
        public Team_Members()
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
            await Navigation.PushAsync(new TeamMember_DisplayUserPage(_selectedItem));

        }

        private async void Admins_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedItem = e.Item as Admin;
            await Navigation.PushAsync(new TeamAdmin_DisplayUserPage(selectedItem));

        }


        private async void Chat_Button_CLicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Team_Chat());
        }

        private async void Announcements_Button_CLicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Team_Announcements());
        }

        private async void Calendar_Button_CLicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Team_Calendar());
        }

        public void OnDelete2(object sender, EventArgs e)
        {

            if (Global.TeamSignedIn != null)
            {
                var mi = ((MenuItem)sender);
                var user = mi.CommandParameter as User;

                if (Global.TeamSignedIn != null)
                {
                    Task.Run(new System.Action(async () =>
                    {
                        await App.Database.RemoveUser(Global.TeamSignedIn, user);
                    }));
                }

                Task.Run(new System.Action(async () =>
                {
                    await Navigation.PushAsync(new Team_Members());
                }));
            }

        }

        public void OnDelete3(object sender, EventArgs e)
        {

            if (Global.TeamSignedIn != null)
            {
                var mi = ((MenuItem)sender);
                var admin = mi.CommandParameter as Admin;

                if (Global.TeamSignedIn != null)
                {
                    Task.Run(new System.Action(async () =>
                    {
                        await App.Database.RemoveTeamAdmin(Global.TeamSignedIn, admin);
                    }));
                }

                Task.Run(new System.Action(async () =>
                {
                    await Navigation.PushAsync(new Team_Members());
                }));
            }

        }

        private async void Back_Button_MyTeams(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DisplayUser_Homepage());
        }

    }

}