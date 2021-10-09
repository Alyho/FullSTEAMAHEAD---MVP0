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
        private string Email_;
        private string Age_;
        private string Nickname_;
        private string Grade_;

        private string State_;
        private string City_;
        private string Zipcode_;
        private string PhoneNumber_;
        private string Bio_;

        private string Distance_;
        private string Gender_;
        private string Role_;
        private string Privacy_;

        private bool _userCreated;
        private bool _userExists;
        private bool _unfilled;
        private bool _noIntAge;

        public bool UserCreated
        {
            get { return _userCreated; }
        }
        public bool Unfilled
        {
            get { return _unfilled; }
        }
        public bool NoIntAge
        {
            get { return _noIntAge; }
        }


        public bool UserExists
        {
            get { return _userExists; }
        }

        public MainPageViewModel(INavigation Navigation)
        {
            _navigation = Navigation;

            if (Global.UserSignedIn != null)
            {
                Username = Global.UserSignedIn.Username;
                Password = Global.UserSignedIn.Password;
                Nickname = Global.UserSignedIn.Nickname;
                Email = Global.UserSignedIn.Email;
                Age = Global.UserSignedIn.Information.Age;
                Grade = Global.UserSignedIn.Information.Grade;
                City = Global.UserSignedIn.Information.City;
                Zipcode = Global.UserSignedIn.Information.Zip_Code;
                State = Global.UserSignedIn.Information.State;
                PhoneNumber = Global.UserSignedIn.Information.Phone_Number;
                Bio = Global.UserSignedIn.Information.Bio;
                Gender = Global.UserSignedIn.Information.Preferences.Gender;
                Distance = Global.UserSignedIn.Information.Preferences.Distance;
                Privacy = Global.UserSignedIn.Information.Preferences.Privacy;
                Role = Global.UserSignedIn.Information.Role;
            }


            SaveCommand = new Command(async () =>
            {
                if (Global.UserSignedIn != null)
                {
                    if (Username_ != Global.UserSignedIn.Username && Username_ != null)
                    {
                        var _user = new User
                        {
                            Username = Username_,
                            Password = Password_,
                            Nickname = Nickname_,
                            Email = Email_,
                            Information = new Information()
                        };

                        //call the database to find any users
                        var found = await App.Database.GetAccountAsync(_user.Username);

                        if (found != null)
                        {
                            //user already exists
                            _userCreated = false;
                            var ar = new PropertyChangedEventArgs(nameof(UserCreated));
                            PropertyChanged?.Invoke(this, ar);
                        }

                        else
                        {
                            await App.Database.UpdateUsername(Global.UserSignedIn, _user.Username);
                            Global.UserSignedIn.Username = _user.Username;
                            Global.UserSignedIn.Password = _user.Password;
                            Global.UserSignedIn.Nickname = _user.Nickname;
                            Global.UserSignedIn.Email = _user.Email;
                            Global.UserSignedIn.Information.Age = Age_;

                          

                            if (Global.UserSignedIn.Password == null || Global.UserSignedIn.Nickname == "" || Global.UserSignedIn.Email == "" ||
                             Global.UserSignedIn.Information.Age == null)
                            {
                                _unfilled = true;

                                var ar = new PropertyChangedEventArgs(nameof(Unfilled));
                                PropertyChanged?.Invoke(this, ar);
                            }
                            else
                            {
                                int value;
                                if (int.TryParse(Global.UserSignedIn.Information.Age, out value))
                                {
                                    _userCreated = true;
                                    await App.Database.UpdateAccount(Global.UserSignedIn);
                                    var ar = new PropertyChangedEventArgs(nameof(UserCreated));
                                    PropertyChanged?.Invoke(this, ar);
                                } else
                                {
                                    _noIntAge = true;
                                    var ar = new PropertyChangedEventArgs(nameof(NoIntAge));
                                    PropertyChanged?.Invoke(this, ar);
                                }
                            }
                        }
                    }

                    else
                    {

                        Global.UserSignedIn.Password = Password_;
                        Global.UserSignedIn.Information.Age = Age_;
                        Global.UserSignedIn.Nickname = Nickname_;
                        Global.UserSignedIn.Email = Email_;

                        if (Global.UserSignedIn.Password == null || Global.UserSignedIn.Nickname == "" || Global.UserSignedIn.Email == "" ||
                         Global.UserSignedIn.Information.Age == null)
                        {
                            _unfilled = true;

                            var ar = new PropertyChangedEventArgs(nameof(Unfilled));
                            PropertyChanged?.Invoke(this, ar);
                        }
                        else
                        {
                            int value;
                            if (int.TryParse(Global.UserSignedIn.Information.Age, out value))
                            {
                                _userCreated = true;
                                await App.Database.UpdateAccount(Global.UserSignedIn);
                                var ar = new PropertyChangedEventArgs(nameof(UserCreated));
                                PropertyChanged?.Invoke(this, ar);
                            }
                            else
                            {
                                _noIntAge = true;
                                var ar = new PropertyChangedEventArgs(nameof(NoIntAge));
                                PropertyChanged?.Invoke(this, ar);
                            }
                        }
                    }

                } 
                
                else
                {
                    var _user = new User
                    {
                        Username = Username_,
                        Password = Password_,
                        Nickname = Nickname_,
                        Email = Email_,
                        Information = new Information()
                    };

                    //call the database to find any users
                    var found = await App.Database.GetAccountAsync(_user.Username);

                    if (found != null)
                    {
                        //user already exists
                        _userCreated = false;
                        var ar = new PropertyChangedEventArgs(nameof(UserCreated));
                        PropertyChanged?.Invoke(this, ar);
                    }

                    else
                    {
                        Global.UserSignedIn = _user;
                        Global.UserSignedIn.Information.State = State_;
                        Global.UserSignedIn.Information.City = City_;
                        Global.UserSignedIn.Information.Age = Age_;
                        Global.UserSignedIn.Information.Role = Role_;
                        Global.UserSignedIn.Information.Preferences.Gender = Gender_;

                        if (Global.UserSignedIn.Password == null || Global.UserSignedIn.Password == "" || Global.UserSignedIn.Username == null || 
                        Global.UserSignedIn.Username == "" || Global.UserSignedIn.Nickname == "" || Global.UserSignedIn.Email == "" ||
                        Global.UserSignedIn.Information.Role == null || Global.UserSignedIn.Information.State == null ||
                        Global.UserSignedIn.Information.City == "" || Global.UserSignedIn.Information.Age == null || Global.UserSignedIn.Information.Preferences.Gender == null)
                        {
                            _unfilled = true;

                            Global.UserSignedIn = null;

                            var ar = new PropertyChangedEventArgs(nameof(Unfilled));
                            PropertyChanged?.Invoke(this, ar);
                            
                        }
                        else
                        {
                            int value;
                            if (int.TryParse(Global.UserSignedIn.Information.Age, out value))
                            {
                                _userCreated = true;
                                await App.Database.UpdateAccount(Global.UserSignedIn);
                                var ar = new PropertyChangedEventArgs(nameof(UserCreated));
                                PropertyChanged?.Invoke(this, ar);
                            }
                            else
                            {
                                _noIntAge = true;
                                var ar = new PropertyChangedEventArgs(nameof(NoIntAge));
                                PropertyChanged?.Invoke(this, ar);
                            }
                        }

                    }
                }
                
            });

            NextCommand = new Command(async () =>
            {
                Global.UserSignedIn.Information.City = City_;
                Global.UserSignedIn.Information.State = State_;
                Global.UserSignedIn.Information.Zip_Code = Zipcode_;
                Global.UserSignedIn.Information.Phone_Number = PhoneNumber_;
                Global.UserSignedIn.Information.Bio = Bio_;
                Global.UserSignedIn.Information.Grade = Grade_;
                
                if (Global.UserSignedIn.Information.City == "" || Global.UserSignedIn.Information.State == null)
                {
                    _unfilled = true;

                    var ar = new PropertyChangedEventArgs(nameof(Unfilled));
                    PropertyChanged?.Invoke(this, ar);
                }
                else
                {
                    await App.Database.UpdateAccount(Global.UserSignedIn);
                    await _navigation.PushAsync(new ChangeUserInformation3());
                }
               
            });

            SaveUserCommand = new Command(async () =>
           {
               Global.UserSignedIn.Information.Role = Role_;
               Global.UserSignedIn.Information.Preferences.Distance = Distance_;
               Global.UserSignedIn.Information.Preferences.Gender = Gender_;
               Global.UserSignedIn.Information.Preferences.Privacy = Privacy_;

               if (Global.UserSignedIn.Information.Role == null || Global.UserSignedIn.Information.Preferences.Distance == null || Global.UserSignedIn.Information.Preferences.Gender == null ||
               Global.UserSignedIn.Information.Preferences.Privacy == null)
               {
                   _unfilled = true;

                   var ar = new PropertyChangedEventArgs(nameof(Unfilled));
                   PropertyChanged?.Invoke(this, ar);
               }
               else
               {
                   await App.Database.UpdateAccount(Global.UserSignedIn);
                   await _navigation.PushAsync(new Settingspage());
               }

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
                                              Global.UserSignedIn = await App.Database.GetAccountAsync(_user.Username); 
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

        public string Email
        {
            get => Email_;
            set
            {
                if (Email_ != value)
                {
                   Email_ = value;
                    var args = new PropertyChangedEventArgs(nameof(Email));
                    PropertyChanged?.Invoke(this, args);
                }
            }
        }

        public string Nickname
        {
            get => Nickname_;
            set
            {
                if (Nickname_ != value)
                {
                    Nickname_ = value;
                    var args = new PropertyChangedEventArgs(nameof(Nickname));
                    PropertyChanged?.Invoke(this, args);
                }
            }
        }

        public string Age
        {
            get => Age_;
            set
            {
                if (Age_ != value)
                {
                    Age_ = value;
                    var args = new PropertyChangedEventArgs(nameof(Age));
                    PropertyChanged?.Invoke(this, args);
                }
            }
        }

        public string Grade
        {
            get => Grade_;
            set
            {
                if (Grade_ != value)
                {
                    Grade_ = value;
                    var args = new PropertyChangedEventArgs(nameof(Grade));
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

        public string PhoneNumber
        {
            get => PhoneNumber_;
            set
            {
                if (PhoneNumber_ != value)
                {
                    PhoneNumber_ = value;
                    var args = new PropertyChangedEventArgs(nameof(PhoneNumber));
                    PropertyChanged?.Invoke(this, args);
                }
            }
        }

        public string Bio
        {
            get => Bio_;
            set
            {
                if (Bio_ != value)
                {
                    Bio_ = value;
                    var args = new PropertyChangedEventArgs(nameof(Bio));
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
