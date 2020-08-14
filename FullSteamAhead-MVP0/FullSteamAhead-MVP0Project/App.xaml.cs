using System;
using System.IO;
using UsernamePasswordProject.Views;
using UsernamePasswordProject.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UsernamePasswordProject
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
                    database = new FirebaseDatabase("https://usernamepasswordproject.firebaseio.com/");
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
