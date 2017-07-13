using MeetAndGoMobile.Modules.LoginModule.Views;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Unity;

namespace MeetAndGoMobile.Modules.LoginModule
{
    public class LoginModule : IModule
    {
        private readonly IUnityContainer _unityContainer;

        public LoginModule(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        public void Initialize()
        {
            _unityContainer.RegisterTypeForNavigation<LoginPage>();
        }
    }
}
