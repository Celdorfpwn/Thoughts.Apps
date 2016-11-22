using Microsoft.AspNet.SignalR.Client;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using WpfClient.BL;
using WpfClient.Models;

namespace WpfClient.ViewModels
{
    public class UsersViewModel
    {
        public ReactiveList<UserViewModel> UserViewModels { get; set; }
        private IHubProxy _hubProxy { get; set; }

        private Dispatcher _dispatcher
        {
            get
            {
                return Application.Current.Dispatcher;
            }
        }

        public UsersViewModel()
        {
            ConfigureHubProxy();  
            UserViewModels = new ReactiveList<UserViewModel>();
        }

        private void UserConnected(UserLocal user)
        {
            if (!UserViewModels.Any(userViewModel => userViewModel.User.Id.Equals(user.Id)))
            {
                UserViewModels.Add(new UserViewModel(user));
            }
        }

        internal void Disconnect()
        {
            HubService.Singleton.Disconnect();
        }

        private void UsersList(IEnumerable<UserLocal> users)
        {
            users.Where(user => !UserViewModels.Select(userViewModel => userViewModel.User.Id).Contains(user.Id))
                .ToList()
                .ForEach(user => UserViewModels.Add(new UserViewModel(user)));
        }

        private void UserDisconnected(UserLocal user)
        {
            UserViewModels.Remove(UserViewModels.Single(userViewModel => userViewModel.User.Id.Equals(user.Id)));
        }

        private void ConfigureHubProxy()
        {
            _hubProxy = HubService.Singleton.ChatHub;

            _hubProxy.On<UserLocal>("UserConnected", (user) =>
            {
                _dispatcher.InvokeAsync(() =>
                {
                    UserConnected(user);
                });
            });

            _hubProxy.On<IEnumerable<UserLocal>>("UsersList", (users) =>
            {
                _dispatcher.InvokeAsync(() =>
                {
                    UsersList(users);
                });
            });

            _hubProxy.On<UserLocal>("UserDisconected", (user) =>
             {
                 _dispatcher.InvokeAsync(() =>
                 {
                     UserDisconnected(user);
                 });
             });
        }
    }
}
