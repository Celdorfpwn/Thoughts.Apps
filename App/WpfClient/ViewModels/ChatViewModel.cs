using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Thoughts.Api.Models.Entities;
using WpfClient.BL;
using WpfClient.Models;

namespace WpfClient.ViewModels
{
    public class ChatViewModel : INotifyPropertyChanged
    {

        public UserModel UserModel { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private MainWindow _mainWindow { get; set; }

        private IHubProxy _hubProxy { get; set; }

        private Dispatcher _dispatcher
        {
            get
            {
                return Application.Current.Dispatcher;
            }
        }

        public ChatViewModel(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            _hubProxy = HubService.Singleton.ChatHub;

            _hubProxy.On("ReceiveChatRequest", (User user) =>
             {
                 _dispatcher.Invoke(() =>
                 {
                     UserModel = new UserModel
                     {
                         User = user
                     };
                     _mainWindow.SetChatView();
                 });
             });
        }


        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
