using System.IO;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using ImageCircle.Forms.Plugin.Droid;
using MeetAndGoMobile.Droid.DependencyServices;
using MeetAndGoMobile.Infrastructure.DependencyServices;
using Prism;
using Prism.Ioc;

namespace MeetAndGoMobile.Droid
{
    [Activity(
        Label = "Meet & Go", 
        Icon = "@mipmap/ic_launcher", 
        Theme = "@style/MainTheme", 
        MainLauncher = true,
        WindowSoftInputMode = SoftInput.AdjustResize,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]

    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static MainActivity CurrentActivity;

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat)
            {
                // Full screen with visible status bar
                Window.AddFlags(WindowManagerFlags.LayoutNoLimits);
            }

            base.OnCreate(bundle);

            Xamarin.Essentials.Platform.Init(this, bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            ImageCircleRenderer.Init();

            Xamarin.Forms.DependencyService.Register<IStatusBarController>();
            Xamarin.Forms.DependencyService.Register<IPicturePicker, PicturePickerService>();

            LoadApplication(new App(new AndroidInitializer()));

            CurrentActivity = this;
        }

        // Field, property, and method for Picture Picker
        public static readonly int PickImageId = 1000;

        public TaskCompletionSource<(Stream stream, string name, string format)> PickImageTaskCompletionSource { set; get; }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent intent)
        {
            base.OnActivityResult(requestCode, resultCode, intent);

            if (requestCode == PickImageId)
            {
                if (resultCode == Result.Ok && intent != null)
                {
                    var uri = intent.Data;
                    var stream = ContentResolver.OpenInputStream(uri);

                    // Set the Stream as the completion of the Task
                    // TODO: RETURN REAL FORMAT
                    PickImageTaskCompletionSource.SetResult((stream: stream, name: "test", format: ".jpg"));
                }
                else
                {
                    PickImageTaskCompletionSource.SetResult((null, null, null));
                }
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}

