﻿using FullSteamAheadMVP0Project.Models;
using FullSteamAheadMVP0Project.ViewModels;
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
    public MyTeamsPage()
    {
        InitializeComponent();
        BindingContext = new MyTeamsPageViewModel();
    }

    private async void HomePage_Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Homepage());
    }
}
}