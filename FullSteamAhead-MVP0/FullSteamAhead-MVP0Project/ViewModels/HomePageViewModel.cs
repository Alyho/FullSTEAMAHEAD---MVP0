using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using FullSteamAheadMVP0Project.Models;
using FullSteamAheadMVP0Project;
using System.Windows.Input;
using FullSteamAheadMVP0Project.Views;

public class HomePageViewModel : INotifyPropertyChanged
{

    public event PropertyChangedEventHandler PropertyChanged;
    public INavigation Navigation { get; set; }
    
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

    public HomePageViewModel()
    {
        list = new ObservableCollection<Team>();
        myList = new ObservableCollection<Team>();

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
            await Navigation.PushAsync(new MyTeamsPage());

        });
    }

}

