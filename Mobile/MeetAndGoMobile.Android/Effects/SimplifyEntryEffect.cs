using Android.Widget;
using MeetAndGoMobile.Droid.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName(nameof(MeetAndGoMobile))]
[assembly: ExportEffect(typeof(SimplifyEntryEffect), nameof(SimplifyEntryEffect))]
namespace MeetAndGoMobile.Droid.Effects
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

            if (Control is EditText editText)
            {
                editText.SetHintTextColor(new Color(134, 134, 134).ToAndroid());
            }
        }

        protected override void OnDetached()
        {

        }
    }
}