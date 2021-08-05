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
public partial class FAQ_page : ContentPage
{
    public FAQ_page()
    {
        InitializeComponent();
    }

    private async void BackToSettings2_Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Settingspage());
    }
}
}