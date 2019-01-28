using MeetAndGoMobile.iOS.DependencyServices;
using MeetAndGoMobile.Infrastructure.DependencyServices;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(KeyboardController))]
namespace MeetAndGoMobile.iOS.DependencyServices
{
    public class KeyboardController : IKeyboardController
    {
        public void HideKeyboard()
        {
            UIApplication.SharedApplication.KeyWindow.EndEditing(true);
        }
    }
}