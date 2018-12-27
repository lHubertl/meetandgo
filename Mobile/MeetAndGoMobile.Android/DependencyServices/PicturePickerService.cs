using System.IO;
using System.Threading.Tasks;
using Android.Content;
using MeetAndGoMobile.Infrastructure.DependencyServices;

namespace MeetAndGoMobile.Droid.DependencyServices
{
    public class PicturePickerService : IPicturePicker
    {
        public Task<Stream> GetImageStreamAsync()
        {
            // Define the Intent for getting images
            Intent intent = new Intent();
            intent.SetType("image/*");
            intent.SetAction(Intent.ActionGetContent);

            // Start the picture-picker activity (resumes in MainActivity.cs)
            MainActivity.CurrentActivity.StartActivityForResult(
                Intent.CreateChooser(intent, "Select Picture"),
                MainActivity.PickImageId);

            // Save the TaskCompletionSource object as a MainActivity property
            MainActivity.CurrentActivity.PickImageTaskCompletionSource = new TaskCompletionSource<Stream>();

            // Return Task object
            return MainActivity.CurrentActivity.PickImageTaskCompletionSource.Task;
        }
    }
}