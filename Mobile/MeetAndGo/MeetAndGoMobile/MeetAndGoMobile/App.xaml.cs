using MeetAndGoMobile.Modules.LoginModule;
using Prism.Unity;
using MeetAndGoMobile.Views;
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

            NavigationService.NavigateAsync("NavigationPage/MainPage?title=Hello%20from%20Xamarin.Forms");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<MainPage>();
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            ModuleCatalog.AddModule(new ModuleInfo(nameof(LoginModule), typeof(LoginModule), InitializationMode.OnDemand));
        }
    }
}
