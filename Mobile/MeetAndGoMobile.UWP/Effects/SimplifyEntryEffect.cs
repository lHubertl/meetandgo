using MeetAndGoMobile.UWP.Effects;
using Windows.UI.Xaml.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
using Windows.UI.Xaml.Media;
using Color = Windows.UI.Color;

[assembly: ResolutionGroupName(nameof(MeetAndGoMobile))]
[assembly: ExportEffect(typeof(SimplifyEntryEffect), nameof(SimplifyEntryEffect))]
namespace MeetAndGoMobile.UWP.Effects
{
    public class SimplifyEntryEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            if (Control is null)
            {
                return;
            }

            if (Element is null)
            {
                return;
            }

            if (Control is TextBox textBox)
            {
                textBox.BorderThickness = new Windows.UI.Xaml.Thickness(0, 0, 0, 2);
                textBox.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 91, 91, 91));
            }
        }

        protected override void OnDetached()
        {

        }
    }
}
