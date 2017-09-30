using System;
using System.IO;
using Android.OS;
using App2.Droid.SQLite;
using App2.SQLite;
using Xamarin.Forms;


[assembly: Dependency(typeof(SQLiteHelper))]
namespace App2.Droid.SQLite
{
    public class SQLiteHelper : ISQLiteHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}