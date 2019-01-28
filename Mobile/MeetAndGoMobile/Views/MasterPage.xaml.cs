using System;
using MeetAndGoMobile.Infrastructure.DependencyServices;
using MeetAndGoMobile.Services.Contracts;
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

            _masterPageService.Subscribe(ToggleMaster);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            _masterPageService.Unsubscribe(ToggleMaster);
        }

        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            if (Detail is NavigationPage navigationPage)
            {
                if (navigationPage.CurrentPage.BindingContext is ViewModelBase viewModelBase)
                {
                    if (e is TappedEventArgs tappedEventArgs)
                    {
                        IsPresented = false;

                        var newPageName = tappedEventArgs.Parameter.ToString();

                        if (string.Equals(newPageName, navigationPage.CurrentPage.GetType().Name))
                        {
                            // No need to navigate to the same page stack twice
                            return;
                        }

                        DependencyService.Get<IStatusBarController>()?.SetVisibility(true);

                        // TODO: CHECK NAVIGATION TO PAGE A THEN PAGE A + 1 THEN PAGE B THEN GO BACK
                        viewModelBase.NavigationService.NavigateAsync(newPageName);
                    }
                }
            }
        }

        private void ToggleMaster(ToggleActions action)
        {
            if (action == ToggleActions.ToggleMenu)
            {
                IsPresented = !IsPresented;
            }
        }
    }
}