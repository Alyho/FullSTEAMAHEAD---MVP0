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

