using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeetAndGoMobile.Infrastructure.UserControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavigationBar : Grid
    {
        #region Public fields

        public static readonly BindableProperty LeftTextProperty = BindableProperty.Create(
            nameof(LeftText),
            typeof(string),
            typeof(NavigationBar),
            propertyChanged: LeftTextPropertyChanged);

        public static readonly BindableProperty RightTextProperty = BindableProperty.Create(
            nameof(RightText),
            typeof(string),
            typeof(NavigationBar),
            propertyChanged: RightTextPropertyChanged);

        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
            nameof(Title),
            typeof(string),
            typeof(NavigationBar),
            propertyChanged: TitlePropertyChanged);

        public static readonly BindableProperty LeftImageSourceProperty = BindableProperty.Create(
            nameof(LeftImageSource),
            typeof(string),
            typeof(NavigationBar),
            propertyChanged: LeftImageSourcePropertyChanged);

        public static readonly BindableProperty RightImageSourceProperty = BindableProperty.Create(
            nameof(RightImageSource),
            typeof(string),
            typeof(NavigationBar),
            propertyChanged: RightImageSourcePropertyChanged);

        public static readonly BindableProperty LeftTapCommandProperty = BindableProperty.Create(
            nameof(LeftTapCommand),
            typeof(ICommand),
            typeof(NavigationBar),
            propertyChanged: LeftTapCommandPropertyChanged);

        public static readonly BindableProperty RightTapCommandProperty = BindableProperty.Create(
            nameof(RightTapCommand),
            typeof(ICommand),
            typeof(NavigationBar),
            propertyChanged: RightTapCommandPropertyChanged);

        public string LeftText
        {
            get => (string)GetValue(LeftTextProperty);
            set => SetValue(LeftTextProperty, value);
        }

        public string RightText
        {
            get => (string)GetValue(RightTextProperty);
            set => SetValue(RightTextProperty, value);
        }

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public string LeftImageSource
        {
            get => (string)GetValue(LeftImageSourceProperty);
            set => SetValue(LeftImageSourceProperty, value);
        }

        public string RightImageSource
        {
            get => (string)GetValue(RightImageSourceProperty);
            set => SetValue(RightImageSourceProperty, value);
        }

        public ICommand LeftTapCommand
        {
            get => (ICommand)GetValue(LeftTapCommandProperty);
            set => SetValue(LeftTapCommandProperty, value);
        }

        public ICommand RightTapCommand
        {
            get => (ICommand)GetValue(RightTapCommandProperty);
            set => SetValue(RightTapCommandProperty, value);
        }

        #endregion  Public fields

        #region Constructor

        public NavigationBar()
        {
            InitializeComponent();
        }

        #endregion Constructor

        #region Private methods

        private static void LeftTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is NavigationBar control))
            {
                return;
            }

            var value = newValue as string;

            if (string.IsNullOrWhiteSpace(value))
            {
                control.LeftLabel.Text = string.Empty;
                control.LeftImage.IsVisible = false;
            }
            else
            {
                control.LeftLabel.Text = value;
                control.LeftImage.Source = "top_arrow_left";
                control.LeftImage.IsVisible = true;
            }
        }

        private static void RightTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is NavigationBar control)
            {
                control.RightLabel.Text = newValue as string;
            }
        }

        private static void TitlePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is NavigationBar control)
            {
                control.TitleLabel.Text = newValue as string;
            }
        }

        private static void LeftImageSourcePropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (!(bindable is NavigationBar control))
            {
                return;
            }

            var imageSource = newvalue as string;

            if (!string.IsNullOrEmpty(imageSource))
            {
                control.LeftImage.Source = ImageSource.FromFile(imageSource);
                control.LeftImage.IsVisible = true;
            }
            else
            {
                control.LeftImage.IsVisible = false;
            }
        }

        private static void RightImageSourcePropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (!(bindable is NavigationBar control))
            {
                return;
            }

            var imageSource = newvalue as string;

            if (!string.IsNullOrEmpty(imageSource))
            {
                control.RightImage.Source = ImageSource.FromFile(imageSource);
                control.RightImage.IsVisible = true;
            }
            else
            {
                control.RightImage.IsVisible = false;
            }
        }

        private static void LeftTapCommandPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is NavigationBar control))
            {
                return;
            }

            if (newValue is ICommand command)
            {
                var tapGesture = new TapGestureRecognizer { Command = command };
                control.LeftStackLayout.GestureRecognizers.Clear();
                control.LeftStackLayout.GestureRecognizers.Add(tapGesture);
            }
            else
            {
                control.LeftStackLayout.GestureRecognizers.Clear();
            }
        }

        private static void RightTapCommandPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is NavigationBar control))
            {
                return;
            }

            if (newValue is ICommand command)
            {
                var tapGesture = new TapGestureRecognizer { Command = command };
                control.RightStackLayout.GestureRecognizers.Clear();
                control.RightStackLayout.GestureRecognizers.Add(tapGesture);
            }
            else
            {
                control.RightStackLayout.GestureRecognizers.Clear();
            }
        }

        #endregion Private methods
    }
}