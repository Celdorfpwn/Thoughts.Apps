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
using System.Threading.Tasks;
using Android.Util;
using Microsoft.AspNet.SignalR.Client;

namespace Thoughts.AndroidApp.BL
{
    public class ChatService
    {

        private static ChatService _instance { get; set; }

        public static ChatService Singleton
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ChatService();
                }

                return _instance;
            }
        }

        private HubConnection _hubConnection { get; set; }

        private IHubProxy _chatProxy { get; set; }

        private Action<UserMessage> _receiveCallback { get; set; }

        private Action<User> _userConnected { get; set; }

        public Action<UserMessage> ReceiveCallback
        {
            set
            {
                _receiveCallback = value;
            }
        }

        public Action<User> UserConnected
        {
            set
            {
                _userConnected = value;
            }
        }

        public ChatService()
        {
            _hubConnection = new HubConnection(AppSettings.SERVER_URL);

            _chatProxy = _hubConnection.CreateHubProxy("chat");

            _chatProxy.On<UserMessage>("Receive", (message) =>
            {
                if (_receiveCallback != null)
                {
                    _receiveCallback.Invoke(message);
                }
            });

            _chatProxy.On<User>("UserConnected", (user) =>
            {
                if(_userConnected != null)
                {
                    _userConnected.Invoke(user);
                }
            });
        }

        public async Task ConnectAsync()
        {
            await _hubConnection.Start();
            await _chatProxy.Invoke("Connect",AppSettings.Username);
        }

        public Task SendMessage(UserMessage message)
        {
            return _chatProxy.Invoke("Send", message);
        }
    }
}