using MeetAndGoMobile.Infrastructure.Constants;
using Xamarin.Forms;

namespace MeetAndGoMobile.Infrastructure.Controls
{
    public class ExtendedEntry : Entry
    {
        /// <summary>
        /// Not implemented on Android
        /// Not implemented on iOS
        /// </summary>
        public Thickness Border { get; set; } = new Thickness(0,0,0, 2);
        
        public Color BorderColor { get; set; } = Colors.Gray;
    }
}
