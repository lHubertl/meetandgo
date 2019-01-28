using Android.App;
using Android.Content;
using Android.Views.InputMethods;
using MeetAndGoMobile.Droid.DependencyServices;
using MeetAndGoMobile.Infrastructure.DependencyServices;

[assembly: Xamarin.Forms.Dependency(typeof(KeyboardController))]
namespace MeetAndGoMobile.Droid.DependencyServices
{
    public class KeyboardController : IKeyboardController
    {
        public void HideKeyboard()
        {
            var context = MainActivity.CurrentActivity;

            if (context?.GetSystemService(Context.InputMethodService) is InputMethodManager inputMethodManager && context != null)
            {
                var activity = context as Activity;
                var token = activity.CurrentFocus?.WindowToken;
                inputMethodManager.HideSoftInputFromWindow(token, HideSoftInputFlags.None);

                activity.Window.DecorView.ClearFocus();
            }
        }
    }
}
