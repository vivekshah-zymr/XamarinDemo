using System;
using System.IO;
using Xamarin.Forms;
using DemoApp.Droid;
using DemoApp.DBHelper;

[assembly: Dependency(typeof(SQLite_Android))]

namespace DemoApp.Droid
{
    public class SQLite_Android : IFileHelper
    {
        public SQLite_Android()
        {
        }

		#region ISQLite implementation

		public SQLite.Net.SQLiteConnection GetConnection()
		{
			var fileName = "Demo.db3";
			var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			var path = Path.Combine(documentsPath, fileName);
            System.Diagnostics.Debug.WriteLine("dbPath===={0}", path);
			var platform = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
			var connection = new SQLite.Net.SQLiteConnection(platform, path);

			return connection;
		}

		#endregion
	}
}
