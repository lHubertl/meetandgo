using Prism;
using Prism.Ioc;
using MeetAndGoMobile.ViewModels;
using MeetAndGoMobile.Views;
using Xamarin.Forms.Xaml;
using MeetAndGoMobile.Infrastructure.Resources;
using Plugin.Multilingual;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MeetAndGoMobile
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            // Set current device language to application
            Strings.Culture = CrossMultilingual.Current.CurrentCultureInfo;

            await NavigationService.NavigateAsync($"{nameof(CustomNavigationPage)}/{nameof(SignUpPage)}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<CustomNavigationPage, CustomNavigationPageViewModel>();
            containerRegistry.RegisterForNavigation<SignUpPage, SignUpPageViewModel>();
        }
    }
}
