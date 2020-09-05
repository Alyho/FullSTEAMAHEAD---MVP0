using System;
using System.IO;
using FullSteamAheadMVP0Project.Models;
using FullSteamAheadMVP0Project.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FullSteamAheadMVP0Project
{
    public partial class App : Application
    {
        
        static IDatabase database;

        public static IDatabase Database
        {
            get
            { 
                if (database == null)
                {
                    //database = new LocalDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "accounts.db3"));
                    database = new FirebaseDatabase("https://full-steam-ahead---mvp-0.firebaseio.com/");
                }
                return database;
            }
        }
        

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
