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
public partial class TeamNotifications : ContentPage
{
    public TeamNotifications()
    {
        InitializeComponent();
        /*BindingContext = new NotificationsViewModel();*/
    }

    private async void MyTeam_Calendar_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyTeams_Chat());
        }

    private async void TeamSettings_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TeamSettingspage());
        }
    }
}