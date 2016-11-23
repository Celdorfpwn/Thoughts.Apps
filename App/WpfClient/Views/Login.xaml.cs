using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfClient.BL;
using Microsoft.AspNet.SignalR.Client;
using Thoughts.Api.Models.ApiModels;
using Thoughts.Api.Models.Entities;
using System.Diagnostics;

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {

        private MainWindow MainWindow { get; set; }

        private readonly IHubProxy _hubProxy = HubService.Singleton.ChatHub;

        public Login(MainWindow mainWindow)
        {
            InitializeComponent();
            MainWindow = mainWindow;
            _hubProxy.On("LoginSuccessful", LoginSuccessful);
        }

        private void UsernameTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(UsernameTextbox.Text.Length > 5)
            {
                LoginButton.IsEnabled = true;
            }
            else
            {
                LoginButton.IsEnabled = false;
            }
        }

        private void LoginSuccessful()
        {
            Dispatcher.Invoke(() =>
            {
                ProgressRing.IsActive = false;
                MainWindow.SetMainViewWindow();
            });
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            LoginGrid.Visibility = Visibility.Collapsed;
            ProgressRing.IsActive = true;
            try
            {
                await _hubProxy.Invoke("Login", new User
                {
                    Username = UsernameTextbox.Text
                });
                ProgressRing.IsActive = false;
            }
            catch(Exception)
            {
                ProgressRing.IsActive = false;
                LoginGrid.Visibility = Visibility.Visible;
            }
        }
    }
}
