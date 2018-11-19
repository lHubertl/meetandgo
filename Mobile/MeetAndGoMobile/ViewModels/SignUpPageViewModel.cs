using System.Threading;
using Prism.Navigation;
using System.Windows.Input;
using System.Threading.Tasks;
using MeetAndGo.Shared.Managers;
using MeetAndGo.Shared.Models.Authorization;
using MeetAndGoMobile.Constants;
using MeetAndGoMobile.Infrastructure.Commands;
using MeetAndGoMobile.Infrastructure.Resources;
using MeetAndGoMobile.Services;
using MeetAndGoMobile.Views;
using Prism.Services;

namespace MeetAndGoMobile.ViewModels
{
    public class SignUpPageViewModel : ViewModelBase
    {
        private readonly IAccountService _accountService;

        private string _phoneNumberText;
        public string PhoneNumberText
        {
            get => _phoneNumberText;
            set => SetProperty(ref _phoneNumberText, value);
        }
        
        public ICommand SignInCommand => new SingleExecutionCommand(async () => await ExecuteSignInCommandAsync());
        
        public ICommand TermCommand => new SingleExecutionCommand(async() => await ExecuteTermCommandAsync());

        public ICommand ContinueCommand => new SingleExecutionCommand(async () => await ExecuteContinueCommandAsync());

        public SignUpPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IAccountService accountService) : base(navigationService, dialogService)
        {
            _accountService = accountService;
        }

        private async Task ExecuteSignInCommandAsync()
        {
            await UserNotificationAsync(Strings.NotImplemented);
        }

        private async Task ExecuteTermCommandAsync()
        {
            await UserNotificationAsync(Strings.NotImplemented);
        }

        private async Task ExecuteContinueCommandAsync()
        {
            var validationManager = ValidationManager.Create();
            if (!validationManager.ValidatePhoneNumber(PhoneNumberText, Strings.V_PhoneNumber).IsValid)
            {
                await UserNotificationAsync(validationManager.ToString(), Strings.Validation);
                return;
            }

            var model = new MessageConfirmModel { PhoneNumber = PhoneNumberText };

            var result = await PerformDataRequestAsync(() => _accountService.ConfirmPhoneNumberAsync(model, CancellationToken.None));

            if (result)
            {
                var navigationParameters = new NavigationParameters
                {
                    { NavParamConstants.MessageConfirmModel, model }
                };

                await NavigationService.NavigateAsync(nameof(ConfirmPhonePage), navigationParameters);
            }
        }
    }
}
