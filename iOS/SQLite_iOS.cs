using System;
using System.IO;
using SQLite;
using DemoApp.DBHelper;
using Xamarin.Forms;
using DemoApp.iOS;

[assembly: Dependency(typeof(SQLite_iOS))]


namespace DemoApp.iOS
{
    public class SQLite_iOS : IFileHelper
    {
        public SQLite_iOS()
        {
        }

		#region ISQLite implementation

		public SQLite.Net.SQLiteConnection GetConnection()
		{
			var fileName = "Demo.db3";
			var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			var libraryPath = Path.Combine(documentsPath, "..", "Library");
			var path = Path.Combine(libraryPath, fileName);
            System.Diagnostics.Debug.WriteLine("dbPath===={0}", path);
			var platform = new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();
			var connection = new SQLite.Net.SQLiteConnection(platform, path);

			return connection;
		}

		#endregion
	}
}
