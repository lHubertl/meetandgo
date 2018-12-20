using System.Windows.Input;
using MeetAndGoMobile.Services;
using Prism.Ioc;
using Prism.Navigation;
using Xamarin.Forms;

namespace MeetAndGoMobile.ViewModels
{
	public class HomePageViewModel : ViewModelBase
	{
        private readonly IMasterPageService _masterPageService;

        public ICommand ToggleMasterCommand => new Command(() => _masterPageService.ToggleMaster());

        public HomePageViewModel(INavigationService navigationService, IContainerProvider container, IMasterPageService masterPageService) 
            : base(navigationService, container)
        {
            _masterPageService = masterPageService;
        }
    }
}
