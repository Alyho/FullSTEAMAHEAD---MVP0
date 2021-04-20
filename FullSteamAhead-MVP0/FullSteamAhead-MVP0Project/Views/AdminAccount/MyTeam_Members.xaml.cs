using FullSteamAheadMVP0Project.Models;
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
    public partial class MyTeam_Members : ContentPage
    {
        public MyTeam_Members()
        {
            InitializeComponent();
            /*BindingContext = new DisplayUser_HomePageViewModel();*/
        }

        /*private async void Members_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedItem = e.Item as User;
            await Navigation.PushAsync(new Member_DisplayUserPage());

        }

        private async void Admins_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedItem = e.Item as Admin;
            await Navigation.PushAsync(new Admin_DisplayUserPage());

        }

        private async void Mentors_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedItem = e.Item as User;
            await Navigation.PushAsync(new Mentor_DisplayUserPage());

        } */

        private async void Chat2_Button_CLicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyTeam_Chat());
        }

        private async void Announcements2_Button_CLicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyTeam_Announcements());
        }

        private async void Calendar2_Button_CLicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyTeam_Calendar());
        }

    }

}