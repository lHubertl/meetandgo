using MeetAndGoMobile.Infrastructure.Constants;
using MeetAndGoMobile.Infrastructure.UserControls.ViewModels;
using Xamarin.Forms;

namespace MeetAndGoMobile.Infrastructure.UserControls
{
    public class BaseContentTemplate : Grid
    {
        private View _navigationBarView;

        public static readonly BindableProperty HasNavigationBarProperty = BindableProperty.Create(
            nameof(HasNavigationBar),
            typeof(bool),
            typeof(BaseContentTemplate),
            true,
            propertyChanged: HasNavigationBarPropertyChanged);

        public static readonly BindableProperty NavigationBarProperty = BindableProperty.Create(
            nameof(NavigationBar),
            typeof(NavigationBarViewModel),
            typeof(BaseContentTemplate),
            default(NavigationBarViewModel),
            propertyChanged: NavigationBarPropertyChanged);

        public bool HasNavigationBar
        {
            get => (bool)GetValue(HasNavigationBarProperty);
            set => SetValue(HasNavigationBarProperty, value);
        }
        public NavigationBarViewModel NavigationBar
        {
            get => (NavigationBarViewModel)GetValue(NavigationBarProperty);
            set => SetValue(NavigationBarProperty, value);
        }

        public BaseContentTemplate()
        {
            RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });

            var content = new ContentPresenter();
            Children.Add(content, 0, 1);
        }

        private static void NavigationBarPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is BaseContentTemplate control))
            {
                return;
            }

            if (newValue is NavigationBarViewModel value)
            {
                if (!control.HasNavigationBar)
                {
                    return;
                }

                if (control._navigationBarView == null)
                {
                    control.InitializeNavigationBar();
                }
            }
            else
            {
                if (control._navigationBarView != null)
                {
                    control.Children.Remove(control._navigationBarView);
                    control._navigationBarView = null;
                }
            }
        }

        private static void HasNavigationBarPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is BaseContentTemplate control))
            {
                return;
            }

            if ((bool) newValue)
            {
                if (control._navigationBarView == null)
                {
                    control.InitializeNavigationBar();
                }
            }
            else
            {
                if (control._navigationBarView != null)
                {
                    control.Children.Remove(control._navigationBarView);
                    control._navigationBarView = null;
                }
            }
        }

        private void InitializeNavigationBar()
        {
            var barView = _navigationBarView = new NavigationBar
            {
                BackgroundColor = Colors.StatusBarBrown,
                Padding = new Thickness(0, AppSettings.StatusBarHeight + 10, 0, 10)
            };

            barView.BindingContext = BindingContext;
            barView.SetBinding(UserControls.NavigationBar.LeftTextProperty, $"{nameof(NavigationBar)}.{nameof(NavigationBar.LeftText)}");
            barView.SetBinding(UserControls.NavigationBar.RightTextProperty, $"{nameof(NavigationBar)}.{nameof(NavigationBar.RightText)}");
            barView.SetBinding(UserControls.NavigationBar.TitleProperty, $"{nameof(NavigationBar)}.{nameof(NavigationBar.Title)}");
            barView.SetBinding(UserControls.NavigationBar.LeftImageSourceProperty, $"{nameof(NavigationBar)}.{nameof(NavigationBar.LeftImageSource)}");
            barView.SetBinding(UserControls.NavigationBar.RightImageSourceProperty, $"{nameof(NavigationBar)}.{nameof(NavigationBar.RightImageSource)}");
            barView.SetBinding(UserControls.NavigationBar.LeftTapCommandProperty, $"{nameof(NavigationBar)}.{nameof(NavigationBar.LeftTapCommand)}");
            barView.SetBinding(UserControls.NavigationBar.RightTapCommandProperty, $"{nameof(NavigationBar)}.{nameof(NavigationBar.RightTapCommand)}");

            Children.Add(barView, 0, 0);
        }
    }
}