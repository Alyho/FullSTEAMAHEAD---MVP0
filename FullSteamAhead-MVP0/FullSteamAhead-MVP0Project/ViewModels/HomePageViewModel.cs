using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using FullSteamAheadMVP0Project.Models;
using FullSteamAheadMVP0Project;
using System.Windows.Input;
using FullSteamAheadMVP0Project.Views;
using System.Collections.Generic;
using System.Threading.Tasks;

public class HomePageViewModel : ContentPage, INotifyPropertyChanged
{

    public new event PropertyChangedEventHandler PropertyChanged;

    private INavigation _navigation;

    protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public ObservableCollection<Team> list { get; }
    public ObservableCollection<Team> myList { get; }
    public Command MyTeamsCommand { get; }

    public ICommand SearchCommand => new Command<string>(async (string query) =>
    {
        var normalizedQuery = query?.ToLower() ?? "";

        var teams = await App.Database.TeamSearch(normalizedQuery, Global.UserSignedIn);
        if (teams != null)
        {
            list.Clear();
            foreach (var team in teams)
            {
                list.Add(team);
            }
            TeamListView = list;
        }
    });



    private ObservableCollection<Team> _TeamListView;
    public ObservableCollection<Team> TeamListView
    {
        get
        {
            return _TeamListView;
        }
        set
        {
            _TeamListView = value;
            NotifyPropertyChanged();
        }
    }

    private bool AgeChecked = false;
    public bool AgeCheckbox 
    { 
        get { return AgeChecked; }
        set 
        {
            AgeChecked = value;
            FilterTeams();
        }
    }

    private bool GenderChecked = false;
    public bool GenderCheckbox
    {
        get { return GenderChecked; }
        set
        {
            GenderChecked = value;
            FilterTeams();
        }
    }

    private bool StateChecked = false;
    public bool StateCheckbox
    {
        get { return StateChecked; }
        set
        {
            StateChecked = value;
            FilterTeams();
        }
    }


    public void FilterTeams()
    {
        Task.Run(new System.Action(async () =>
        {
            var teams = await App.Database.GetTeamsAsync();
            var teamlist = new List<Team>();

            list.Clear();
            if (AgeChecked == true && GenderChecked == true && StateChecked == true)
            {
                teamlist = App.Database.FilterTeamAge(teams, Global.UserSignedIn.Information.Age);
                teamlist = App.Database.FilterTeamGender(teams, Global.UserSignedIn.Information.Preferences.Gender);
                teamlist = App.Database.FilterTeamState(teams, Global.UserSignedIn.Information.State); 
            }
            else if (AgeChecked == true && GenderChecked == true)
            {
                teamlist = App.Database.FilterTeamAge(teams, Global.UserSignedIn.Information.Age);
                teamlist = App.Database.FilterTeamGender(teams, Global.UserSignedIn.Information.Preferences.Gender);
            }
            else if (AgeChecked == true && StateChecked == true)
            {
                teamlist = App.Database.FilterTeamAge(teams, Global.UserSignedIn.Information.Age);
                teamlist = App.Database.FilterTeamState(teams, Global.UserSignedIn.Information.State);
            }
            else if (AgeChecked == true)
            {
                teamlist = App.Database.FilterTeamAge(teams, Global.UserSignedIn.Information.Age);
            }
            else if (GenderChecked == true && StateChecked == true)
            {
                teamlist = App.Database.FilterTeamGender(teams, Global.UserSignedIn.Information.Preferences.Gender);
                teamlist = App.Database.FilterTeamState(teams, Global.UserSignedIn.Information.State);
            }
            else if (GenderChecked == true)
            {
                teamlist = App.Database.FilterTeamGender(teams, Global.UserSignedIn.Information.Preferences.Gender);
            }
            else if (StateChecked == true)
            {
                teamlist = App.Database.FilterTeamState(teams, Global.UserSignedIn.Information.State);
            }
            else
            {
               teamlist = App.Database.FilterBestTeamResults(teams, Global.UserSignedIn);
            }

            foreach (var team in teamlist)
            {
                list.Add(team);
            }

            if (list != null)
            {
                TeamListView = list;
            }

        }));
    }

    public HomePageViewModel(INavigation Navigation)
    {
        list = new ObservableCollection<Team>();
        myList = new ObservableCollection<Team>();
        _navigation = Navigation;


        Task.Run(new System.Action(async () =>
        {
            var teams = await App.Database.GetTeamsAsync();
               
            list.Clear();
            var teamBest = App.Database.FilterBestTeamResults(teams, Global.UserSignedIn);
            foreach (var team in teamBest)
            {
                list.Add(team);
            }

            if (list != null)
            {
                TeamListView = list;
            }           

        }));
        

    }

}

