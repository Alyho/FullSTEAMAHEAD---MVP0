using FullSteamAheadMVP0Project.Models;
using FullSteamAheadMVP0Project.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Media.Abstractions;
using System.ComponentModel;

namespace FullSteamAheadMVP0Project.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Member_DisplayUserPage : ContentPage
    {
        public Member_DisplayUserPage()
        {
        }

        MediaFile file;

        public event PropertyChangedEventHandler PropertyChanged;

        protected async override void OnAppearing()
        {
            base.OnAppearing();
        }

        public Member_DisplayUserPage(User user)
        {
            InitializeComponent();
            BindingContext = new DisplayUserPageViewModel(user);
        }

        private async void Back(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyTeams_Members());
        }

    }

}

