using Xamarin.Forms;

namespace MeetAndGoMobile.Infrastructure.Behaviors
{
    public class ImageInvisibleIfSourceEmptyBehavior : Behavior<Image>
    {
        protected override void OnAttachedTo(Image bind)
        {
            base.OnAttachedTo(bind);

            // Perform setup
            bind.PropertyChanged += Bind_PropertyChanged;
        }


        protected override void OnDetachingFrom(Image bind)
        {
            base.OnDetachingFrom(bind);

            // Perform clean up
            bind.PropertyChanged -= Bind_PropertyChanged;
        }
        
        private void Bind_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
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
