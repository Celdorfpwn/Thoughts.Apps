using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Microsoft.AspNet.SignalR.Client;
using System.Threading;
using Android.Util;
using Java.Lang;

namespace Thoughts.Android
{
    [Activity(Label = "Thoughts.Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private HubConnection _hubConnection { get; set; }

        private IHubProxy _chatProxy { get; set; }

        private List<UserMessage> _userMessages { get; set; }

        private EditText _messageEditText { get; set; }

        private ListView _messagesListView { get; set; }

        private MessagesListAdapter _adapter { get; set; }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            InitChat();
            SetContentView(Resource.Layout.Main);
            _userMessages = new List<UserMessage>();
            _messagesListView = FindViewById<ListView>(Resource.Id.MessagesListView);

            _adapter = new MessagesListAdapter(this, _userMessages);
            _messagesListView.Adapter = _adapter;

            var button = FindViewById<Button>(Resource.Id.SendButton);
            _messageEditText = FindViewById<EditText>(Resource.Id.MesssageEditText);
            button.Click += Button_Click;

        }

        private async void Button_Click(object sender, EventArgs e)
        {
            var message = _messageEditText.Text;
            _messageEditText.Text = "";
            await _chatProxy.Invoke("Send", "AndroidUser", message);
        }

        private void InitChat()
        {
            _hubConnection = new HubConnection("http://thoughtsapi.azurewebsites.net/chat");
            _chatProxy = _hubConnection.CreateHubProxy("chat");

            _chatProxy.On<UserMessage>("Receive", (message) =>
            {
                _messagesListView.Post(() =>
                {
                    _userMessages.Add(message);
                    _adapter.NotifyDataSetChanged();
                });
            });

            _hubConnection.Start().Wait();
        }
    }


    public class MessagesListAdapter : BaseAdapter
    {
        private Context _context { get; set; }

        private List<UserMessage> _userMessages { get; set; }

        public MessagesListAdapter(Context context, List<UserMessage> userMessages)
        {
            _context = context;
            _userMessages = userMessages;
        }

        public override int Count
        {
            get
            {
                return _userMessages.Count;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var message = _userMessages[position];

            var inflater = _context.GetSystemService(Activity.LayoutInflaterService) as LayoutInflater;

            convertView = inflater.Inflate(Resource.Layout.Message, null);

            var senderTextView = convertView.FindViewById<TextView>(Resource.Id.SenderTextView);
            var messageTextView = convertView.FindViewById<TextView>(Resource.Id.MessageTextView);

            senderTextView.Text = message.Sender;
            messageTextView.Text = message.Message;

            return convertView;
        }
    }

    public class UserMessage
    {
        public string Sender { get; set; }

        public string Message { get; set; }
    }
}

