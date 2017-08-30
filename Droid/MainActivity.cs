using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Xamarin.Forms;
using DemoApp.Views;

namespace DemoApp.Droid
{
    [Activity(Label = "DemoApp", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            UserDialogs.Init(this);

            base.OnCreate(bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);

			//Calls from the view that should rotate
            MessagingCenter.Subscribe<MusicDetailsPage>(this, "allowLandScapePortrait", sender =>
			{
				RequestedOrientation = ScreenOrientation.Unspecified;
			});

			//When the page is closed this is called
			MessagingCenter.Subscribe<MusicDetailsPage>(this, "preventLandScape", sender =>
			{
				RequestedOrientation = ScreenOrientation.Portrait;
			});
            ImageCircle.Forms.Plugin.Droid.ImageCircleRenderer.Init();
            LoadApplication(new App());
        }

		// Field, property, and method for Picture Picker
		public static readonly int PickImageId = 1000;

		public TaskCompletionSource<Stream> PickImageTaskCompletionSource { set; get; }

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent intent)
		{
			base.OnActivityResult(requestCode, resultCode, intent);

			if (requestCode == PickImageId)
			{
				if ((resultCode == Result.Ok) && (intent != null))
				{
					Android.Net.Uri uri = intent.Data;
					Stream stream = ContentResolver.OpenInputStream(uri);

					// Set the Stream as the completion of the Task
					PickImageTaskCompletionSource.SetResult(stream);
				}
				else
				{
					PickImageTaskCompletionSource.SetResult(null);
				}
			}
		}
    }
}
