using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;
using FullSteamAheadMVP0Project.Models;
using System.Dynamic;
using FullSteamAheadMVP0Project;

public class HomePage2ViewModel : INotifyPropertyChanged
{

    public event PropertyChangedEventHandler PropertyChanged;

    private string _TeamSearched;
    private ObservableCollection<Team> _TeamListView;

    public Command SearchCommand { get; set; }

    public ObservableCollection<Team> TeamListView {
        get
        {
            return _TeamListView;
        }
        set
        {
            if (_TeamListView != value)
            {
                _TeamListView = value;
                var args = new PropertyChangedEventArgs(nameof(TeamListView));
                PropertyChanged?.Invoke(this, args);
            }
        }
    }

    public HomePage2ViewModel()
    {
        TeamListView = new ObservableCollection<Team>();

        SearchCommand = new Command(async () =>
        {
            var _team = new Team
            {
                Team_Username = _TeamSearched
            };

            var found = await App.Database.IsTeamValid(_team);

            if (found == true)
            {
                var team = await App.Database.UsernameSearch(_team.Team_Username);
                var list = new ObservableCollection<Team>();
                list.Add(team);
                _TeamListView = list;
            }
            else
            {
                TeamListView.Clear();
            }
        });
    }

    public string SearchText
    {
        get => _TeamSearched;
        set
        {
            if (_TeamSearched != value)
            {
                _TeamSearched = value;
                var args = new PropertyChangedEventArgs(nameof(SearchText));
                PropertyChanged?.Invoke(this, args);
            }
        }
    }


}
