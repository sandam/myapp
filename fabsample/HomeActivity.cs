using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using ActionBar =  Android.Support.V7.App.ActionBar;
using Android.Text;
using com.refractored.fab;
using Android.Support.V7.Widget;
using RecyclerView = Android.Support.V7.Widget.RecyclerView;
using Android.Support.V4.Widget;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using SupportFragment = Android.Support.V4.App.Fragment;
using System.Collections.Generic;

namespace FabSample
{
	[Activity (Label = "TimeTapp")]			
	public class HomeActivity : ActionBarActivity
	{
		private SupportToolbar mToolbar;
		private ActionBarDrawerToggle mDrawerToggle;
		private DrawerLayout mDrawerLayout;
		private ListView mDrawer;
		private Android.Support.V7.Widget.Toolbar v7Toolbar;
		private ArrayAdapter mLeftAdapter;
		private List<string> mLeftDataSet;

		protected override void OnCreate (Bundle bundle)
		{
			RequestWindowFeature(WindowFeatures.NoTitle);
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Home);

			//toolbar on the home page that's acting like an action bar, theme that is set up is with no actionbar
			v7Toolbar = FindViewById<Android.Support.V7.Widget.Toolbar> (Resource.Id.ToolbarSell);
			SetSupportActionBar (v7Toolbar);
			SupportActionBar.Title = "TimeTapp";

			mDrawerLayout = FindViewById<Android.Support.V4.Widget.DrawerLayout> (Resource.Id.drawer_layout);
			mDrawer = FindViewById<ListView>(Resource.Id.left_drawer);
			mDrawer.Tag = 0;

			mLeftDataSet = new List<string>();
			mLeftDataSet.Add ("Item 1");
			mLeftDataSet.Add ("Item 2");
			mLeftAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, mLeftDataSet);
			mDrawer.Adapter = mLeftAdapter;
			mDrawer.ItemClick+= MenuListView_ItemClick;

			mDrawerToggle = new ActionBarDrawerToggle(
				this, //Host Activity
				mDrawerLayout, //DrawerLayout
				Resource.String.openDrawer, //Opened Message
				Resource.String.closeDrawer //Closed Message
			);

			mDrawerLayout.SetDrawerListener(mDrawerToggle);
			SupportActionBar.SetHomeButtonEnabled(true);
			SupportActionBar.SetDisplayShowTitleEnabled(true);
			mDrawerToggle.SyncState();

			if (bundle != null)
			{
				if (bundle.GetString("DrawerState") == "Opened")
				{
					SupportActionBar.SetTitle(Resource.String.openDrawer);
				}

				else
				{
					SupportActionBar.SetTitle(Resource.String.closeDrawer);
				}
			}

			else
			{

				SupportActionBar.SetTitle(Resource.String.closeDrawer);
			}

		
			//"toolbar" that actually onclick of an image
			ImageView imageOptionTime = (ImageView)FindViewById(Resource.Id.imageViewTime);

			imageOptionTime.Click += (s, arg) => {
				Android.Support.V7.Widget.PopupMenu menu = new Android.Support.V7.Widget.PopupMenu (this, imageOptionTime);
				menu.MenuInflater.Inflate (Resource.Menu.Choose_Country, menu.Menu);
				menu.Show ();
			};

			//textview onclick SELL, setup an onclick color?
			TextView SellTextView = (TextView)FindViewById(Resource.Id.SellTextView);
//			Typeface font = Typeface.CreateFromAsset(Assets, "Roboto-Light.ttf");
//			SellTextView.Typeface = font;

			SellTextView.Click += (sender, e) => {

				var intent = new Intent(this, typeof(SellActivity));
				StartActivity(intent);

			};

			//textview onclick TRADE, setup an onclick color?
			TextView TradeTextView = (TextView)FindViewById(Resource.Id.TradeTextView);
			TradeTextView.Click += (sender, e) => {

				var intent = new Intent(this, typeof(TradeActivity));

				StartActivity(intent);

			};

			//textview onclick BUY, setup an onclick color?
			TextView BuyTextView = (TextView)FindViewById(Resource.Id.BuyTextView);
			BuyTextView.Click += (sender, e) => {

				var intent = new Intent(this, typeof(BuyActivity));

				StartActivity(intent);

			};

			//textview onclick HISTORY, setup an onclick color?
			TextView HistoryTextView = (TextView)FindViewById(Resource.Id.HistoryTextView);
			BuyTextView.Click += (sender, e) => {

				var intent = new Intent(this, typeof(BuyActivity));

				StartActivity(intent);

			};

		}

		void MenuListView_ItemClick (object sender, AdapterView.ItemClickEventArgs e)
		{
			mDrawerLayout.CloseDrawers();
			mDrawerToggle.SyncState();
		}


		//dropdown options for toolbar
		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.main, menu);
			return base.OnCreateOptionsMenu(menu);
		}

		//onclick option from toolbar
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

			switch (item.ItemId)
			{

			case Android.Resource.Id.Home:
				//The hamburger icon was clicked which means the drawer toggle will handle the event

				mDrawerToggle.OnOptionsItemSelected(item);
				return true;

//			case Resource.Id.action_refresh:
//				//Refresh
//				return true;
//
//			case Resource.Id.action_help:
//
//				return true;

			default:
				return base.OnOptionsItemSelected (item);
		}
			
	}

		protected override void OnSaveInstanceState (Bundle outState)
		{
			if (mDrawerLayout.IsDrawerOpen((int)GravityFlags.Left))
			{
				outState.PutString("DrawerState", "Opened");
			}

			else
			{
				outState.PutString("DrawerState", "Closed");
			}

			base.OnSaveInstanceState (outState);
		}

		protected override void OnPostCreate (Bundle savedInstanceState)
		{
			base.OnPostCreate (savedInstanceState);
			mDrawerToggle.SyncState();
		}

		public override void OnConfigurationChanged (Android.Content.Res.Configuration newConfig)
		{
			base.OnConfigurationChanged (newConfig);
			mDrawerToggle.OnConfigurationChanged(newConfig);
		}
	}



	}






