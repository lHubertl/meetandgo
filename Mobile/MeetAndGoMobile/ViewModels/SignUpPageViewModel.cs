using Prism.Navigation;
using System.Windows.Input;
using System.Threading.Tasks;
using MeetAndGoMobile.Infrastructure.Commands;
using MeetAndGoMobile.Views;

namespace MeetAndGoMobile.ViewModels
{
    public class SignUpPageViewModel : ViewModelBase
    {
        private string _phoneNumberText;
        public string PhoneNumberText
        {
            get => _phoneNumberText;
            set => SetProperty(ref _phoneNumberText, value);
        }
        
        public ICommand SignInCommand => new SingleExecutionCommand(async () => await ExecuteSignInCommandAsync());
        
        public ICommand TermCommand => new SingleExecutionCommand(async() => await ExecuteTermCommandAsync());

        public ICommand ContinueCommand => new SingleExecutionCommand(async () => await ExecuteContinueCommandAsync());

        public SignUpPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
        }

        private Task ExecuteContinueCommandAsync()
        {
            return NavigationService.NavigateAsync(nameof(ConfirmPhonePage));
        }

        private async Task ExecuteSignInCommandAsync()
        {

        }

        private async Task ExecuteTermCommandAsync()
        {

        }

    }
}
