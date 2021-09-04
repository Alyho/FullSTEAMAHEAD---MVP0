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
public partial class SignUpPage : ContentPage
{
    public SignUpPage()
    {
        InitializeComponent();
    }

        private async void LookingForTeam_Button_Clicked (object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateStudentAccount());
        }

        private async void Back_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

    private async void Admin_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateTeamAccount());
        }
    }
}