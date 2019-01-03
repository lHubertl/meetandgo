using System;
using MeetAndGoMobile.iOS.Renderers;
using MeetAndGoMobile.Infrastructure.Controls;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedEntry), typeof(ExtendedEntryRenderer))]
namespace MeetAndGoMobile.iOS.Renderers
{
    internal class ExtendedEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (!(Element is ExtendedEntry element) || Control is null)
            {
                return;
            }

            Control.TintColor = element.BorderColor.ToUIColor();

            if (Math.Abs(element.Border.HorizontalThickness) < 1 &&
                Math.Abs(element.Border.VerticalThickness) < 1)
            {
                Control.BorderStyle = UITextBorderStyle.None;
            }
            else
            {
                Control.BorderStyle = UITextBorderStyle.Line;
            }
        }
    }
}