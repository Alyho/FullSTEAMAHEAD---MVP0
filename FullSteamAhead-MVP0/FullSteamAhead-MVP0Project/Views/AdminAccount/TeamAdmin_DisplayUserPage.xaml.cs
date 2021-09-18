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
    public partial class TeamAdmin_DisplayUserPage : ContentPage
    {
        public TeamAdmin_DisplayUserPage()
        {
        }

        public TeamAdmin_DisplayUserPage(Admin admin)
        {
            InitializeComponent();
            BindingContext = new AdminDisplayViewModel(admin);
        }

        private async void Back(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Team_Members());
        }

    }

}

