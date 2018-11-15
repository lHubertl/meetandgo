using Xamarin.Forms;

namespace MeetAndGoMobile.Infrastructure.Controls
{
    public class ExtendedButton : Button
    {
        public static readonly BindableProperty RoundedHorizontallyProperty =
            BindableProperty.Create(
                nameof(RoundedHorizontally),
                typeof(bool),
                typeof(ExtendedButton));

        public bool ActiveGradient { get; set; }

        public Color GradientStartColor { get; set; }

        public Color GradientEndColor { get; set; }

        public bool RoundedHorizontally
        {
            get => (bool)GetValue(RoundedHorizontallyProperty);
            set => SetValue(RoundedHorizontallyProperty, value);
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == HeightProperty.PropertyName)
            {
                if (RoundedHorizontally)
                {
                    CornerRadius = (int) (Height / 2);
                }
            }
        }
    }
}