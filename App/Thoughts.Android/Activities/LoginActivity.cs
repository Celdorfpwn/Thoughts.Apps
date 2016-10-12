using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Thoughts.Android.Activities;
using Android.Util;
using Thoughts.Android.BL;
using Android.Views.InputMethods;

namespace Thoughts.Android
{
    [Activity(Label = "Thoughts.Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class LoginActivity : Activity
    {

        private EditText _usernameEditText { get; set; }

        private Button _loginButton { get; set; }

        private LinearLayout _loginLinearLayout { get; set; }

        private ProgressBar _progressBar { get; set; }


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Login);

            _loginButton = FindViewById<Button>(Resource.Id.LoginButton);
            _usernameEditText = FindViewById<EditText>(Resource.Id.NameEditText);
            _loginLinearLayout = FindViewById<LinearLayout>(Resource.Id.LoginLinearLayout);
            _progressBar = FindViewById<ProgressBar>(Resource.Id.ProgressBar);

            _usernameEditText.TextChanged += _usernameEditText_TextChanged;
            _loginButton.Click += _loginButton_Click;
        }

        private void _loginButton_Click(object sender, EventArgs e)
        {
            HideSoftKeyboard();
            StartChatActivity();         
        }


        private void HideSoftKeyboard()
        {
            InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
            imm.HideSoftInputFromWindow(_usernameEditText.WindowToken, 0);
        }


        public void StartChatActivity()
        {
            var intent = new Intent(this,typeof(ChatActivity));

            intent.PutExtra("Username", _usernameEditText.Text);

            StartActivity(intent);
        }

        private void _usernameEditText_TextChanged(object sender, global::Android.Text.TextChangedEventArgs e)
        {
            if(_usernameEditText.Text.Length > 5)
            {
                _loginButton.Enabled = true;
            }
            else
            {
                _loginButton.Enabled = false;
            }
        }


    }

}

