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

            mainPageViewModel = new MainPageViewModel(this.Navigation);
            this.BindingContext = mainPageViewModel;
        }

        

    }

}