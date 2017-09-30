using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using App2.iOS.SQLite;
using App2.SQLite;
using Foundation;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteHelper))]
namespace App2.iOS.SQLite
{
    public class SQLiteHelper : ISQLiteHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, filename);
        }
    }
}