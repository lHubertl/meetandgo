using Prism.Navigation;
using Prism.Services;

namespace MeetAndGoMobile.ViewModels
{
    public class CustomNavigationPageViewModel : ViewModelBase
    {
        public CustomNavigationPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {

        }
    }
}
