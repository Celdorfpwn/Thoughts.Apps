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
using Thoughts.AndroidApp.BL;
using Thoughts.AndroidApp.Activities;

namespace Thoughts.AndroidApp.ViewModels
{
    public class UserViewModel
    {

        private MenuActivity _activity { get; set; }


        public User User { get; private set; }


        public UserViewModel(MenuActivity activity,User user)
        {
            _activity = activity;
            User = user;
        }
        public void UserLinearLayout_Click(object sender, EventArgs e)
        {
            _activity.StartChatActivity(this);
        }
    }
}