using Android.Content.Res;
using MeetAndGoMobile.Droid.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportEffect(typeof(SimplifyDatePickerEffect), nameof(SimplifyDatePickerEffect))]
namespace MeetAndGoMobile.Droid.Effects
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

            Control.BackgroundTintList = ColorStateList.ValueOf(new Android.Graphics.Color(134, 134, 134));
        }

        protected override void OnDetached()
        {

        }
    }
}