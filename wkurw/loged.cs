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
using Android.Support.V4.Widget;
using Android.Support.V4.App;


namespace wkurw
{
    [Activity(Label = "loged")]
    public class loged : Activity
    {
        string left1 = "Hello Word", left2 = "Kalkulator";
        DrawerLayout mDrawerLayout;
        List<string> mLeftItems = new List<string>();
        ArrayAdapter mLeftAdapter;
        ListView mLeftDrawer;

        List<string> mRightItems = new List<string>();
        ArrayAdapter mRightAdapter;
        ListView mRightDrawer;

        ActionBarDrawerToggle mDrawerToggle;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.after_log);
            mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.Drawer);
            mRightDrawer = FindViewById<ListView>(Resource.Id.rightList);

            mRightItems.Add("support us");
            mRightItems.Add("exit");

            mLeftDrawer = FindViewById<ListView>(Resource.Id.leftList);
            mLeftItems.Add(left1);
            mLeftItems.Add(left2);

            mLeftDrawer.Tag = 0;
            mRightDrawer.Tag = 1;

            mDrawerToggle = new MyActionBarDrawerToggle(this, mDrawerLayout, Resource.Drawable.ic_menu, Resource.String.open_drawer, Resource.String.close_drawer);

            mLeftAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, mLeftItems);
            mLeftDrawer.Adapter = mLeftAdapter;

            mRightAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, mRightItems);
            mRightDrawer.Adapter = mRightAdapter;

            mDrawerLayout.SetDrawerListener(mDrawerToggle);
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            ActionBar.SetHomeButtonEnabled(true);
            ActionBar.SetDisplayShowTitleEnabled(true);

            mLeftDrawer.ItemClick += mLeftDrawer_ItemClickShort;
            mLeftDrawer.ItemLongClick += mLeftDrawer_ItemClickLong;

            mRightDrawer.ItemClick += mRightDrawer_ItemClickShort;
            mRightDrawer.ItemLongClick += mRightDrawer_ItemClickLong;

        }

        void mRightDrawer_ItemClickLong(object sender, AdapterView.ItemLongClickEventArgs e)
        {

        }

        public void mRightDrawer_ItemClickShort(object sender, AdapterView.ItemClickEventArgs e)
        {
            string iteam = mRightItems[e.Position];
            switch (iteam)
            {
                case "support us":
                    Finish(); break;
                case "exit":
                    Android.OS.Process.KillProcess(Android.OS.Process.MyPid()); break;
            }
        }

        protected void mLeftDrawer_ItemClickShort(object sender, AdapterView.ItemClickEventArgs e)
        {
            string iteam = mLeftItems[e.Position];
            switch (iteam)
            {
                case "Hello Word":
                    Intent intent = new Intent(this, typeof(activityClicer));
                    this.StartActivity(intent); break;
                case "Kalkulator":
                    Intent intent2 = new Intent(this, typeof(activityCalculator));
                    this.StartActivity(intent2);
                    this.Finish(); //kill this layout (before changed) 
                    break;
            }
        }

        void mLeftDrawer_ItemClickLong(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            Console.WriteLine(mLeftItems[e.Position]);
        }
        protected override void OnPostCreate(Bundle savedInstanceState)
        {
            base.OnPostCreate(savedInstanceState);
            mDrawerToggle.SyncState();
        }

        public override void OnConfigurationChanged(Android.Content.Res.Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
            mDrawerToggle.OnConfigurationChanged(newConfig);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.action_bar, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (mDrawerToggle.OnOptionsItemSelected(item))
            {
                if (mDrawerLayout.IsDrawerOpen(mRightDrawer))
                {
                    mDrawerLayout.CloseDrawer(mRightDrawer);
                }
                return true;
            }

            switch (item.ItemId)
            {
                case Resource.Id.support:
                    if (mDrawerLayout.IsDrawerOpen(mRightDrawer))
                    {
                        mDrawerLayout.CloseDrawer(mRightDrawer);
                    }
                    else
                    {
                        mDrawerLayout.CloseDrawer(mLeftDrawer);
                        mDrawerLayout.OpenDrawer(mRightDrawer);
                    }
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }


        }
    }
}