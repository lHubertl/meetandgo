using System;
using System.IO;
using MeetAndGoMobile.Common.Dependencies;
using Xamarin.Forms;
using FileHelper = MeetAndGoMobile.iOS.Dependencies.FileHelper;

[assembly: Dependency(typeof(FileHelper))]
namespace MeetAndGoMobile.iOS.Dependencies
{
    public class FileHelper : IFileHelper
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