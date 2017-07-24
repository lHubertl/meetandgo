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

            Container.Resolve<IModuleManager>().LoadModule("LoginModule");
            NavigationService.NavigateAsync("SmartNavigationPage/LoginPage");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterTypeForNavigation<SmartNavigationPage>();
            Container.RegisterTypeForNavigation<SmartMasterDetailPage>();
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            ModuleCatalog.AddModule(new ModuleInfo(nameof(LoginModule), typeof(LoginModule), InitializationMode.OnDemand));
        }
    }
}
