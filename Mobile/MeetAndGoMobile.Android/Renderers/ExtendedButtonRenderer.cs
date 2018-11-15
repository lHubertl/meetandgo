using System.ComponentModel;
using Android.Content;
using MeetAndGoMobile.Droid.Renderers;
using MeetAndGoMobile.Infrastructure.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ExtendedButton), typeof(ExtendedButtonRenderer))]

namespace MeetAndGoMobile.Droid.Renderers
{
    public class ExtendedButtonRenderer : ButtonRenderer
    {
        public ExtendedButtonRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (!(Element is ExtendedButton element) || Control is null)
            {
                return;
            }

            if (element.ActiveGradient)
            {
                ConfigureGradient(element);
            }

        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Element is null || Control is null)
            {
                return;
            }

            if (e.PropertyName == VisualElement.HeightProperty.PropertyName)
            {

            }
        }

        private void ConfigureGradient(ExtendedButton element)
        {
            //Element.BackgroundColor = Color.Transparent;

            //var startColor = element.GradientStartColor.ToAndroid();
            //var endColor = element.GradientEndColor.ToAndroid();

            //var mask = new GradientDrawable();
            //mask.SetColor(Color.Red.ToAndroid());
            //mask.SetCornerRadius(element.CornerRadius * Resources.DisplayMetrics.Density);

            //var gd = new GradientDrawable(GradientDrawable.Orientation.TlBr, new int[] {startColor, endColor});

            //int[][] states = new int[][] { new int[] { Control.DrawingCacheBackgroundColor } };
            //int[] colors = new int[] { Color.Blue.ToAndroid() }; // sets the ripple color to blue

            //ColorStateList colorStateList = new ColorStateList(states, colors);

            //var ripple = new RippleDrawable(colorStateList, gd, mask);

            //Control.Background = ripple;
        }
    }
}