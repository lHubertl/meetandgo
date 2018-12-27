using MeetAndGoMobile.UWP.Effects;
using Windows.UI.Xaml.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
using Application = Windows.UI.Xaml.Application;

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
                var fontFamily = textBox.FontFamily;
                var fontSize = textBox.FontSize;

                var style = (Windows.UI.Xaml.Style) Application.Current.Resources["SimplifyEntryEffectStyle"];
                textBox.Style = style;

                textBox.FontFamily = fontFamily;
                textBox.FontSize = fontSize;
            }
        }
        
        protected override void OnDetached()
        {

        }
    }
}
