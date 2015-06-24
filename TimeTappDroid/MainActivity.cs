using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;


namespace TimeTappDroid
{
	[Activity (Label = "TimeTappDroid", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{

		protected override void OnCreate (Bundle bundle)
		{
			RequestWindowFeature(WindowFeatures.NoTitle);
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Login);

			Button loginButton = (Button)FindViewById(Resource.Id.login);
			loginButton.Click += (sender, e) => {

				var intent = new Intent(this, typeof(HomeActivity));
				//intent.PutStringArrayListExtra("phone_numbers", _phoneNumbers);
				StartActivity(intent);
				//string url = "http://api.geonames.org/findNearByWeatherJSON?lat=" +
				//latitude.Text +
				//	"&lng=" +
				//	longitude.Text +
				//	"&username=demo";

				// Fetch the weather information asynchronously, parse the results,
				// then update the screen:
				//JsonValue json = await FetchWeatherAsync (url);
				//ParseAndDisplay (json);

			};
						
		}
}
}


