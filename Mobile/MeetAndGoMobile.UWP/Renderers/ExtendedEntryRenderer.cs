using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using MeetAndGoMobile.Infrastructure.Controls;
using MeetAndGoMobile.UWP.Extensions;
using MeetAndGoMobile.UWP.Renderers;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(ExtendedEntry), typeof(ExtendedEntryRenderer))]
namespace MeetAndGoMobile.UWP.Renderers
{
    internal class ExtendedEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Entry> e)
        {
            base.OnElementChanged(e);

            if (Control is null)
            {
                return;
            }

            if (!(Element is ExtendedEntry element))
            {
                return;
            }

            Control.BorderThickness = element.Border.Convert();
            Control.BorderBrush = new SolidColorBrush(element.BorderColor.Convert());

            Control.Style = (Style)Application.Current.Resources["SimplifyEntryEffectStyle"];
        }
    }
}
