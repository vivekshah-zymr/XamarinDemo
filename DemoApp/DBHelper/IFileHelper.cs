using System;
using SQLite.Net;

namespace DemoApp.DBHelper
{
    public interface IFileHelper
	{
		//string GetLocalFilePath(string filename);

        SQLiteConnection GetConnection();
	}
}
