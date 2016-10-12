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
using Microsoft.AspNet.SignalR.Client;
using Thoughts.Android.Activities;
using Android.Util;
using System.Threading.Tasks;

namespace Thoughts.Android.BL
{
    public class ChatViewModel
    {
        private ChatService _chatService { get; set; }

        private List<UserMessage> _userMessages { get; set; }

        private ListView _messagesListView { get; set; }

        private MessagesListAdapter _adapter { get; set; }

        private ChatActivity activity { get; set; }

        private TextView _messageEditText { get; set; }

        private Button _sendButton { get; set; }

        private string _username { get; set; }

        public ChatViewModel(ChatActivity activity,ChatService chatService, string username)
        {
            _username = username;

            _messagesListView = activity.FindViewById<ListView>(Resource.Id.MessagesListView);
            _messageEditText = activity.FindViewById<EditText>(Resource.Id.MesssageEditText);
            _sendButton = activity.FindViewById<Button>(Resource.Id.SendButton);

            _userMessages = new List<UserMessage>();
            _adapter = new MessagesListAdapter(activity, _userMessages);
            _messagesListView.Adapter = _adapter;

            _sendButton.Click += SendMessage;

            _chatService = chatService;
            _chatService.SetActions(AddMessage);
        }

        private void SendMessage(object sender, EventArgs e)
        {
            var message = new UserMessage
            {

                Sender = _username,
                Message = _messageEditText.Text
            };
            _messageEditText.Text = "";

            _chatService.SendMessage(message);

            AddMessage(message);     
        }

        public void AddMessage(UserMessage message)
        {
            _messagesListView.Post(() =>
            {
                _userMessages.Add(message);
                _adapter.NotifyDataSetChanged();
            });
        }
    }
}
