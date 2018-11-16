using System.Threading.Tasks;
using System.Windows.Input;
using MeetAndGoMobile.Infrastructure.Commands;
using MeetAndGoMobile.Views;
using Prism.Navigation;

namespace MeetAndGoMobile.ViewModels
{
	public class ConfirmPhonePageViewModel : ViewModelBase
	{
        private string _confirmationCode;
        public string ConfirmationCode
        {
            get => _confirmationCode;
            set => SetProperty(ref _confirmationCode, value);
        }

	    public ICommand ContinueCommand => new SingleExecutionCommand(async () => await ExecuteContinueCommandAsync());

	    public ConfirmPhonePageViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

	    private async Task ExecuteContinueCommandAsync()
	    {
	        await NavigationService.NavigateAsync(nameof(CreateAccountPage));
	    }
    }
}
