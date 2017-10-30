using MeetAndGoMobile.Modules.MainModule.Views;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Unity;

namespace MeetAndGoMobile.Modules.MainModule
{
    public class MainModule : IModule
    {
        private readonly IUnityContainer _unityContainer;

        public MainModule(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        public void Initialize()
        {
            _unityContainer.RegisterTypeForNavigation<MainPage>();
        }
    }
}
