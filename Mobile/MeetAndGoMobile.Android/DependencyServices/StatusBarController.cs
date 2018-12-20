using Android.OS;
using Android.Views;
using MeetAndGoMobile.Droid.DependencyServices;
using MeetAndGoMobile.Infrastructure.DependencyServices;

[assembly: Xamarin.Forms.Dependency(typeof(StatusBarController))]
namespace MeetAndGoMobile.Droid.DependencyServices
{
    public class StatusBarController : IStatusBarController
    {
        public void SetVisibility(bool isVisible)
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat)
            {
                if (isVisible)
                {
                    MainActivity.CurrentActivity.Window.ClearFlags(WindowManagerFlags.LayoutNoLimits);
                }
                else
                {
                    MainActivity.CurrentActivity.Window.AddFlags(WindowManagerFlags.LayoutNoLimits);
                }
            }
        }
    }
}