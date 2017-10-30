using System.Threading.Tasks;
using MeetAndGoMobile.Modules.LoginModule;
using Prism.Unity;
using MeetAndGoMobile.Views;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Xamarin.Forms;

namespace MeetAndGoMobile
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<SmartNavigationPage>();
            Container.RegisterTypeForNavigation<SmartMasterDetailPage>();
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            ModuleCatalog.AddModule(new ModuleInfo(nameof(LoginModule), typeof(LoginModule), InitializationMode.OnDemand));
        }

        protected override async void OnStart()
        {
            base.OnStart();

            await NavigateToLoginPageAsync();

            //await NavigateToMainPageAsync();
        }

        protected override void OnSleep()
        {
            base.OnSleep();
        }

        protected override void OnResume()
        {
            base.OnResume();
        }

        private async Task NavigateToLoginPageAsync()
        {
            Container.Resolve<IModuleManager>().LoadModule("LoginModule");
            await NavigationService.NavigateAsync("SmartNavigationPage/LoginPage");
        }

        private async Task NavigateToMainPageAsync()
        {
            Container.Resolve<IModuleManager>().LoadModule("MainModule");
            await NavigationService.NavigateAsync("SmartNavigationPage/MainPage");
        }
    }
}
