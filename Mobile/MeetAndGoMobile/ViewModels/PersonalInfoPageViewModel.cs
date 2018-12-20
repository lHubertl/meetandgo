using System.Threading.Tasks;
using System.Windows.Input;
using MeetAndGoMobile.Infrastructure.Commands;
using Prism.Ioc;
using Prism.Navigation;

namespace MeetAndGoMobile.ViewModels
{
    public class PersonalInfoPageViewModel : ViewModelBase
    {
        public ICommand SaveChangesCommand => new SingleExecutionCommand(ExecuteSaveChangesCommand);

        public PersonalInfoPageViewModel(INavigationService navigationService, IContainerProvider container)
            : base(navigationService, container)
        {

        }

        private async Task ExecuteSaveChangesCommand()
        {

        }
    }
}
