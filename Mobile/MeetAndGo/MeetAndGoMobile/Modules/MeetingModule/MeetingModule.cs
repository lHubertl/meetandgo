using MeetAndGoMobile.Modules.MeetingModule.Views;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Unity;

namespace MeetAndGoMobile.Modules.MeetingModule
{
    public class MeetingModule : IModule
    {
        private readonly IUnityContainer _unityContainer;

        public MeetingModule(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        public void Initialize()
        {
            _unityContainer.RegisterTypeForNavigation<MeetingPage>();
        }
    }
}
