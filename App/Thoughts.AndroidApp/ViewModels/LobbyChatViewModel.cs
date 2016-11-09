using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Thoughts.AndroidApp.Activities;
using Android.Util;
using System.Threading.Tasks;
using Thoughts.AndroidApp.BL;
using Thoughts.AndroidApp.ViewModels.Adapters;

namespace Thoughts.AndroidApp.ViewModels
{
    public class LobbyChatViewModel
    {
        private ChatService _chatService { get; set; }

        private ListView _messagesListView { get; set; }

        private MessagesListAdapter _adapter { get; set; }

        private LobbyChatActivity activity { get; set; }

        private TextView _messageEditText { get; set; }

        private Button _sendButton { get; set; }

        private string _username
        {
            get
            {
                return AppSettings.Username;
            }
        }

        public LobbyChatViewModel(LobbyChatActivity activity,ChatService chatService)
        {
            _messagesListView = activity.FindViewById<ListView>(Resource.Id.MessagesListView);
            _messageEditText = activity.FindViewById<EditText>(Resource.Id.MesssageEditText);
            _sendButton = activity.FindViewById<Button>(Resource.Id.SendButton);
            _adapter = new MessagesListAdapter(activity);
            _messagesListView.Adapter = _adapter;

            _sendButton.Click += SendMessage;

            _chatService = chatService;
            _chatService.ReceiveCallback = AddMessage;
        }

        private void SendMessage(object sender, EventArgs e)
        {
            var message = new UserMessage
            {

                Sender = _username,
                Message = _messageEditText.Text
            };

            _messageEditText.Text = "";

            AddLocalMessage(message);

            _chatService.SendMessage(message); 
        }

        public void AddMessage(UserMessage message)
        {
            _messagesListView.Post(() =>
            {
                _adapter.Add(new UserMessageViewModel { UserMessage = message, IsLocal = false });
            });
        }

        public void AddLocalMessage(UserMessage message)
        {
            _messagesListView.Post(() =>
            {
                _adapter.Add(new UserMessageViewModel { UserMessage = message, IsLocal = true });
            });
        }
    }
}
