
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

namespace FabSample
{
	[Activity (MainLauncher = true)]
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