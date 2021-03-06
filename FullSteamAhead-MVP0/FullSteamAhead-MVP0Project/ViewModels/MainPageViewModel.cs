using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;
using FullSteamAheadMVP0Project.Models;
using FullSteamAheadMVP0Project.Views;

namespace FullSteamAheadMVP0Project.ViewModels
{
    public class MainPageViewModel : ContentPage, INotifyPropertyChanged
    {
        private INavigation _navigation;
        public Command SaveCommand { get; }
        public Command CheckUserCommand { get; }

        public Command NextCommand { get; }

        public Command SaveUserCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        private string Username_;
        private string Password_;
        private string State_;
        private string City_;
        private string Zipcode_;
        private string Distance_;
        private string Gender_;
        private string Role_;
        private string Privacy_;

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

        public MainPageViewModel(INavigation Navigation)
        {
            _navigation = Navigation;

            SaveCommand = new Command(async () =>
            {
                var _user = new User
                {
                    Username = Username_,
                    Password = Password_,
                    Information=new Information()
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

                    _userCreated = true;
                    Global.UserSignedIn = _user;
                }

                //Raise the Property Changed Event to notify the MainPage
                var ar = new PropertyChangedEventArgs(nameof(UserCreated));
                PropertyChanged?.Invoke(this, ar);

                //clear the textboxes
                Username = string.Empty;
                Password = string.Empty;
                
            });

            NextCommand = new Command(async () =>
            {
                Global.UserSignedIn.Information.City = City_;
                Global.UserSignedIn.Information.State = State_;
                Global.UserSignedIn.Information.Zip_Code = Zipcode_;

                await _navigation.PushAsync(new CreateStudentAccount3());
            });

            SaveUserCommand = new Command(async () =>
           {
               Global.UserSignedIn.Information.Role = Role_;
               Global.UserSignedIn.Information.Preferences.Distance = Distance_;
               Global.UserSignedIn.Information.Preferences.Gender = Gender_;
               Global.UserSignedIn.Information.Preferences.Privacy = Privacy_;

               await App.Database.SaveAccountAsync(Global.UserSignedIn);

               await _navigation.PushAsync(new Homepage());
           });

            CheckUserCommand = new Command(async () =>
                                      {
                                          var _user = new User
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
                                              Global.UserSignedIn = _user; 
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

        public string City
        {
            get => City_;
            set
            {
                if (City_ != value)
                {
                    City_ = value;
                    var args = new PropertyChangedEventArgs(nameof(City));
                    PropertyChanged?.Invoke(this, args);
                }
            }
        }

        public string State
        {
            get => State_;
            set
            {
                if (State_ != value)
                {
                    State_ = value;
                    var args = new PropertyChangedEventArgs(nameof(State));
                    PropertyChanged?.Invoke(this, args);
                }
            }
        }

        public string Zipcode
        {
            get => Zipcode_;
            set
            {
                if (Zipcode_ != value)
                {
                    Zipcode_ = value;
                    var args = new PropertyChangedEventArgs(nameof(Zipcode));
                    PropertyChanged?.Invoke(this, args);
                }
            }
        }

        public string Distance
        {
            get => Distance_;
            set
            {
                if (Distance_ != value)
                {
                    Distance_ = value;
                    var args = new PropertyChangedEventArgs(nameof(Distance));
                    PropertyChanged?.Invoke(this, args);
                }
            }
        }

        public string Gender
        {
            get => Gender_;
            set
            {
                if (Gender_ != value)
                {
                    Gender_ = value;
                    var args = new PropertyChangedEventArgs(nameof(Gender));
                    PropertyChanged?.Invoke(this, args);
                }
            }
        }

        public string Role
        {
            get => Role_;
            set
            {
                if (Role_ != value)
                {
                    Role_ = value;
                    var args = new PropertyChangedEventArgs(nameof(Role));
                    PropertyChanged?.Invoke(this, args);
                }
            }
        }

        public string Privacy 
        {
            get => Privacy_;
            set
            {
                if (Privacy_ != value)
                {
                    Privacy_ = value;
                    var args = new PropertyChangedEventArgs(nameof(Privacy));
                    PropertyChanged?.Invoke(this, args);
                }
            }
        }
    }
}
