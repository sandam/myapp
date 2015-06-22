
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

namespace TimeTappDroid
{
	[Activity (Label = "HomeActivity")]			
	public class HomeActivity : Activity
	{
		//ImageView imageOptionTime = (ImageView)FindViewById(Resource.Id.imageViewTime);
		//ImageView imageOptionData = (ImageView)FindViewById(Resource.Id.imageViewData);
		//ImageView imageOptionSMS = (ImageView)FindViewById(Resource.Id.imageViewSMS);

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Home);
			ImageView imageOptionTime = (ImageView)FindViewById(Resource.Id.imageViewTime);
			//imageOptionTime.Click += new EventHandler (imageOptionTimeClick);
			//imageOptionData.Click += new EventHandler (imageOptionDataClick);
			//imageOptionSMS.Click += new EventHandler (imageOptionSMSClick);

			imageOptionTime.Click += (s, arg) => {
				PopupMenu menu = new PopupMenu (this, imageOptionTime);
				menu.MenuInflater.Inflate (Resource.Layout.Home_Menu_ChooseCountry, menu.Menu);
				menu.Show ();
			};

			TextView SellTextView = (TextView)FindViewById(Resource.Id.SellTextView);
			SellTextView.Click += (sender, e) => {

				var intent = new Intent(this, typeof(SellActivity));

				StartActivity(intent);

			};
		}
				
		


		}
		}

	



