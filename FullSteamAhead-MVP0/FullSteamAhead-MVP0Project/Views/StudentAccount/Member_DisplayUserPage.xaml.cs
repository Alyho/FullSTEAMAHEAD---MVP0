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
    public partial class Member_DisplayUserPage : ContentPage
    {
        public Member_DisplayUserPage()
        {
        }

        public Member_DisplayUserPage(User user)
        {
            InitializeComponent();
            BindingContext = new DisplayUserPageViewModel(user);
        }

    }

}

