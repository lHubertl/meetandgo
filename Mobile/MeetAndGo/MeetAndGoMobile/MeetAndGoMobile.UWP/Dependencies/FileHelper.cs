using System.IO;
using Windows.Storage;
using Xamarin.Forms;
using MeetAndGoMobile.Common.Dependencies;
using FileHelper = MeetAndGoMobile.UWP.Dependencies.FileHelper;

[assembly: Dependency(typeof(FileHelper))]
namespace MeetAndGoMobile.UWP.Dependencies
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            return Path.Combine(ApplicationData.Current.LocalFolder.Path, filename);
        }
    }
}
