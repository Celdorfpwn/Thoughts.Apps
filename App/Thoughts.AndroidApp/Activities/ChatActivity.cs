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
using Thoughts.AndroidApp.BL;
using Android.Util;
using System.Threading;
using System.Threading.Tasks;

namespace Thoughts.AndroidApp.Activities
{
    [Activity(Label = "Thoughts",ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.KeyboardHidden | Android.Content.PM.ConfigChanges.ScreenSize)]
    public class ChatActivity : Activity
    {

        private ChatViewModel _viewModel { get; set; }

        private LinearLayout _chatLinearLayout { get; set; }

        private ProgressBar _progressBar { get; set; }

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Chat);

            _chatLinearLayout = FindViewById<LinearLayout>(Resource.Id.ChatLinearLayout);
            _progressBar = FindViewById<ProgressBar>(Resource.Id.ProgressBar);

            var username = Intent.GetStringExtra("Username");
            var chatService = new ChatService();
            _viewModel = new ChatViewModel(this, chatService,username);


            await chatService.ConnectAsync();
            EnableChatLayout();
        }


        public void EnableChatLayout()
        {
            _progressBar.Visibility = ViewStates.Gone;
            _chatLinearLayout.Visibility = ViewStates.Visible;           
        }
    }

}