﻿using System.Windows;
using Autofac;
using FriendOrganizer.Data.ViewModel;
using FriendOrganizer.Startup;

namespace FriendOrganizer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var bootstrapper = new Bootstrapper();
            var container = bootstrapper.Bootstrap();
            var mainViewModel = container.Resolve<MainWindow>();
            mainViewModel.Show();
        }
    }
}
