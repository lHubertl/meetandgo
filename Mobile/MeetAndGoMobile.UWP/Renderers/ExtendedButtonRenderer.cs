using MeetAndGoMobile.Infrastructure.Controls;
using MeetAndGoMobile.UWP.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
using Application = Windows.UI.Xaml.Application;

[assembly: ExportRenderer(typeof(ExtendedButton), typeof(ExtendedButtonRenderer))]

namespace MeetAndGoMobile.UWP.Renderers
{
    public class ExtendedButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (!(Element is ExtendedButton element) || Control is null)
            {
                return;
            }
            
            if (!element.ActiveGradient)
            {
                ConfigureGreenBorderRoundedButtonStyleStyle();
            }
        }

        private void ConfigureGreenBorderRoundedButtonStyleStyle()
        {
            var style = (Windows.UI.Xaml.Style) Application.Current.Resources["GreenBorderRoundedButtonStyle"];
            Control.Style = style;
        }
    }
}
