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
using Prism.Ioc;

namespace MeetAndGoMobile.ViewModels
{
    public class SignUpPageViewModel : ViewModelBase
    {
        private readonly IAccountService _accountService;

        private string _phoneNumber;
        public string PhoneNumber
        {
            get => _phoneNumber;
            set => SetProperty(ref _phoneNumber, value);
        }
        
        public ICommand SignInCommand => new SingleExecutionCommand(async () => await ExecuteSignInCommandAsync());
        
        public ICommand TermCommand => new SingleExecutionCommand(async() => await ExecuteTermCommandAsync());

        public ICommand ContinueCommand => new SingleExecutionCommand(async () => await ExecuteContinueCommandAsync());

        public SignUpPageViewModel(INavigationService navigationService, IContainerProvider container, IAccountService accountService)
            : base(navigationService, container)
        {
            _accountService = accountService;
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            if (parameters == null)
            {
                return;
            }

            if (parameters.TryGetValue(StringConstants.PhoneNumber, out string phoneNumber))
            {
                PhoneNumber = phoneNumber;
            }
        }

        private Task ExecuteSignInCommandAsync()
        {
            var navParams = new NavigationParameters
            {
                { StringConstants.PhoneNumber, PhoneNumber }
            };

            return NavigationService.NavigateAsync(nameof(SignInPage), navParams);
        }

        private async Task ExecuteTermCommandAsync()
        {
            await UserNotificationAsync(Strings.NotImplemented);
        }

        private async Task ExecuteContinueCommandAsync()
        {
            var validationManager = ValidationManager.Create();
            if (!validationManager.ValidatePhoneNumber(PhoneNumber, Strings.V_PhoneNumber).IsValid)
            {
                await UserNotificationAsync(validationManager.ToString(), Strings.Validation);
                return;
            }

            var model = new MessageConfirmModel { PhoneNumber = PhoneNumber };

            var result = await PerformDataRequestAsync(() => _accountService.ConfirmPhoneNumberAsync(model, CancellationToken.None));

            if (result)
            {
                var navigationParameters = new NavigationParameters
                {
                    { StringConstants.MessageConfirmModel, model }
                };

                await NavigationService.NavigateAsync(nameof(ConfirmPhonePage), navigationParameters);
            }
        }
    }
}
