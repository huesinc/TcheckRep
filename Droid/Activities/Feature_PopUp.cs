﻿using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace RentProof.Droid
{
    public class OnIncludeFeaturePopUp : EventArgs
    {
    }

    [Activity(Label = "Support_PopUp")]
    public class Feature_PopUp : DialogFragment
    {
        private Button mPopUpButton;
        public event EventHandler<OnIncludeFeaturePopUp> mFeatureSurveyComplete;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.IncludeFeature_PopUp, container, false);

            mPopUpButton = view.FindViewById<Button>(Resource.Id.popUpButton);

            mPopUpButton.Click += mFeatureSurveyPopUp_Click;

            return view;
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            base.OnActivityCreated(savedInstanceState);
            Dialog.Window.Attributes.WindowAnimations = Resource.Style.popup_animation;
        }

        private void mFeatureSurveyPopUp_Click(object sender, EventArgs e)
        {
            //user has clicked on signup button
            mFeatureSurveyComplete.Invoke(this, new OnIncludeFeaturePopUp());
            Dismiss();
        }
    }
}