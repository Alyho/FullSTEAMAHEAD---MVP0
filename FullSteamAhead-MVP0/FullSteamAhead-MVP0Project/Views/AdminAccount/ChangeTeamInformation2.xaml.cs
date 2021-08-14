using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using FullSteamAheadMVP0Project.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FullSteamAheadMVP0Project.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangeTeamInformation2 : ContentPage
    {
        public CreateTeamAccountViewModel createTeamAccountViewModel { get; set; }

        public ChangeTeamInformation2()
        {
            InitializeComponent();

            createTeamAccountViewModel = new CreateTeamAccountViewModel(this.Navigation);
            this.BindingContext = createTeamAccountViewModel;
        }

        private async void Back2(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChangeTeamInformation1());
        }
    }
}