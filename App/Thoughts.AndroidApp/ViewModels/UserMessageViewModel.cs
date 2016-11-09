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

namespace Thoughts.AndroidApp.ViewModels
{
    public class UserMessageViewModel
    {
        public UserMessage UserMessage { get; set; }

        public bool IsLocal { get; set; }
    }
}