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

namespace SimpsonApp.Droid
{
    [Activity(Theme = "@style/MyTheme.Splash", //Indicates the theme to use for this activity
              MainLauncher = true, //Set it as boot activity
              NoHistory = true,
              Label = "TheSimpson",
              Icon = "@mipmap/icon",
              ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)] //Doesn't place it in back stack
    public class SplashScreen : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.Window.AddFlags(WindowManagerFlags.Fullscreen);
            this.StartActivity(typeof(MainActivity));

            // Create your application her`e
        }
    }
}