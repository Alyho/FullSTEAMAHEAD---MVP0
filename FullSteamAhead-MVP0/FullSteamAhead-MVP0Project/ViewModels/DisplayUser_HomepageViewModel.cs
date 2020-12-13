using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using FullSteamAheadMVP0Project.Models;
using FullSteamAheadMVP0Project;
using System.Windows.Input;

public class DisplayUser_HomePageViewModel : INotifyPropertyChanged
{

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public ObservableCollection<User> list { get; }

    public ICommand SearchCommand => new Command<string>(async (string query) =>
    {
        var normalizedQuery = query?.ToLower() ?? "";

        var users = await App.Database.AccountSearch(normalizedQuery);
        if (users != null)
        {
            list.Clear();
            foreach (var user in users)
            {
                list.Add(user);
            }
            UserListView = list;
        }
    });

    private ObservableCollection<User> _UserListView;
    public ObservableCollection<User> UserListView
    {
        get
        {
            return _UserListView;
        }
        set
        {
            _UserListView = value;
            NotifyPropertyChanged();
        }
    }

    public DisplayUser_HomePageViewModel()
    {
        list = new ObservableCollection<User>();
    }

}

