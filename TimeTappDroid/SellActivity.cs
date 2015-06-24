
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
using ActionBarTabsExample;
using Android.Graphics.Drawables;
using Android.Graphics;

namespace TimeTappDroid
{
	[Activity (Label = "SellActivity")]			
	public class SellActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.SellFragment);

			this.ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
			this.ActionBar.SetDisplayShowTitleEnabled (false);
			this.ActionBar.SetDisplayShowHomeEnabled (false);
			this.ActionBar.SetDisplayUseLogoEnabled(false);
			//SetBackgroundDrawable((new ColorDrawable(Color.Rgb(139, 195, 74))));

			AddTab("Time", 0,  new TimeFragment());

			AddTab("Money", 0,  new MoneyFragment());


		}

		void AddTab (string tabText, int iconResourceId, Fragment fragment)
		{
			var tab = this.ActionBar.NewTab ();            
			tab.SetText (tabText);

			if (iconResourceId != 0) {
				tab.SetIcon (iconResourceId);
			}

			// must set event handler for replacing tabs tab
			tab.TabSelected += delegate(object sender, ActionBar.TabEventArgs e) {
				e.FragmentTransaction.Replace(Resource.Id.fragmentContainer, fragment);
			};

			this.ActionBar.AddTab (tab);
		}      

	}
}
	

		

	


