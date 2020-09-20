using FullSteamAheadMVP0Project.Models;
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
    public partial class Homepage2 : ContentPage
    {
        public IList<Team> TeamListView { get; private set; }
        public Homepage2()
        {
            InitializeComponent();

            TeamListView = new List<Team>();

            BindingContext = this;

            
        }
        void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Team selectedItem = e.SelectedItem as Team;
        }

        void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            Team tappedItem = e.Item as Team;
        }

    }
}