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

namespace Thoughts.AndroidApp
{
    public static class AppSettings
    {
        public static  string Username { get; set; }

        public const string SERVER_URL = "http://thoughtsapi.azurewebsites.net/chat";
    }
}