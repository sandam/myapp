
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
using Android.Graphics.Drawables;
using Android.Graphics;
using Android.Support.V7.App;
using Toolbar = Android.Support.V7.Widget;
using Android.Support.V7;

namespace TimeTappDroid
{
	[Activity (Label = "Buy", Theme="@style/MyTheme")]
	public class BuyActivity : ActionBarActivity
	{
		private Android.Support.V7.Widget.Toolbar v7Toolbar;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Buy);

			v7Toolbar = FindViewById<Android.Support.V7.Widget.Toolbar> (Resource.Id.ToolbarBuy);

			//Toolbar will now take on default actionbar characteristics
			SetSupportActionBar (v7Toolbar);

			SupportActionBar.Title = "Buy";

			FragmentTransaction transaction = FragmentManager.BeginTransaction();
			SlidingTabsFragment fragment = new SlidingTabsFragment();
			transaction.Replace(Resource.Id.sample_content_fragment, fragment);
			transaction.Commit();
		}



		/// <Docs>The options menu in which you place your items.</Docs>
		/// <returns>To be added.</returns>
		/// <summary>
		/// This is the menu for the Toolbar/Action Bar to use
		/// </summary>
		/// <param name="menu">Menu.</param>
		public override bool OnCreateOptionsMenu (IMenu menu)
		{
			MenuInflater.Inflate (Resource.Menu.action_menu, menu);
			return base.OnCreateOptionsMenu (menu);
		}
		public override bool OnOptionsItemSelected (IMenuItem item)
		{	
			Toast.MakeText(this, "Top ActionBar pressed: " + item.TitleFormatted, ToastLength.Short).Show();
			return base.OnOptionsItemSelected (item);
		}
	}
}






