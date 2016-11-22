using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Thoughts.Api.Models.Entities;
using WpfClient.BL;
using WpfClient.Models;

namespace WpfClient.ViewModels
{
    public class ContactsViewModel
    {
        public ObservableCollection<ContactModel> Contacts { get; private set; }

        private IHubProxy _hubProxy { get; set; }

        private Dispatcher _dispatcher
        {
            get
            {
                return Application.Current.Dispatcher;
            }
        }

        public ContactsViewModel()
        {
            Contacts = new ObservableCollection<ContactModel>();
            _hubProxy = HubService.Singleton.ChatHub;

            _hubProxy.On("ReceiveUsers", (IEnumerable<User> users) =>
             {
                 _dispatcher.Invoke(() =>
                 {
                     foreach (var user in users)
                     {
                         Contacts.Add(new ContactModel
                         {
                             User = user
                         });
                     }
                 });
             });

            _hubProxy.On("NewContact", (User user) =>
            {
                _dispatcher.Invoke(() =>
                {
                    Contacts.Add(new ContactModel
                    {
                        User = user
                    });
                });
            });

            _hubProxy.On("RemoveContact", (User user) =>
             {
                 _dispatcher.Invoke(() =>
                 {
                     var removeContact = GetContactModel(user.ConnectionId);
                     if(removeContact!=null)
                     {
                         Contacts.Remove(removeContact);
                     }
                 });
             });

            _hubProxy.On("ReceiveChatRequest", (User user) =>
             {
                 _dispatcher.Invoke(() =>
                 {
                     var contact = GetContactModel(user.ConnectionId);
                     if(contact != null)
                     {
                         contact.OnInvite();
                     }
                 });
             });
        }

        private ContactModel GetContactModel(string connectionId)
        {
            return Contacts.FirstOrDefault(contact => contact.User.ConnectionId == connectionId);
        }

        public async Task SendChatRequest(ContactModel contactModel)
        {
            await _hubProxy.Invoke("SendChatRequest", contactModel.User);
        }

        public async Task RequestContacts()
        {
            await _hubProxy.Invoke("RequestUsers");
        }
    }
}
