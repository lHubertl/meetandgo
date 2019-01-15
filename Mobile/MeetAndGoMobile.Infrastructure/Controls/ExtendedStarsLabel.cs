using System.Linq;
using MeetAndGoMobile.Infrastructure.Constants;
using Xamarin.Forms;

namespace MeetAndGoMobile.Infrastructure.Controls
{
    public class ExtendedStarsLabel : Label
    {
        public static readonly BindableProperty GradeProperty = BindableProperty.Create(
            nameof(Grade),
            typeof(int),
            typeof(ExtendedStarsLabel),
            1,
            propertyChanged: GradePropertyChanged);

        public static readonly BindableProperty MaxGradeProperty = BindableProperty.Create(
            nameof(MaxGrade),
            typeof(int),
            typeof(ExtendedStarsLabel),
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

        public ExtendedStarsLabel()
        {
            TextColor = Colors.StarColor;
            UpdateContent();
        }

        private static void GradePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if(!(bindable is ExtendedStarsLabel control))
            {
                return;
            }

            control.UpdateContent();
        }
        
        private void UpdateContent()
        {
            const string star = "★ ";
            const string emptyStar = "☆ ";

            var text =
                $"{string.Concat(Enumerable.Repeat(star, Grade))}{string.Concat(Enumerable.Repeat(emptyStar, MaxGrade - Grade))}";

            Text = text;
        }
    }
}