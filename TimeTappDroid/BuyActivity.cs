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
using Android.Support.V4.View;
using NUnit.Framework;
using com.refractored;
using ActionBar =  Android.Support.V7.App.ActionBar;
using Android.Text;
using com.refractored.fab;
using Android.Support.V7.Widget;
using RecyclerView = Android.Support.V7.Widget.RecyclerView;

namespace TimeTappDroid
{
	[Activity (Label = "Buy", Theme="@style/MyTheme")]
	public class BuyActivity : ActionBarActivity, ActionBar.ITabListener
	{
//		private Android.Support.V7.Widget.Toolbar v7Toolbar;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			InitActionBar();
			// Set our view from the "main" layout resource
//			SetContentView (Resource.Layout.Buy);

//			v7Toolbar = FindViewById<Android.Support.V7.Widget.Toolbar> (Resource.Id.ToolbarBuy);
//
//			//Toolbar will now take on default actionbar characteristics
//			SetSupportActionBar (v7Toolbar);
//
//			SupportActionBar.Title = "Buy";

//			FragmentTransaction transaction = FragmentManager.BeginTransaction();
//			SlidingTabsFragment fragment = new SlidingTabsFragment();
//			transaction.Replace(Resource.Id.sample_content_fragment, fragment);
//			transaction.Commit();

		
		}

		private void InitActionBar()
		{
			if (SupportActionBar == null)
				return;

			var actionBar = SupportActionBar;
			actionBar.NavigationMode = (int)ActionBarNavigationMode.Tabs;

			var tab1 = actionBar.NewTab();
			tab1.SetTabListener(this);
			tab1.SetText("ListView");
			actionBar.AddTab(tab1);

			var tab2 = actionBar.NewTab();
			tab2.SetTabListener(this);
			tab2.SetText("RecyclerView");
			actionBar.AddTab(tab2);

			var tab3 = actionBar.NewTab();
			tab3.SetTabListener(this);
			tab3.SetText("ScrollView");
			actionBar.AddTab(tab3);
		}

		public void OnTabReselected(ActionBar.Tab tab, Android.Support.V4.App.FragmentTransaction ft)
		{
		}

		public void OnTabSelected(ActionBar.Tab tab, Android.Support.V4.App.FragmentTransaction ft)
		{
			switch (tab.Text)
			{
			case "ListView":
				ft.Replace(Android.Resource.Id.Content, new ListViewFragment());
				break;
			case "RecyclerView":
				ft.Replace(Android.Resource.Id.Content, new RecyclerViewFragment());
				break;
			case "ScrollView":
				ft.Replace(Android.Resource.Id.Content, new ScrollViewFragment());
				break;
			}
		}

		public void OnTabUnselected(ActionBar.Tab tab, Android.Support.V4.App.FragmentTransaction ft)
		{

		}

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.action_menu, menu);
			return base.OnCreateOptionsMenu(menu);
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			if(item.ItemId == Resource.Id.about)
			{
				var text = (TextView)LayoutInflater.Inflate(Resource.Layout.about_view, null);
				text.TextFormatted = (Html.FromHtml(GetString(Resource.String.about_body)));
				new Android.Support.V7.App.AlertDialog.Builder(this)
					.SetTitle(Resource.String.about)
					.SetView(text)
					.SetInverseBackgroundForced(true)
					.SetPositiveButton(Android.Resource.String.Ok, (sender, args) =>
						{
							((IDialogInterface)sender).Dismiss();
						}).Create().Show();
			}
			return base.OnOptionsItemSelected(item);
		}
	}

	public class ListViewFragment : Android.Support.V4.App.Fragment, IScrollDirectorListener, AbsListView.IOnScrollListener
	{
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var root = inflater.Inflate(Resource.Layout.fragment_listview, container, false);

			var list = root.FindViewById<ListView>(Android.Resource.Id.List);
			var adapter = new ListViewAdapter(Activity, Resources.GetStringArray(Resource.Array.countries));
			list.Adapter = adapter;

			var fab = root.FindViewById<FloatingActionButton>(Resource.Id.fab);
			fab.AttachToListView(list, this, this);
			fab.Click += (sender, args) =>
			{
				Toast.MakeText(Activity, "FAB Clicked!", ToastLength.Short).Show();
			};
			return root;
		}

		public void OnScrollDown()
		{
			Console.WriteLine("ListViewFragment: OnScrollDown");
		}

		public void OnScrollUp()
		{
			Console.WriteLine("ListViewFragment: OnScrollUp");
		}

		public void OnScroll(AbsListView view, int firstVisibleItem, int visibleItemCount, int totalItemCount)
		{
			Console.WriteLine("ListViewFragment: OnScroll");
		}

		public void OnScrollStateChanged(AbsListView view, ScrollState scrollState)
		{
			Console.WriteLine("ListViewFragment: OnScrollChanged");
		}
	}

	public class RecyclerViewFragment : Android.Support.V4.App.Fragment, Android.Views.ViewTreeObserver.IOnScrollChangedListener
	{
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var root = inflater.Inflate(Resource.Layout.fr, container, false);

			var recyclerView = root.FindViewById<RecyclerView>(Resource.Id.recycler_view);
			recyclerView.HasFixedSize = true;
			recyclerView.SetItemAnimator(new DefaultItemAnimator());
			recyclerView.SetLayoutManager(new LinearLayoutManager(Activity));
			recyclerView.AddItemDecoration(new DividerItemDecoration(Activity, DividerItemDecoration.VerticalList));

			var adapter = new RecyclerViewAdapter(Activity, Resources.GetStringArray(Resource.Array.countries));
			recyclerView.SetAdapter(adapter);

			var fab = root.FindViewById<FloatingActionButton>(Resource.Id.fab);
			fab.AttachToRecyclerView(recyclerView, this);
			fab.Size = FabSize.Mini;
			fab.Click += (sender, args) =>
			{
				Toast.MakeText(Activity, "FAB Clicked!", ToastLength.Short).Show();
			};
			return root;
		}

		public void OnScrollDown()
		{
			Console.WriteLine("RecyclerViewFragment: OnScrollDown");
		}

		public void OnScrollUp()
		{
			Console.WriteLine("RecyclerViewFragment: OnScrollUp");
		}
	}

	public class ScrollViewFragment : Android.Support.V4.App.Fragment, Android.Views.ViewTreeObserver.IOnScrollChangedListener, IScrollDirectorListener
	{

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var root = inflater.Inflate(Resource.Layout.fragment_scrollview, container, false);

			var scrollView = root.FindViewById<ObservableScrollView>(Resource.Id.scroll_view);
			var list = root.FindViewById<LinearLayout>(Resource.Id.list);

			var countries = Resources.GetStringArray(Resource.Array.countries);
			foreach(var country in countries)
			{
				var textView = (TextView)inflater.Inflate(Resource.Layout.list_item, container, false);
				var values = country.Split(',');
				var name = values[0];
				var flagResId = Resources.GetIdentifier(values[1], "drawable", Activity.PackageName);
				textView.Text = name;
				textView.SetCompoundDrawablesWithIntrinsicBounds(flagResId, 0, 0, 0);
				list.AddView(textView);
			}

			var fab = root.FindViewById<FloatingActionButton>(Resource.Id.fab);
			fab.AttachToScrollView(scrollView, this, this);
			return root;
		}
		public void OnScrollDown()
		{
			Console.WriteLine("ScrollViewFragment: OnScrollDown");
		}

		public void OnScrollUp()
		{
			Console.WriteLine("ScrollViewFragment: OnScrollUp");
		}

		public void OnScrollChanged(ScrollView who, int l, int t, int oldl, int oldt)
		{
			Console.WriteLine("ScrollViewFragment: OnScrollChanged");
		}
	}



		/// <Docs>The options menu in which you place your items.</Docs>
		/// <returns>To be added.</returns>
		/// <summary>
		/// This is the menu for the Toolbar/Action Bar to use
		/// </summary>
		/// <param name="menu">Menu.</param>
//		public override bool OnCreateOptionsMenu (IMenu menu)
//		{
//			MenuInflater.Inflate (Resource.Menu.action_menu, menu);
//			return base.OnCreateOptionsMenu (menu);
//		}
//		public override bool OnOptionsItemSelected (IMenuItem item)
//		{	
//			Toast.MakeText(this, "Top ActionBar pressed: " + item.TitleFormatted, ToastLength.Short).Show();
//			return base.OnOptionsItemSelected (item);
//		}
//	}
}
}






