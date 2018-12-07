using MeetAndGoMobile.Infrastructure.Commands;
using Prism.Ioc;
using Prism.Navigation;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;

namespace MeetAndGoMobile.ViewModels
{
    public class MasterPageViewModel : ViewModelBase
    {
        public string Version => $"v{VersionTracking.CurrentVersion}";

        public ICommand NavigateCommand => new SingleExecutionCommand(ExecuteNavigateCommand);

        public MasterPageViewModel(INavigationService navigationService, IContainerProvider container) : base(navigationService, container)
        {
        }
        
        private async Task ExecuteNavigateCommand(object pageNameObj)
        {
            if(pageNameObj is string pageName)
            {
                await NavigationService.NavigateAsync(pageName);
            }
        }
    }
}
