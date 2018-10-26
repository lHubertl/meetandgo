using Xamarin.Forms;

namespace MeetAndGoMobile.Infrastructure.Effects
{
    public class SimplifyEntryEffect : RoutingEffect
    {
        public SimplifyEntryEffect() : base($"{nameof(MeetAndGoMobile)}.{nameof(SimplifyEntryEffect)}")
        {
        }
    }
}
