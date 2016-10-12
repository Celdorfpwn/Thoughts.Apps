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
using System.Threading.Tasks;
using Android.Util;

namespace Thoughts.Android.BL
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

        public Task Connect()
        {
            return _hubConnection.Start();
        }

        public void SetActions(Action<UserMessage> receiveCallback)
        {
            _receiveCallback = receiveCallback;
        }


        public Task SendMessage(UserMessage message)
        {
            return _chatProxy.Invoke("Send", message.Sender, message.Message);
        }
    }
}