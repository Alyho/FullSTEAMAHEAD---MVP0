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
    public partial class MyTeam_Calendar : ContentPage
    {
        public MyTeam_Calendar()
        {
            InitializeComponent();
        }
        private async void Chat2_Button_CLicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyTeam_Chat());
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