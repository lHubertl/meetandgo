using System;
using MeetAndGoMobile.Infrastructure.DependencyServices;
using MeetAndGoMobile.Services;
using MeetAndGoMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeetAndGoMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPage : MasterDetailPage
    {
        private readonly IMasterPageService _masterPageService;

        public MasterPage(IMasterPageService masterPageService)
        {
            _masterPageService = masterPageService;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _masterPageService.SubscribeToggleListener(ToggleMaster);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            _masterPageService.UnsubscribeToggleListener(ToggleMaster);
        }

        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            if (Detail is NavigationPage navigationPage)
            {
                if (navigationPage.CurrentPage.BindingContext is ViewModelBase viewModelBase)
                {
                    if (e is TappedEventArgs tappedEventArgs)
                    {
                        ToggleMaster();
                        viewModelBase.NavigationService.NavigateAsync(tappedEventArgs.Parameter.ToString());

                        DependencyService.Get<IStatusBarController>()?.SetVisibility(true);
                    }
                }
            }
        }

        private void ToggleMaster()
        {
            IsPresented = !IsPresented;
        }
    }
}