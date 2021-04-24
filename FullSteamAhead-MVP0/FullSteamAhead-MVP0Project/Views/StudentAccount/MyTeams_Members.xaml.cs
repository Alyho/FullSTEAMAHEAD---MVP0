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
public partial class MyTeams_Members : ContentPage
{
    public MyTeams_Members()
    {
        InitializeComponent();
        BindingContext = new MembersViewModel();
    }

        private async void Members_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedItem = e.Item as User;
            await Navigation.PushAsync(new Member_DisplayUserPage(selectedItem));

        }

        private async void Admins_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedItem = e.Item as Admin;
            await Navigation.PushAsync(new Admin_DisplayUserPage(selectedItem));

        }

        private async void Mentors_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedItem = e.Item as User;
            await Navigation.PushAsync(new Mentor_DisplayUserPage(selectedItem));

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

    }

}