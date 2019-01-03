using System;
using Android.Content;
using Android.Content.Res;
using MeetAndGoMobile.Droid.Renderers;
using MeetAndGoMobile.Infrastructure.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ExtendedEntry), typeof(ExtendedEntryRenderer))]
namespace MeetAndGoMobile.Droid.Renderers
{
    internal class ExtendedEntryRenderer : EntryRenderer
    {
        public ExtendedEntryRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (!(Element is ExtendedEntry element) || Control is null)
            {
                return;
            }
            
            Control.BackgroundTintList = ColorStateList.ValueOf(element.BorderColor.ToAndroid());
            
            if (Math.Abs(element.Border.HorizontalThickness) < 1 && 
                Math.Abs(element.Border.VerticalThickness) < 1)
            {
                Control.Background = null;
            }
        }
    }
}