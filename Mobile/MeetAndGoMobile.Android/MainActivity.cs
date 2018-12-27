using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using ImageCircle.Forms.Plugin.Droid;
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
        public static Activity CurrentActivity;

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

            LoadApplication(new App(new AndroidInitializer()));

            CurrentActivity = this;
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

