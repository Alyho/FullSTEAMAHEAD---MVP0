using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UsernamePasswordProject.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class SignUpPage : ContentPage
{
    public SignUpPage()
    {
        InitializeComponent();
    }

    private async void LookingForTeam_Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CreateStudentAccount());
    }
}
}