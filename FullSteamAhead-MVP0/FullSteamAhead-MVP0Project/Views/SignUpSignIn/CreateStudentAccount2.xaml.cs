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
    public partial class CreateStudentAccount2 : ContentPage
    {
        public MainPageViewModel mainPageViewModel { get; set; }

        public CreateStudentAccount2()
        {
            InitializeComponent();

            mainPageViewModel = new MainPageViewModel();
            mainPageViewModel.PropertyChanged += MainPageViewModel_PropertyChanged;
            this.BindingContext = mainPageViewModel;
        }

        private async void MainPageViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            /*if (e.PropertyName == "UserSecondStep")
            {
                if (mainPageViewModel.UserSecondStep)
                    await Navigation.PushAsync(new CreateStudentAccount3());

            }*/
        }

        private async void CreateStudentAccount3_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateStudentAccount3());
        }

    }

}