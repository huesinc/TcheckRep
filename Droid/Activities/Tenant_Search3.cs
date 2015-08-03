﻿using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;

namespace RentProof.Droid
{
    [Activity(Label = "Tenant_Search", Theme = "@style/MyTheme")]
    public class Tenant_Search3 : AppCompatActivity

    {
        private Button mButtonDislike;
        private Button mButtonLike;
        private DrawerLayout mDrawerLayout;
        private NavigationBar mDrawerToggle;
        private ArrayAdapter mLeftAdapter;
        private List<string> mLeftDataSet;
        private ListView mLeftDrawer;
        private ArrayAdapter mRightAdapter;
        private List<string> mRightDataSet;
        private ListView mRightDrawer;
        private SupportToolbar mToolbar;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Tenant_Search3);


            mButtonDislike = FindViewById<Button>(Resource.Id.buttonDislike);
            mButtonDislike.Click += (object sender, EventArgs args) =>
            {
                //pull up dialog
                var transaction = FragmentManager.BeginTransaction();
                var featurePopUp = new Feature_PopUp();
                featurePopUp.Show(transaction, "do you like fragment");
                featurePopUp.mFeatureSurveyComplete += mFeaturePopUpButton_Click;
            };

            mButtonLike = FindViewById<Button>(Resource.Id.buttonLike);
            mButtonLike.Click += (object sender, EventArgs args) =>
            {
                //pull up dialog
                var transaction = FragmentManager.BeginTransaction();
                var featurePopUp = new Feature_PopUp();
                featurePopUp.Show(transaction, "do you like fragment");
                featurePopUp.mFeatureSurveyComplete += mFeaturePopUpButton_Click;
            };

            /************TOOLBAR******************************************************/
            mToolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
            mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            mLeftDrawer = FindViewById<ListView>(Resource.Id.left_drawer);
            mRightDrawer = FindViewById<ListView>(Resource.Id.right_drawer);

            //tag left and right drawer for case statment when clicked 
            mLeftDrawer.Tag = 0;
            mRightDrawer.Tag = 1;
            //Set action support toolbar with private class variable
            SetSupportActionBar(mToolbar);


            //***********LEFT DATA SET******************************/
            //Left data set, these are the buttons you see when you click on the drawers 
            mLeftDataSet = new List<string>();
            //my_profile has a string in the string xml file in values directory
            mLeftDataSet.Add(GetString(Resource.String.main_menu));
            //log_out has a string in the string xml file in values directory
            mLeftDataSet.Add(GetString(Resource.String.log_out));
            mLeftAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, mLeftDataSet);
            mLeftDrawer.Adapter = mLeftAdapter;
            //click event for the left drawer 
            mLeftDrawer.ItemClick += mLeftDrawer_ItemClick;

            //***********RIGHT DATA SET******************************/
            mRightDataSet = new List<string>();
            //drawer_faq has a string in the string xml file in values directory


            //support has a string in the string xml file in values directory
            mRightDataSet.Add(GetString(Resource.String.help_popup));
            //rentproof has a string in the string xml file in values directory
            mRightDataSet.Add(GetString(Resource.String.support));
            mRightAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, mRightDataSet);
            mRightDrawer.Adapter = mRightAdapter;
            mRightDrawer.ItemClick += mRightDrawer_ItemClick;

            mDrawerToggle = new NavigationBar(
                this, //Host Activity
                mDrawerLayout, //DrawerLayout
                Resource.String.openDrawer, //Opened Message
                Resource.String.closeDrawer //Closed Message
                );

            mDrawerLayout.SetDrawerListener(mDrawerToggle);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
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
                //This is the first the time the activity is ran
                SupportActionBar.SetTitle(Resource.String.closeDrawer);
            }
        }

        private void mLeftDrawer_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            switch (e.Position)
            {
                case 0:
                    var mDrawerButtonMyProfile = new Intent(this, typeof (Main_Menu_Activity));
                    StartActivity(mDrawerButtonMyProfile);
                    break;

                case 1:
                    var mLogout = new Intent(this, typeof (MainActivity));
                    StartActivity(mLogout);
                    break;
            }
        }

        private void mRightDrawer_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            switch (e.Position)
            {
                case 0:
                    var mDrawerButtonFAQ = new Intent(this, typeof (Main_Menu_Activity));
                    StartActivity(mDrawerButtonFAQ);
                    break;

                case 1:
                    var mDrawerButtonSupport = new Intent(this, typeof (Main_Menu_Activity));
                    StartActivity(mDrawerButtonSupport);
                    break;
            }
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    //The hamburger icon was clicked which means the drawer toggle will handle the event
                    //all we need to do is ensure the right drawer is closed so the don't overlap
                    mDrawerLayout.CloseDrawer(mRightDrawer);
                    mDrawerToggle.OnOptionsItemSelected(item);


                    return true;

                case Resource.Id.toolbar:
                    //Refresh
                    return true;

                case Resource.Id.action_help:
                    if (mDrawerLayout.IsDrawerOpen(mRightDrawer))
                    {
                        //Right Drawer is already open, close it
                        mDrawerLayout.CloseDrawer(mRightDrawer);
                    }
                    else
                    {
                        //Right Drawer is closed, open it and just in case close left drawer
                        mDrawerLayout.OpenDrawer(mRightDrawer);
                        mDrawerLayout.CloseDrawer(mLeftDrawer);
                    }
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.main_action_bar, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            if (mDrawerLayout.IsDrawerOpen((int) GravityFlags.Left))
            {
                outState.PutString("DrawerState", "Opened");
            }

            else
            {
                outState.PutString("DrawerState", "Closed");
            }

            base.OnSaveInstanceState(outState);
        }

        protected override void OnPostCreate(Bundle savedInstanceState)
        {
            base.OnPostCreate(savedInstanceState);
            mDrawerToggle.SyncState();
        }

        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
            mDrawerToggle.OnConfigurationChanged(newConfig);
        }

        private void mFeaturePopUpButton_Click(object sender, OnIncludeFeaturePopUp e)
        {
            var intent = new Intent(this, typeof (Tenant_Search));
            StartActivity(intent);
            Finish();
        }
    }
}