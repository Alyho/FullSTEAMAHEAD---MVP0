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
public partial class DisplayUser_Homepage : ContentPage
{
    public DisplayUser_Homepage()
    {
        InitializeComponent();
        
        /*BindingContext = new DisplayUser_HomePageViewModel();*/
    }
}
}