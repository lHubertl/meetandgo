using Xamarin.Forms;

namespace MeetAndGoMobile.Infrastructure.Controls
{
    public class StarStaticControl : StackLayout
    {
        public static readonly BindableProperty GradeProperty = BindableProperty.Create(
            nameof(Grade),
            typeof(int),
            typeof(StarStaticControl),
            1,
            propertyChanged: GradePropertyChanged);

        public static readonly BindableProperty MaxGradeProperty = BindableProperty.Create(
            nameof(MaxGrade),
            typeof(int),
            typeof(StarStaticControl),
            5,
            propertyChanged: GradePropertyChanged);

        public int Grade
        {
            get => (int)GetValue(GradeProperty);
            set => SetValue(GradeProperty, value);
        }
        public int MaxGrade
        {
            get => (int)GetValue(MaxGradeProperty);
            set => SetValue(MaxGradeProperty, value);
        }

        public StarStaticControl()
        {
            Orientation = StackOrientation.Horizontal;
            Spacing = 5;
            UpdateUi();
        }

        private static void GradePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if(!(bindable is StarStaticControl control))
            {
                return;
            }

            control.UpdateUi();
        }
        
        private void UpdateUi()
        {
            Children.Clear();

            for (int i = 1; i <= MaxGrade; i++)
            {
                var imageSource = i <= Grade ? "small_star.png" : "small_empty_star.png";
                var starImage = new Image { Source = imageSource };
                Children.Add(starImage);
            }
        }
    }
}