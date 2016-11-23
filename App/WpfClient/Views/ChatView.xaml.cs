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
using WpfClient.ViewModels;
using WpfClient.Views.ChatViews;

namespace WpfClient.Views
{
    /// <summary>
    /// Interaction logic for ChatView.xaml
    /// </summary>
    public partial class ChatView : UserControl
    {

        private MainWindow _mainWindow { get; set; }

        private IHubProxy _chatProxy { get; set; }

        private MessagesView _messagesView { get; set; }

        private ReceiveChatRequestView _receiveChatRequestView { get; set; }

        private SendChatRequestView _sendChatRequestView { get; set; }

        private Dispatcher _dispatcher
        {
            get
            {
                return Application.Current.Dispatcher;
            }
        }

        public ChatView(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            _chatProxy = HubService.Singleton.ChatHub;
            _messagesView = new MessagesView(this);
            _receiveChatRequestView = new ReceiveChatRequestView(this);
            _sendChatRequestView = new SendChatRequestView(this);
            InitializeComponent();
            InitializeChatProxy();
        }

        private void InitializeChatProxy()
        {
            _chatProxy.On("ReceiveChatRequest", (User user) =>
            {
                _dispatcher.Invoke(() =>
                {
                    _receiveChatRequestView.SetRequesterUser(user);
                    Content = _receiveChatRequestView;
                    _mainWindow.SetChatView();
                });
            });
        }

        public async Task SendChatRequest(User user)
        {
            _mainWindow.SetChatView();
            Content = _sendChatRequestView;
            await _sendChatRequestView.SendChatRequest(user);
        }
    }
}
