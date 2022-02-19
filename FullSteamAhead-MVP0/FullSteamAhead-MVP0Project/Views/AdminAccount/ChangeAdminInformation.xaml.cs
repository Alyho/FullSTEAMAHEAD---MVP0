using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using FullSteamAheadMVP0Project.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Plugin.Media.Abstractions;

namespace FullSteamAheadMVP0Project.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangeAdminInformation : ContentPage
    {

        //FirebaseStorageModel firebaseStorageHelper = new FirebaseStorageModel();
        MediaFile file;

        public event PropertyChangedEventHandler PropertyChanged;

        private string _adminusername;
        public string adminusername
        {
            get
            {
                return _adminusername;
            }
            set
            {
                _adminusername = value;
                var args = new PropertyChangedEventArgs(nameof(adminusername));
                PropertyChanged?.Invoke(this, args);
            }
        }


        protected async override void OnAppearing()
        {
            base.OnAppearing();
        }

        public CreateTeamAccountViewModel createTeamAccountViewModel { get; set; }

        public ChangeAdminInformation()
        {
            InitializeComponent();
            createTeamAccountViewModel = new CreateTeamAccountViewModel(this.Navigation);
            createTeamAccountViewModel.PropertyChanged += CreateTeamAccountViewModel_PropertyChanged;
            this.BindingContext = createTeamAccountViewModel;
        }

        private async void CreateTeamAccountViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "AdminExists")
            {
                if (createTeamAccountViewModel.AdminExists)
                {
                    await DisplayAlert("Error", "This Username is already taken", "OK");
                }
            }

            if (e.PropertyName == "Dot")
            {
                if (createTeamAccountViewModel.Dot)
                {
                    await DisplayAlert("Error", "Username cannot contain periods", "OK");
                }
            }

        }

        private async void Back3(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChangeTeamInformation2());
        }

    }

}