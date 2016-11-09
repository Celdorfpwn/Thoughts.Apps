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
using Java.Lang;
using Android.Util;

namespace Thoughts.AndroidApp.ViewModels.Adapters
{
    public class UsersListAdapter : CollectionAdapter<UserViewModel>
    {
        public UsersListAdapter(Context context)
            :base(context)
        {
        }

 
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var viewModel = _viewModels[position];

            var inflater = _context.GetSystemService(Activity.LayoutInflaterService) as LayoutInflater;

            convertView = inflater.Inflate(Resource.Layout.User, null);

            var userLinearLayout = convertView.FindViewById<LinearLayout>(Resource.Id.UserLinearLayout);

            userLinearLayout.Click += viewModel.UserLinearLayout_Click;

            var userNameTextView = convertView.FindViewById<TextView>(Resource.Id.UserNameTextView);

            userNameTextView.Text = viewModel.User.Name;

            return convertView;
        }
    }
}