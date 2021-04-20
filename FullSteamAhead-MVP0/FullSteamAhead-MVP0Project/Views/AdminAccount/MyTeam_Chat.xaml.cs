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
    public partial class MyTeam_Chat : ContentPage
    {
        public MyTeam_Chat()
        {
            InitializeComponent();
        }

        private async void Calendar2_Button_CLicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyTeam_Calendar());
        }

        private async void Announcements2_Button_CLicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyTeam_Announcements());
        }

        private async void Members2_Button_CLicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyTeam_Members());
        }

    }


}