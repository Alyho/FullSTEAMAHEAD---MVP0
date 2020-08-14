using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;
using UsernamePasswordProject.Models;

namespace UsernamePasswordProject.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public Command SaveCommand { get; }

        private ObservableCollection<Account> _userListView;

        public event PropertyChangedEventHandler PropertyChanged;

        private string Username_;
        private string Password_;

        private bool _userCreated;

        public bool UserCreated
        {
            get { return _userCreated; }
        }

        public MainPageViewModel()
        {
            _userListView = new ObservableCollection<Account>();
            //get existing users from database to populate the collection
            PopulateUsers();

            SaveCommand = new Command(async () =>
            {
                var _user = new Account
                {
                    Username = Username_,
                    Password = Password_
                };

                //call the database to find any users
                var found = await App.Database.GetAccountAsync(_user.Username);
                
                if (found != null) 
                {
                    //user already exists
                    _userCreated = false;
                }
                else
                {
                    //save the new user
                    await App.Database.SaveAccountAsync(_user);
                    _userListView.Add(_user);

                    _userCreated = true;
                }

                //Raise the Property Changed Event to notify the MainPage
                var ar = new PropertyChangedEventArgs(nameof(UserCreated));
                PropertyChanged?.Invoke(this, ar);

                //clear the textboxes
                Username = string.Empty;
                Password = string.Empty;
                
            });

        }

        public async void PopulateUsers()
        {
            var users = await App.Database.GetAccountsAsync();
            foreach (var user in users)
            {
                _userListView.Add(user);
            }
        }

        public ObservableCollection<Account> userListView
        {
            get { return _userListView; }
            set
            {
                if (_userListView != value)
                {
                    _userListView = value;
                    var args = new PropertyChangedEventArgs(nameof(userListView));
                    PropertyChanged?.Invoke(this, args);
                }
            }
        }

        public string Username
        {
            get => Username_;
            set
            {
                if (Username_ != value)
                {
                    Username_ = value;
                    var args = new PropertyChangedEventArgs(nameof(Username));
                    PropertyChanged?.Invoke(this, args);
                }
            }
        }

        public string Password
        {
            get => Password_;
            set
            {
                if (Password_ != value)
                {
                    Password_ = value;
                    var args = new PropertyChangedEventArgs(nameof(Password));
                    PropertyChanged?.Invoke(this, args);
                }
            }
        }


    }
}
