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

    public event PropertyChangedEventHandler PropertyChanged;

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

        var teams = await App.Database.TeamSearch(normalizedQuery);
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

    public HomePageViewModel(INavigation Navigation)
    {
        list = new ObservableCollection<Team>();
        myList = new ObservableCollection<Team>();
        _navigation = Navigation;


        if (Global.UserSignedIn.Information.Role == "Mentor")
        {
            Task.Run(new System.Action(async ()  =>
            {
                var teams = await App.Database.GetTeamsAsync();

                list.Clear();
                var teamPrivacies = App.Database.FilterTeamPrivacy(teams);
                var teamCities = App.Database.FilterTeamCity(teamPrivacies, Global.UserSignedIn.Information.City, Global.UserSignedIn.Information.State);
                foreach (var team in teamCities)
                {
                    list.Add(team);
                }
                TeamListView = list;

            }));
            
        } 
        
        else
        {
            Task.Run(new System.Action(async () =>
            {
                var teams = await App.Database.GetTeamsAsync();
               
                list.Clear();
                var teamBest = App.Database.FilterBestTeamResults(teams, Global.UserSignedIn);
                foreach (var team in teamBest)
                {
                    list.Add(team);
                }
                TeamListView = list;

            }));
        }
        
        MyTeamsCommand = new Command(async () =>
        {
            var myTeams = await App.Database.GetTeamsAsync();

            myList.Clear();
            foreach (var team in myTeams)
            {
                if (team.Members.ContainsKey(Global.UserSignedIn.Username))
                {
                    myList.Add(team);
                }
            }

            Global.MyTeams = myList;
            await _navigation.PushAsync(new MyTeamsPage());

        });

    }

}

