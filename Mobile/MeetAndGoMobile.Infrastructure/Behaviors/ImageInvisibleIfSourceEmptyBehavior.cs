using Xamarin.Forms;

namespace MeetAndGoMobile.Infrastructure.Behaviors
{
    public class ImageInvisibleIfSourceEmptyBehavior : Behavior<Image>
    {
        protected override void OnAttachedTo(Image bindable)
        {
            base.OnAttachedTo(bindable);

            // Perform setup
            bindable.PropertyChanging += Bindable_PropertyChanging;
        }

        protected override void OnDetachingFrom(Image bindable)
        {
            base.OnDetachingFrom(bindable);

            // Perform clean up
            bindable.PropertyChanging -= Bindable_PropertyChanging;
        }

        private void Bindable_PropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            if (e.PropertyName == Image.SourceProperty.PropertyName)
            {
                if (sender is Image image)
                {
                    image.IsVisible = image.Source != null;
                }
            }
        }
    }
}
