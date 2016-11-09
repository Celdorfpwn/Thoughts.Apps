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

namespace Thoughts.AndroidApp.ViewModels.Adapters
{
    public class MessagesListAdapter : CollectionAdapter<UserMessageViewModel>
    {

        public MessagesListAdapter(Context context)
            :base(context)
        {
        }


        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var viewModel = _viewModels[position];

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