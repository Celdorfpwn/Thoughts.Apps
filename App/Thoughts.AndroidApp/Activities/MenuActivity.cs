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
using Thoughts.AndroidApp.ViewModels.Adapters;
using Thoughts.AndroidApp.ViewModels;
using Android.Util;

namespace Thoughts.AndroidApp.Activities
{
    [Activity(Label = "Thoughts", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.KeyboardHidden | Android.Content.PM.ConfigChanges.ScreenSize)]
    public class MenuActivity : Activity
    {

        private ListView _usersListView { get; set; }

        private ProgressBar _progressBar { get; set; }

        private LinearLayout _menuLinearLayout { get; set; }

        private UsersListAdapter _adapter { get; set; }


        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Menu);
            SetControls();
            SetAdapter();

            ChatService.Singleton.UserConnected = UserConnected;

            await ChatService.Singleton.ConnectAsync();

            ShowMenu();
        }

        internal void StartChatActivity(UserViewModel userViewModel)
        {
            Log.Error("FUCKFOCC", userViewModel.User.Id);
        }

        private void ShowMenu()
        {
            _progressBar.Visibility = ViewStates.Gone;
            _menuLinearLayout.Visibility = ViewStates.Visible;
        }

        private void UserConnected(User user)
        {
            _usersListView.Post(() =>
            {
                _adapter.Add(new UserViewModel(this,user));
            });
        }

        private void SetAdapter()
        {
            _adapter = new UsersListAdapter(this);
            _usersListView.Adapter = _adapter;
        }

        private void SetControls()
        {
            _usersListView = FindViewById<ListView>(Resource.Id.UsersListView);
            _progressBar = FindViewById<ProgressBar>(Resource.Id.ProgressBar);
            _menuLinearLayout = FindViewById<LinearLayout>(Resource.Id.MenuLinearLayout);
        }
    }
}