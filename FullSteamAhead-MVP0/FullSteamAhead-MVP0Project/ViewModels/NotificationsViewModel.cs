﻿using FullSteamAheadMVP0Project.Models;
using FullSteamAheadMVP0Project.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace FullSteamAheadMVP0Project.ViewModels
{
    public class NotificationsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _noEmail;

        public bool NoEmail
        {
            get { return _noEmail; }
        }
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private IList<Container> _NotificationsListView;

        public IList<Container> NotificationsListView
        {
            get
            {
                return _NotificationsListView;
            }
            set
            {
                _NotificationsListView = value;
                NotifyPropertyChanged();
            }
        }



        public NotificationsViewModel(INavigation Navigation)
        {

            Dictionary<string, User> UserRequestsDict = new Dictionary<string, User>();
            List<Container> UserRequestList = new List<Container>();

            Dictionary<string, Team> TeamRequestsDict = new Dictionary<string, Team>();
            List<Container> TeamRequestList = new List<Container>();

            if (Global.TeamSignedIn != null)
            {

                Task.Run(new System.Action(async () =>
                {
                    UserRequestsDict = await App.Database.GetUserRequests(Global.TeamSignedIn);

                    foreach (KeyValuePair<string, User> entry in UserRequestsDict)
                    {
                        UserRequestList.Add(new Container(Navigation)
                        {
                            Username = entry.Value.Username,
                            Name = entry.Value.Nickname,
                            Role = entry.Value.Information.Role
                        }) ; 

                    }

                    NotificationsListView = UserRequestList;
                }));

            }
            
            else
            {
                Task.Run(new System.Action(async () =>
                {
                    TeamRequestsDict = await App.Database.GetTeamRequests(Global.UserSignedIn);

                    foreach (KeyValuePair<string, Team> entry in TeamRequestsDict)
                    {
                        TeamRequestList.Add(new Container(Navigation)
                        {
                            Username = entry.Value.Team_Username,
                            Name = entry.Value.Team_Nickname,
                            Location = entry.Value.Team_Information.City + ", " + entry.Value.Team_Information.State
                        });

                    }

                    NotificationsListView = TeamRequestList;
                }));
            }
        }
    }

    public class Container : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string Username { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Date { get; set; }
        private INavigation _navigation;
        private bool _noEmail;

        public bool NoEmail
        {
            get { return _noEmail; }
        }

        public Command AcceptCommand { get; set; }
        public Command DeclineCommand { get; set; }

        public Container(INavigation Navigation)
        {

            _navigation = Navigation;

            AcceptCommand = new Command(async () =>
            { 
                if (Global.TeamSignedIn != null)
                {
                    var user = await App.Database.GetAccountAsync(Username);
                    await App.Database.AddUser(Global.TeamSignedIn, user);
                    await App.Database.RemoveUserRequest(Global.TeamSignedIn, user.Username);
                    await App.Database.RemoveTeamRequest(user, Global.TeamSignedIn.Team_Username);

                    try
                    {
                        List<string> emails = new List<string>();
                        emails.Add(user.Email);
                        var message = new EmailMessage
                        {
                            Subject = "Accepted to Team!",
                            Body = "Congrats! We have accepted your request to join our STEM team. On your Full STEAM Ahead app, go to My Teams page where you'll have access to our communications.",
                            To = emails
                        };

                        await Email.ComposeAsync(message);
                    }
                    catch (FeatureNotSupportedException fbsEx)
                    {
                        // Email is not supported on this device
                        _noEmail = true;


                    }
                    catch (Exception ex)
                    {
                        // Some other exception occurred
                        _noEmail = true;
                    }

                    var br = new PropertyChangedEventArgs(nameof(NoEmail));
                    PropertyChanged?.Invoke(this, br);

                    await _navigation.PushAsync(new TeamNotifications());

                }

                else
                {
                    var team = await App.Database.GetTeamAsync(Username);
                    await App.Database.AddUser(team, Global.UserSignedIn);
                    await App.Database.RemoveTeamRequest(Global.UserSignedIn, team.Team_Username);
                    await App.Database.RemoveUserRequest(team, Global.UserSignedIn.Username);

                    try
                    {
                        List<string> emails = new List<string>();
                        emails.Add(team.Team_Information.Team_Email);
                        var message = new EmailMessage
                        {
                            Subject = "A New User Has Joined Your Team",
                            Body = "Hello! I have accepted your request to join your STEM team. On your Full STEAM Ahead app, go to your Members page to check if I am added.",
                            To = emails
                        };

                        await Email.ComposeAsync(message);
                    }
                    catch (FeatureNotSupportedException fbsEx)
                    {
                        // Email is not supported on this device
                        _noEmail = true;


                    }
                    catch (Exception ex)
                    {
                        // Some other exception occurred
                        _noEmail = true;
                    }

                    var br = new PropertyChangedEventArgs(nameof(NoEmail));
                    PropertyChanged?.Invoke(this, br);

                    await _navigation.PushAsync(new Notifications());
                }    
                
            });

            DeclineCommand = new Command(async () =>
            {
                if (Global.TeamSignedIn != null)
                {
                    var user = await App.Database.GetAccountAsync(Username);
                    await App.Database.RemoveUserRequest(Global.TeamSignedIn, user.Username);
                    await App.Database.RemoveTeamRequest(user, Global.TeamSignedIn.Team_Username);

                    try
                    {
                        List<string> emails = new List<string>();
                        emails.Add(user.Email);
                        var message = new EmailMessage
                        {
                            Subject = "Your User Request has been Denied",
                            Body = "Thank you for your interest but unfortunately, we cannot accept you onto our team at this time.",
                            To = emails
                        };

                        await Email.ComposeAsync(message);
                    }
                    catch (FeatureNotSupportedException fbsEx)
                    {
                        // Email is not supported on this device
                        _noEmail = true;


                    }
                    catch (Exception ex)
                    {
                        // Some other exception occurred
                        _noEmail = true;
                    }

                    var br = new PropertyChangedEventArgs(nameof(NoEmail));
                    PropertyChanged?.Invoke(this, br);

                    await _navigation.PushAsync(new TeamNotifications());

                }

                else
                {
                    var team = await App.Database.GetTeamAsync(Username);
                    await App.Database.RemoveTeamRequest(Global.UserSignedIn, team.Team_Username);
                    await App.Database.RemoveUserRequest(team, Global.UserSignedIn.Username);

                    try
                    {
                        List<string> emails = new List<string>();
                        emails.Add(team.Team_Information.Team_Email);
                        var message = new EmailMessage
                        {
                            Subject = "Your Team Request has been Denied",
                            Body = "Thank you for your interest, but unfortunately, I cannot accept your invite at this time.",
                            To = emails
                        };

                        await Email.ComposeAsync(message);
                    }
                    catch (FeatureNotSupportedException fbsEx)
                    {
                        // Email is not supported on this device
                        _noEmail = true;


                    }
                    catch (Exception ex)
                    {
                        // Some other exception occurred
                        _noEmail = true;
                    }

                    var br = new PropertyChangedEventArgs(nameof(NoEmail));
                    PropertyChanged?.Invoke(this, br);

                    await _navigation.PushAsync(new Notifications());
                }
            });
        }
        
    }

}
