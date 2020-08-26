using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;
using FullSteamAheadMVP0Project.Models;

namespace FullSteamAheadMVP0Project.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public Command SaveCommand { get; }
        public Command CheckUserCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        private string Username_;
        private string Password_;

        private bool _userCreated;
        private bool _userExists;

        public bool UserCreated
        {
            get { return _userCreated; }
        }

        public bool UserExists
        {
            get { return _userExists; }
        }

        public MainPageViewModel()
        {

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

                    _userCreated = true;
                }

                //Raise the Property Changed Event to notify the MainPage
                var ar = new PropertyChangedEventArgs(nameof(UserCreated));
                PropertyChanged?.Invoke(this, ar);

                //clear the textboxes
                Username = string.Empty;
                Password = string.Empty;
                
            });

            CheckUserCommand = new Command(async () =>
                                      {
                                          var _user = new Account
                                                      {
                                                          Username = Username_,
                                                          Password = Password_
                                                      };

                                          //call the database to find any users
                                          var found = await App.Database.IsAccountValid(_user);

                                          if (found == false)
                                          {
                                              //user doesn't exist
                                              _userExists = false;
                                          }
                                          else
                                          {
                                              _userExists = true;
                                          }

                                          //Raise the Property Changed Event to notify the MainPage
                                          var ar = new PropertyChangedEventArgs(nameof(UserExists));
                                          PropertyChanged?.Invoke(this, ar);

                                          //clear the textboxes
                                          Username = string.Empty;
                                          Password = string.Empty;

                                      });
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
