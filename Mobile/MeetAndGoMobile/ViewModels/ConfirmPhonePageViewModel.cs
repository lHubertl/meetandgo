using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using MeetAndGo.Shared.Managers;
using MeetAndGo.Shared.Models.Authorization;
using MeetAndGoMobile.Constants;
using MeetAndGoMobile.Infrastructure.Commands;
using MeetAndGoMobile.Infrastructure.Resources;
using MeetAndGoMobile.Services;
using MeetAndGoMobile.Views;
using Prism.Ioc;
using Prism.Navigation;

namespace MeetAndGoMobile.ViewModels
{
	public class ConfirmPhonePageViewModel : ViewModelBase
	{
        private readonly IAccountService _accountService;
	    private MessageConfirmModel _model;

        private string _confirmationCode;
        public string ConfirmationCode
        {
            get => _confirmationCode;
            set => SetProperty(ref _confirmationCode, value);
        }

	    public ICommand ContinueCommand => new SingleExecutionCommand(async () => await ExecuteContinueCommandAsync());

	    public ConfirmPhonePageViewModel(INavigationService navigationService, IContainerProvider container, IAccountService accountService) 
	        : base(navigationService, container)
	    {
	        _accountService = accountService;
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            if (parameters != null)
            {
                if (parameters.TryGetValue(StringConstants.MessageConfirmModel, out MessageConfirmModel model))
                {
                    _model = model;
                }
            }
        }

        private async Task ExecuteContinueCommandAsync()
	    {
	        var validationManager = ValidationManager.Create();
	        if (!validationManager.ValidateSmsCode(ConfirmationCode, Strings.V_ConfirmationCode).IsValid)
	        {
	            await UserNotificationAsync(validationManager.ToString(), Strings.Validation);
	            return;
            }

	        _model.Code = ConfirmationCode;
            
	        var result = await PerformDataRequestAsync(() => _accountService.ConfirmSmsCodeAsync(_model, CancellationToken.None));

	        if (result)
	        {
	            var navigationParameters = new NavigationParameters
	            {
	                { StringConstants.MessageConfirmModel, _model }
	            };

                await NavigationService.NavigateAsync(nameof(CreateAccountPage), navigationParameters);
	        }
	    }
    }
}
