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
using TimeTappDroid;


namespace ActionBarTabsExample 
{
	class TimeFragment: Fragment
	{            
		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			base.OnCreateView (inflater, container, savedInstanceState);

			var view = inflater.Inflate (Resource.Layout.Fragment, container, false);
			var sampleTextView = view.FindViewById<TextView> (Resource.Id.textView);	
			sampleTextView.Text = "JebiseglupiActionBar Tab2";
			return view;
		}
	}
}