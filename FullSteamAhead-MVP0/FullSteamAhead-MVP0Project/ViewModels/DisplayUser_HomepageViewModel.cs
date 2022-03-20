using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using FullSteamAheadMVP0Project.Models;
using FullSteamAheadMVP0Project;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Collections.Generic;

public class DisplayUser_HomePageViewModel : INotifyPropertyChanged
{

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    private UserEx CopyUser(User user)
    {
        var u = new UserEx();
        u.Email = user.Email;
        u.Information = user.Information;
        u.Nickname = user.Nickname;
        u.Password = user.Password;
        u.Team_Requests = user.Team_Requests;
        u.Username = user.Username;
       
        Task.Run(new System.Action(async () =>
        {
            u.ImageFilePath = await App.Database.GetUserFile(u.Username);
        }));

        return u;
    }

    public ObservableCollection<UserEx> list { get; }

    public ICommand SearchUserCommand => new Command<string>(async (string query) =>
    {
       var normalizedQuery = query?.ToLower() ?? "";

        var users = await App.Database.AccountSearch(normalizedQuery, Global.TeamSignedIn);
        if (users != null)
        {
            list.Clear();
            foreach (var user in users)
            {
                list.Add(CopyUser(user));
            }
            UserListView = list;
        }
    });

    private ObservableCollection<UserEx> _UserListView;
    public ObservableCollection<UserEx> UserListView
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
    private bool MentorsChecked = false;
    public bool MentorsCheckbox
    {
        get { return MentorsChecked; }
        set
        {
            MentorsChecked = value;
            FilterTeams();
        }
    }

    private bool StudentsChecked = false;
    public bool StudentsCheckbox
    {
        get { return StudentsChecked; }
        set
        {
            StudentsChecked = value;
            FilterTeams();
        }
    }


    public void FilterTeams()
    {
        Task.Run(new System.Action(async () =>
        {
            var users = await App.Database.GetAccountsAsync();
            var userlist = new List<User>();

            list.Clear();
            if (AgeChecked == true)
            {
                users = App.Database.FilterAccountAge(users, Global.TeamSignedIn.Team_Information.Min_Age, Global.TeamSignedIn.Team_Information.Max_Age);
            }

            if (GenderChecked == true)
            {
                users = App.Database.FilterAccountGender(users, Global.TeamSignedIn.Team_Information.Gender);
            }

            if (StateChecked == true)
            {
                users = App.Database.FilterAccountState(users, Global.TeamSignedIn.Team_Information.State);
            }

            if (MentorsChecked == true)
            {
                userlist = App.Database.FilterAccountRole(users, "Mentor");
                if (StudentsChecked == false)
                {
                    users = userlist;
                }
            }

            if (StudentsChecked == true)
            {
                userlist = App.Database.FilterAccountRole(users, "Student");
                if (MentorsChecked == false)
                {
                    users = userlist;
                }
            }

            if ((StudentsChecked == true && MentorsChecked == true) || (AgeChecked == false && GenderChecked == false && StateChecked == false &&
            StudentsChecked == false && MentorsChecked == false))
            {
                users = await App.Database.GetAccountsAsync();
                users = App.Database.FilterBestAccountResults(users, Global.TeamSignedIn);
            }

            foreach (var user in users)
            {
                list.Add(CopyUser(user));
            }

            if (list != null)
            {
                UserListView = list;
            }

        }));
    }


    public DisplayUser_HomePageViewModel()
    {   
        list = new ObservableCollection<UserEx>();
        
        Task.Run(new System.Action(async () =>
        {
            var users = await App.Database.GetAccountsAsync();

            list.Clear();
            var userBest = App.Database.FilterBestAccountResults(users, Global.TeamSignedIn);

            foreach (var user in userBest)
            {
                list.Add(CopyUser(user));
            }
            UserListView = list;
        }));
    }

}

