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
    public partial class Admin_DisplayUserPage : ContentPage
    {
        public Admin_DisplayUserPage()
        {
        }

        public Admin_DisplayUserPage(Admin admin)
        {
            InitializeComponent();
            /*BindingContext = new ViewModel(admin);*/
        }

    }

}

