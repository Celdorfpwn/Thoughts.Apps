using Microsoft.AspNet.SignalR.Client;
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
using System.Windows.Threading;
using Thoughts.Api.Models.Entities;
using WpfClient.BL;
using WpfClient.Views;

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {

        private MainView _mainView { get; set; }

        private ChatView _chatView { get; set; } 

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = this;
        }



        public void SetMainViewWindow()
        {
            MainContent.Content = _mainView;
        }

        public void SetChatView()
        {
            MainContent.Content = _chatView;
        }

        public async Task SendChatRequest(User user)
        {
            await _chatView.SendChatRequest(user);
        }

        private async void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await HubService.Singleton.Connect();
            ProgressRing.IsActive = false;
            _mainView = new MainView(this);
            _chatView = new ChatView(this);
            MainContent.Content = new Login(this);
        }

        private void MetroWindow_Closed(object sender, EventArgs e)
        {
            HubService.Singleton.Disconnect();
        }

        
    }
}
