using System;
using System.IO;
using Xamarin.Forms;
using MeetAndGoMobile.Common.Dependencies;

[assembly: Dependency(typeof(MeetAndGoMobile.Droid.Dependencies.FileHelper))]
namespace MeetAndGoMobile.Droid.Dependencies
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}