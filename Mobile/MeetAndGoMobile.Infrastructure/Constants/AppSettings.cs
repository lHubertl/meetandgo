using Xamarin.Forms;

namespace MeetAndGoMobile.Infrastructure.Constants
{
    public static class AppSettings
    {
        public static readonly double StatusBarHeight;

        static AppSettings()
        {
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    StatusBarHeight = 20;
                    break;

                case Device.Android:
                    StatusBarHeight = 20;
                    break;

                case Device.UWP:
                    StatusBarHeight = 20;
                    break;
            }
        }
    }
}
