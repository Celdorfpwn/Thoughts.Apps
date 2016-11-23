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
using Thoughts.Api.Models.Entities;
using WpfClient.BL;

namespace WpfClient.Views.ChatViews
{
    /// <summary>
    /// Interaction logic for SendChatRequestView.xaml
    /// </summary>
    public partial class SendChatRequestView : UserControl
    {
        private ChatView _chatView { get; set; }

        private IHubProxy _chatProxy { get; set; }

        public SendChatRequestView(ChatView chatView)
        {
            _chatView = chatView;
            _chatProxy = HubService.Singleton.ChatHub;
            InitializeComponent();
            DataContext = this;
        }

        public async Task SendChatRequest(User user)
        {
            _textLabel.Content = "Waiting for " + user.Username;
            await _chatProxy.Invoke("SendChatRequest", user);
        }
    }
}
