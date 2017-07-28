
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
using Android.Content.PM;
using System.Threading.Tasks;
using Android.Support.V7.App;
using Android.Util;

namespace DemoApp.Droid
{
    [Activity(MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait, Theme = "@style/MyTheme.Splash")]

    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your application here
        }

		// Launches the startup task
		protected override void OnResume()
		{
			base.OnResume();
			Task startupWork = new Task(() => { SimulateStartup(); });
			startupWork.Start();
		}

		// Prevent the back button from canceling the startup process
		public override void OnBackPressed() { }

		// Simulates background work that happens behind the splash screen
		async void SimulateStartup()
		{
			await Task.Delay(500); // Simulate a bit of startup work.
			StartActivity(new Intent(Application.Context, typeof(MainActivity)));
		}
    }
}
