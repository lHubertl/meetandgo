using System.Threading.Tasks;
using Prism.Unity;
using MeetAndGoMobile.Views;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using MeetAndGoMobile.Modules.LoginModule;
using MeetAndGoMobile.Modules.LoginModule.Views;
using MeetAndGoMobile.Modules.MeetingModule;
using MeetAndGoMobile.Modules.MeetingModule.Views;

namespace MeetAndGoMobile
{
    public partial class App : PrismApplication
    {
        private IModuleManager _moduleManager;

        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            _moduleManager = Container.Resolve<IModuleManager>();
            await NavigateToLoginPageAsync();
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<SmartNavigationPage>();
            Container.RegisterTypeForNavigation<SmartMasterDetailPage>();
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            ModuleCatalog.AddModule(new ModuleInfo(nameof(LoginModule), typeof(LoginModule), InitializationMode.OnDemand));
            ModuleCatalog.AddModule(new ModuleInfo(nameof(MeetingModule), typeof(MeetingModule), InitializationMode.OnDemand));
        }

        private async Task NavigateToLoginPageAsync()
        {
            _moduleManager.LoadModule(nameof(LoginModule));
            await NavigationService.NavigateAsync($"{nameof(SmartNavigationPage)}/{nameof(LoginPage)}");
        }

        private async Task NavigateToMeetingPageAsync()
        {
            _moduleManager.LoadModule(nameof(MeetingModule));
            await NavigationService.NavigateAsync($"{nameof(SmartNavigationPage)}/{nameof(MeetingPage)}");
        }
    }
}
