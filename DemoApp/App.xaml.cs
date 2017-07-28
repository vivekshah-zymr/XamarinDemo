using Xamarin.Forms;
using DemoApp.ServiceManagers;
using DemoApp.DBHelper;
using DemoApp.Views;
using System;
using Xamarin.Forms.Xaml;
using System.Diagnostics;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace DemoApp
{
    public partial class App : Application
    {
        public static LoginManager loginManager { get; private set; }
        static PersonDBHelper database;

        public App()
        {
            InitializeComponent();
            loginManager = new LoginManager(new RestService());
            MainPage = new NavigationPage();
            MainPage.Navigation.PushAsync(new DemoApp.Views.StartPage());
		}

		public static PersonDBHelper Database
		{
			get
			{
				if (database == null)
				{
                    database = new PersonDBHelper();
					//database = new PersonDBHelper(DependencyService.Get<IFileHelper>().GetLocalFilePath("DemoSQLite.db3"));
				}
				return database;
			}
		}

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
