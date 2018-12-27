using System;
using MeetAndGoMobile.iOS.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportEffect(typeof(SimplifyDatePickerEffect), nameof(SimplifyDatePickerEffect))]
namespace MeetAndGoMobile.iOS.Effects
{
    public class SimplifyDatePickerEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            throw new NotImplementedException();
        }

        protected override void OnDetached()
        {
            throw new NotImplementedException();
        }
    }
}