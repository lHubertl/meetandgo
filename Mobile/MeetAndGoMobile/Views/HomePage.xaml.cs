using MeetAndGoMobile.Infrastructure.DependencyServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeetAndGoMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            DependencyService.Get<IStatusBarController>()?.SetVisibility(false);
        }
    }
}
