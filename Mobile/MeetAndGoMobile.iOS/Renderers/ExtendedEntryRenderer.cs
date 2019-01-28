using System;
using CoreAnimation;
using CoreGraphics;
using MeetAndGoMobile.iOS.Renderers;
using MeetAndGoMobile.Infrastructure.Constants;
using MeetAndGoMobile.Infrastructure.Controls;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedEntry), typeof(ExtendedEntryRenderer))]
namespace MeetAndGoMobile.iOS.Renderers
{
    internal class ExtendedEntryRenderer : EntryRenderer
    {
        private bool _isBorderAdded;

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
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

            element.Margin = new Thickness(
                element.Margin.Left,
                element.Margin.Top + 7,
                element.Margin.Right,
                element.Margin.Bottom
            );

            Control.BorderStyle = UITextBorderStyle.None;
            Control.TintColor = Colors.Green.ToUIColor();

            element.SizeChanged += Element_SizeChanged;
        }

        private void Element_SizeChanged(object sender, EventArgs e)
        {
            if (_isBorderAdded || Element.Width < 0 || Element.Height < 0)
            {
                return;
            }

            _isBorderAdded = true;

            if (!(Element is ExtendedEntry element))
            {
                return;
            }

            // Create borders (bottom only)
            var border = new CALayer();
            float width = (float)element.Border.Bottom;
            border.BorderColor = element.BorderColor.ToCGColor();
            border.Frame = new CGRect(x: 0, y: element.Height - width, width: element.Width, height: width);
            border.BorderWidth = width;

            Control.Layer.AddSublayer(border);

            Control.Layer.MasksToBounds = true;
            Control.BorderStyle = UITextBorderStyle.None;
        }
    }
}