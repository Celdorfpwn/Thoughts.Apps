using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfClient.BL
{
    public class HubService
    {
        private HubConnection _hubConnection { get; set; }

        public IHubProxy ChatHub { get; private set; }

        public string Username { get; private set; }

        public bool IsConnected
        {
            get
            {
                return !String.IsNullOrEmpty(_hubConnection.ConnectionId);
            }
        }
        
        private HubService()
        {
            _hubConnection = new HubConnection(AppSettings.SERVER_URL);
            ChatHub = _hubConnection.CreateHubProxy("chat");
        }

        public Task Connect()
        {
            return _hubConnection.Start();
        }

        public void Disconnect()
        {
            _hubConnection.Stop();
        }


        public static HubService Singleton
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new HubService();
                }

                return _instance;
            }
        }

        private static HubService _instance { get; set; }


        
    }
}
