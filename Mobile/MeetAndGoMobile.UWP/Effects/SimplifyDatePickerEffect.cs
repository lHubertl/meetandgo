using MeetAndGoMobile.UWP.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportEffect(typeof(SimplifyEntryEffect), nameof(SimplifyEntryEffect))]
namespace MeetAndGoMobile.UWP.Effects
{
    public class SimplifyDatePickerEffect : PlatformEffect
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
            
            // TODO implement design
        }

        protected override void OnDetached()
        {

        }
    }
}
