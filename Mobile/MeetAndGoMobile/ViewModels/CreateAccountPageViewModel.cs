using System.Threading.Tasks;
using System.Windows.Input;
using MeetAndGoMobile.Infrastructure.Commands;
using Prism.Navigation;

namespace MeetAndGoMobile.ViewModels
{
	public class CreateAccountPageViewModel : ViewModelBase
	{
        private string _yourName;
        public string YourName
        {
            get => _yourName;
            set => SetProperty(ref _yourName, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private string _confirmedPassword;
        public string ConfirmedPassword
        {
            get => _confirmedPassword;
            set => SetProperty(ref _confirmedPassword, value);
        }

        public ICommand CreateAccountCommand => new SingleExecutionCommand(ExecuteCreateAccountCommand);

	    public CreateAccountPageViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

	    private Task ExecuteCreateAccountCommand()
	    {
	        throw new System.NotImplementedException();
	    }
    }
}
