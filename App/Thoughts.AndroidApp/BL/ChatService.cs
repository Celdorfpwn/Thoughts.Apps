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

        private HubConnection _hubConnection { get; set; }

        private IHubProxy _chatProxy { get; set; }

        private Action<UserMessage> _receiveCallback { get; set; }

        public ChatService()
        {
            _hubConnection = new HubConnection("http://thoughtsapi.azurewebsites.net/chat");
            _chatProxy = _hubConnection.CreateHubProxy("chat");

            _chatProxy.On<UserMessage>("Receive", (message) =>
            {
                if (_receiveCallback != null)
                {
                    _receiveCallback.Invoke(message);
                }
            });
        }

        public Task ConnectAsync()
        {
            return _hubConnection.Start();
        }

        public void Connect()
        {
            _hubConnection.Start().Wait();
        }

        public void SetActions(Action<UserMessage> receiveCallback)
        {
            _receiveCallback = receiveCallback;
        }


        public Task SendMessage(UserMessage message)
        {
            return _chatProxy.Invoke("Send", message);
        }
    }
}