using Prism.Ioc;
using Prism.Navigation;

namespace MeetAndGoMobile.ViewModels
{
	public class HomePageViewModel : ViewModelBase
	{
        public HomePageViewModel(INavigationService navigationService, IContainerProvider container) : base(navigationService, container)
        {
        }
    }
}
