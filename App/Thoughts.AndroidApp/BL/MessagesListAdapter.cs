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

namespace Thoughts.AndroidApp.BL
{
    public class MessagesListAdapter : BaseAdapter
    {
        private Context _context { get; set; }

        private List<UserMessageViewModel> _userMessages { get; set; }

        public MessagesListAdapter(Context context, List<UserMessageViewModel> userMessages)
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
            var viewModel = _userMessages[position];

            var inflater = _context.GetSystemService(Activity.LayoutInflaterService) as LayoutInflater;

            if (viewModel.IsLocal)
            {
                convertView = inflater.Inflate(Resource.Layout.Message_Local, null);
            }
            else
            {
                convertView = inflater.Inflate(Resource.Layout.Message_NotLocal, null);
            }

            var senderTextView = convertView.FindViewById<TextView>(Resource.Id.SenderTextView);
            var messageTextView = convertView.FindViewById<TextView>(Resource.Id.MessageTextView);

            senderTextView.Text = viewModel.UserMessage.Sender;
            messageTextView.Text = viewModel.UserMessage.Message;

            return convertView;
        }
    }
}