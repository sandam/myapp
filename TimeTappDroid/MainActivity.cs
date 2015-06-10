using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;


namespace TimeTappDroid
{
	[Activity (Label = "TimeTappDroid", MainLauncher = true, Icon = "@drawable/icon", Theme = "@android:style/Theme.Black.NoTitleBar.Fullscreen")]
	public class MainActivity : Activity
	{
		
		private Button loginButton;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			loginButton = (Button)FindViewById(Resource.Id.login);
			loginButton.Click += new EventHandler(login_Click);
			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Login);

			// Get our button from the layout resource,
			// and attach an event to it
			//Button button = FindViewById<Button> (Resource.Id.myButton);
		
			}

		void login_Click(object sender, EventArgs e)
		{
			SetContentView (Resource.Layout.Home);
		}



			
}
}


