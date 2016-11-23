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
    /// Interaction logic for ReceiveChatRequestView.xaml
    /// </summary>
    public partial class ReceiveChatRequestView : UserControl
    {
        private ChatView _chatView { get; set; }

        private IHubProxy _chatProxy { get; set; }


        public ReceiveChatRequestView(ChatView chatView)
        {
            _chatView = chatView;
            _chatProxy = HubService.Singleton.ChatHub;
            InitializeComponent();
        }

        public void SetRequesterUser(User user)
        {
            _textLabel.Content = user.Username + " is inviting you to chat";
        }

    }
}
