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
    public partial class MyTeams_Announcements : ContentPage
    {
        public MyTeams_Announcements()
        {
            InitializeComponent();
        }
        private async void Chat_Button_CLicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyTeams_Chat());
        }

        private async void Calendar_Button_CLicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyTeams_Calendar());
        }

        private async void Members_Button_CLicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyTeams_Members());
        }

    }
}