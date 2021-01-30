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
    public partial class MyTeams_Chat : ContentPage
    {
        public MyTeams_Chat()
        {
            InitializeComponent();
        }

        private async void Calendar_Button_CLicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyTeams_Calendar());
        }

    }


}