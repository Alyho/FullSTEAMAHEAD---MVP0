﻿using FullSteamAheadMVP0Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FullSteamAheadMVP0Project.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class MyTeamsPage : ContentPage
{
    public MyTeamsPage(Team myteams)
    {
        InitializeComponent();
        /*BindingContext = new MyTeamsPageViewModel(myteams);*/
    }
}
}