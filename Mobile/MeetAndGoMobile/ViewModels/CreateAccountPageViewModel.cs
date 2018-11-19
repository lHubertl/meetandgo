using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using MeetAndGo.Shared.Managers;
using MeetAndGo.Shared.Models.Authorization;
using MeetAndGoMobile.Constants;
using MeetAndGoMobile.Infrastructure.Commands;
using MeetAndGoMobile.Infrastructure.Resources;
using MeetAndGoMobile.Services;
using Prism.Navigation;
using Prism.Services;

namespace MeetAndGoMobile.ViewModels
{
	public class CreateAccountPageViewModel : ViewModelBase
	{
	    private readonly IAccountService _accountService;
        private MessageConfirmModel _messageConfirmModel;

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

	    public CreateAccountPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IAccountService accountService) 
	        : base(navigationService, dialogService)
	    {
	        _accountService = accountService;
        }

	    public override void OnNavigatingTo(INavigationParameters parameters)
	    {
	        base.OnNavigatingTo(parameters);

	        if (parameters != null)
	        {
	            if (parameters.TryGetValue(NavParamConstants.MessageConfirmModel, out MessageConfirmModel model))
	            {
	                _messageConfirmModel = model;
	            }
	        }
	    }

        private async Task ExecuteCreateAccountCommand()
	    {
	        var validationManager = ValidationManager.Create();
	        if (!validationManager
	            .Validate(() => !string.IsNullOrWhiteSpace(YourName), Strings.V_UserName)
	            .ValidatePassword(Password, Strings.V_Password)
	            .Validate(() => Password == ConfirmedPassword, Strings.V_ConfirmPassword)
	            .IsValid)
	        {
	            await UserNotificationAsync(validationManager.ToString(), Strings.Validation);
	            return;
            }

	        var model = new RegisterModel
	        {
	            Password = Password,
	            ConfirmPassword = ConfirmedPassword,
	            PhoneNumber = _messageConfirmModel.PhoneNumber,
	            FirstName = YourName,
	            RememberMe = true
	        };

	        var resultToken = await PerformDataRequestAsync(() => _accountService.RegisterAsync(model, CancellationToken.None));
	        if (!string.IsNullOrEmpty(resultToken))
	        {
                
	        }
        }
    }
}
