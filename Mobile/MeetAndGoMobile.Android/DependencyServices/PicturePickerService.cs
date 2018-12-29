using System.IO;
using System.Threading.Tasks;
using Android.Content;
using MeetAndGoMobile.Infrastructure.DependencyServices;
using MeetAndGoMobile.Infrastructure.Resources;

namespace MeetAndGoMobile.Droid.DependencyServices
{
    public class PicturePickerService : IPicturePicker
    {
        public Task<(Stream stream, string name, string format)> PickImageAsync()
        {
            // Define the Intent for getting images
            Intent intent = new Intent();
            intent.SetType("image/*");
            intent.SetAction(Intent.ActionGetContent);

            // Start the picture-picker activity (resumes in MainActivity.cs)
            MainActivity.CurrentActivity.StartActivityForResult(Intent.CreateChooser(intent, Strings.SelectPicture), MainActivity.PickImageId);

            // Save the TaskCompletionSource object as a MainActivity property
            MainActivity.CurrentActivity.PickImageTaskCompletionSource = new TaskCompletionSource<(Stream stream, string name, string format)>();

            // Return Task object
            return MainActivity.CurrentActivity.PickImageTaskCompletionSource.Task;
        }
    }
}