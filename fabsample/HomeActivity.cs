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
using FabSample.Helpers;
using FabSample.Activities;
using Android.Content.Res;
using FabSample.Fragments;

namespace FabSample
{
	[Activity (Label = "TimeTapp")]			
	public class HomeActivity : BaseActivity
	{
		private SupportToolbar mToolbar;

		private MyActionBarDrawerToggle drawerToggle;
		private string drawerTitle;
		private string title;

		private DrawerLayout drawerLayout;
		private ListView drawerListView;
		private static readonly string[] Sections = new[] {
			"Browse", "Friends", "Profile"
		};

		protected override int LayoutResource {
			get {
				return Resource.Layout.Home;
			}
		}

		protected override void OnCreate (Bundle savedInstanceState)
		{
			RequestWindowFeature(WindowFeatures.NoTitle);
			base.OnCreate (savedInstanceState);
//			SetContentView (Resource.Layout.Home);
			this.title = this.drawerTitle = this.Title;

			this.drawerLayout = this.FindViewById<DrawerLayout> (Resource.Id.drawer_layout);
			this.drawerListView = this.FindViewById<ListView> (Resource.Id.left_drawer);
			//toolbar on the home page that's acting like an action bar, theme that is set up is with no actionbar
//			v7Toolbar = FindViewById<Android.Support.V7.Widget.Toolbar> (Resource.Id.ToolbarHome);
//			SetSupportActionBar (v7Toolbar);
//			SupportActionBar.Title = "TimeTapp";
			this.drawerListView.Adapter = new ArrayAdapter<string> (this, Resource.Layout.item_menu, Sections);
			this.drawerListView.ItemClick += (sender, args) => ListItemClicked (args.Position);
			this.drawerLayout.SetDrawerShadow (Resource.Drawable.drawer_shadow_dark, (int)GravityFlags.Start);


			this.drawerToggle = new MyActionBarDrawerToggle (this, this.drawerLayout,
				this.Toolbar,
				Resource.String.drawer_open,
				Resource.String.drawer_close);

			//Display the current fragments title and update the options menu
			this.drawerToggle.DrawerClosed += (o, args) => {
				this.SupportActionBar.Title = this.title;
				this.InvalidateOptionsMenu ();
			};

			//Display the drawer title and update the options menu
			this.drawerToggle.DrawerOpened += (o, args) => {
				this.SupportActionBar.Title = this.drawerTitle;
				this.InvalidateOptionsMenu ();
			};

			//Set the drawer lister to be the toggle.
			this.drawerLayout.SetDrawerListener (this.drawerToggle);

			//if first time you will want to go ahead and click first item.
//			if (savedInstanceState == null) {
//				ListItemClicked (0);
//			}
		
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

		private void ListItemClicked (int position)
		{
			Android.Support.V4.App.Fragment fragment = null;
			switch (position) {				
			case 0:
				fragment = new BrowseFragment ();
				break;
			case 1:
				fragment = new FriendsFragment ();
				break;
			case 2:
				fragment = new ProfileFragment ();
				break;
			}

			if (position == 0 || position == 1 || position == 2) {
				SupportFragmentManager.BeginTransaction ()
				.Replace (Resource.Id.content_frame, fragment)
				.Commit ();
			}

			this.drawerListView.SetItemChecked (position, true);
			SupportActionBar.Title = this.title = Sections [position];
			this.drawerLayout.CloseDrawers();
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

			if (this.drawerToggle.OnOptionsItemSelected (item))
				return true;
				
				return base.OnOptionsItemSelected (item);
		}
			
	

		public override bool OnPrepareOptionsMenu (IMenu menu)
		{

			var drawerOpen = this.drawerLayout.IsDrawerOpen((int)GravityFlags.Left);
			//when open don't show anything
			for (int i = 0; i < menu.Size (); i++)
				menu.GetItem (i).SetVisible (!drawerOpen);


			return base.OnPrepareOptionsMenu (menu);
		}

		protected override void OnPostCreate (Bundle savedInstanceState)
		{
			base.OnPostCreate (savedInstanceState);
			this.drawerToggle.SyncState ();
		}

		public override void OnConfigurationChanged (Configuration newConfig)
		{
			base.OnConfigurationChanged (newConfig);
			this.drawerToggle.OnConfigurationChanged (newConfig);
		}


	
	}



	}






