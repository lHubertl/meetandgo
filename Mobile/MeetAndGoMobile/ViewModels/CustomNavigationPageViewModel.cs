using Prism.Ioc;
using Prism.Navigation;

namespace MeetAndGoMobile.ViewModels
{
    public class CustomNavigationPageViewModel : ViewModelBase
    {
        public CustomNavigationPageViewModel(INavigationService navigationService, IContainerProvider container)
            : base(navigationService, container)
        {

        }
    }
}
