using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using MeetAndGoMobile.Infrastructure.DependencyServices;

namespace MeetAndGoMobile.UWP.DependencyServices
{
    public class PicturePickerService : IPicturePicker
    {
        public async Task<(Stream stream, string name, string format)> PickImageAsync()
        {
            // Create and initialize the FileOpenPicker
            var openPicker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.PicturesLibrary,
            };

            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");

            // Get a file and return a Stream
            StorageFile storageFile = await openPicker.PickSingleFileAsync();

            if (storageFile == null)
            {
                return (null, null, null);
            }

            var raStream = await storageFile.OpenReadAsync();
            return (stream: raStream.AsStreamForRead(), name: storageFile.DisplayName, format: storageFile.FileType);
        }
    }
}
