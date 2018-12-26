using Android.Content.Res;
using Android.Widget;
using MeetAndGoMobile.Droid.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Xamarin.Forms.Color;

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
                editText.BackgroundTintList = ColorStateList.ValueOf(new Android.Graphics.Color(134,134,134));
            }
        }

        protected override void OnDetached()
        {

        }
    }
}