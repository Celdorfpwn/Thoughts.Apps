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

namespace Thoughts.Android.BL
{
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
}