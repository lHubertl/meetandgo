using Windows.UI;
using Windows.UI.Xaml;

namespace MeetAndGoMobile.UWP.Extensions
{
    internal static class PlatformExtensions
    {
        internal static Color Convert(this Xamarin.Forms.Color color)
        {
            return Color.FromArgb(
                (byte)(color.A * 255),
                (byte)(color.R * 255),
                (byte)(color.G * 255),
                (byte)(color.B * 255));
        }

        internal static Thickness Convert(this Xamarin.Forms.Thickness thickness)
        {
            return new Thickness(thickness.Left, thickness.Top, thickness.Right, thickness.Bottom);
        }
    }
}
