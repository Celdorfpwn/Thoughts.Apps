using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfClient.Models;

namespace WpfClient
{
    public class UserMessageViewModel : INotifyPropertyChanged
    {
        public string Sender { get; set; }

        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                _message = value;
                OnPropertyChanged("Message");
            }
        }

        private void Clear()
        {
            _message = String.Empty;
            OnPropertyChanged("Message");
        }

        public UserMessageViewModel Clone
        {
            get
            {
                var result = new UserMessageViewModel
                {
                    IsLocal = true,
                    Message = this.Message
                };
                this.Clear();
                return result;
            }
        }

        public UserMessage UserMessage
        {
            get
            {
                return new UserMessage
                {
                    Sender = Sender,
                    Message = Message
                };
            }
            set
            {
                Sender = value.Sender;
                Message = value.Message;
            }
        }

        public bool IsLocal { get; set; }

        public Visibility Visibility
        {
            get
            {
                if (IsLocal)
                {
                    return Visibility.Collapsed;
                }
                else
                {
                    return Visibility.Visible;
                }
            }
        }

        private string _message { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
