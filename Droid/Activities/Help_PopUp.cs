﻿using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace RentProof.Droid
{
    public class OnHelpEvent : EventArgs
    {
    }

    [Activity(Label = "Help_PopUp")]
    public class Help_Popup : DialogFragment
    {
        private Button mPopUpButton;
        public event EventHandler<OnHelpEvent> mHelpPopUpEvent;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.Support_PopUp, container, false);

            mPopUpButton = view.FindViewById<Button>(Resource.Id.popUpButton);

            mPopUpButton.Click += mHelpPopUpEvent_Click;

            return view;
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            base.OnActivityCreated(savedInstanceState);
            Dialog.Window.Attributes.WindowAnimations = Resource.Style.popup_animation;
        }

        private void mHelpPopUpEvent_Click(object sender, EventArgs e)
        {
            //user has clicked on signup button
            Dismiss();
        }
    }
}