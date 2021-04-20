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
    public partial class MyTeam_Announcements : ContentPage
    {
        public MyTeam_Announcements()
        {
            InitializeComponent();
        }
        private async void Chat2_Button_CLicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyTeam_Chat());
        }

        private async void Calendar2_Button_CLicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyTeam_Calendar());
        }

        private async void Members2_Button_CLicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyTeam_Members());
        }

    }
}