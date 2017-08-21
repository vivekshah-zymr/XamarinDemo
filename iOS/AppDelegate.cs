using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using UIKit;
using DemoApp.Views;
using Xamarin.Forms;

namespace DemoApp.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());
            return base.FinishedLaunching(app, options);
        }

		public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations(UIApplication application, UIWindow forWindow)
		{
			if (Xamarin.Forms.Application.Current == null || Xamarin.Forms.Application.Current.MainPage == null)
			{
				return UIInterfaceOrientationMask.Portrait;
			}
			var mainPage = Xamarin.Forms.Application.Current.MainPage;
            var homePage = mainPage.Navigation.NavigationStack.LastOrDefault();

            if(homePage is HomePage)
            {
                TabbedPage tb = (TabbedPage)((HomePage)homePage).Detail;
                NavigationPage np = (NavigationPage)tb.CurrentPage;
				if (np.Navigation.NavigationStack.LastOrDefault() is MusicDetailsPage)
				{
					return UIInterfaceOrientationMask.All;
				}
            }
			return UIInterfaceOrientationMask.Portrait;
		}
    }
}