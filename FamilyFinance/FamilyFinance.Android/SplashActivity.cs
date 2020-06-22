using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace FamilyFinance.Droid
{
    [Activity(
        Theme = "@style/Theme.Splash",
        Icon = "@mipmap/icon",
        MainLauncher = true,
        ScreenOrientation = ScreenOrientation.Portrait,
        ResizeableActivity = false,
        NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        protected override void OnResume()
        {
            base.OnResume();
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}