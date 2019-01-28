using MeetAndGoMobile.iOS.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName(nameof(MeetAndGoMobile))]
[assembly: ExportEffect(typeof(SimplifyEntryEffect), nameof(SimplifyEntryEffect))]
namespace MeetAndGoMobile.iOS.Effects
{
    public class SimplifyEntryEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
        }

        protected override void OnDetached()
        {
            //throw new NotImplementedException();
        }
    }
}