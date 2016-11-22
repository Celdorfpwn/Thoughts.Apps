using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Thoughts.Api.Models.Entities;

namespace WpfClient.Models
{
    public class ContactModel : INotifyPropertyChanged
    {
        public ContactModel()
        {
            _chatButtonVisibility = Visibility.Visible;
            _inviteButtonsVisibility = Visibility.Hidden;
        }

        public User User { get; set; }


        public Visibility InviteButtonsVisibility
        {
            get
            {
                return _inviteButtonsVisibility;
            }
        }

        public Visibility ChatButtonVisibility
        {
            get
            {
                return _chatButtonVisibility;
            }
        }

        private Visibility _inviteButtonsVisibility { get; set; }

        private Visibility _chatButtonVisibility { get; set; }
 
        public event PropertyChangedEventHandler PropertyChanged;


        public void OnInvite()
        {
            _chatButtonVisibility = Visibility.Hidden;
            _inviteButtonsVisibility = Visibility.Visible;
            OnPropertyChanged("InviteButtonsVisibility");
            OnPropertyChanged("ChatButtonVisibility");
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
