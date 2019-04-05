using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using MeetAndGo.Shared.Managers;
using MeetAndGo.Shared.Models.Authorization;
using MeetAndGoMobile.Constants;
using MeetAndGoMobile.Infrastructure.BusinessLogic.Repositories;
using MeetAndGoMobile.Infrastructure.Commands;
using MeetAndGoMobile.Infrastructure.Resources;
using MeetAndGoMobile.Services.Contracts;
using MeetAndGoMobile.Views;
using Prism.Ioc;
using Prism.Navigation;
using Xamarin.Essentials;

namespace MeetAndGoMobile.ViewModels
{
	public class SignInPageViewModel : ViewModelBase
	{
	    private readonly IAccountService _accountService;
	    private readonly IDataRepository _dataRepository;


        private string _phoneNumber;
        public string PhoneNumber
        {
            get => _phoneNumber;
            set => SetProperty(ref _phoneNumber, value);
        }

        private string _password;
	    public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public ICommand SignInCommand => new SingleExecutionCommand(ExecuteSignInCommand);

	    public ICommand SignUpCommand => new SingleExecutionCommand(ExecuteSignUpCommand);

	    public ICommand TermCommand => new SingleExecutionCommand(async () => await ExecuteTermCommandAsync());

        public SignInPageViewModel(INavigationService navigationService, IContainerProvider container, IAccountService accountService, IDataRepository dataRepository) 
            : base(navigationService, container)
        {
            _dataRepository = dataRepository;
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

	    private async Task ExecuteSignInCommand()
	    {
	        var validation = ValidationManager.Create()
	            .ValidatePhoneNumber(PhoneNumber, Strings.V_PhoneNumber)
	            .ValidatePassword(Password, Strings.V_Password);

	        if (!validation.IsValid)
	        {
	            await UserNotificationAsync(validation.ToString(), Strings.Validation);
                return;
	        }

	        var loginModel = new LoginModel
	        {
	            PhoneNumber = PhoneNumber,
	            Password = Password,
	            RememberMe = true
	        };

	        var token = await PerformDataRequestAsync(() => _accountService.SignInAsync(loginModel, CancellationToken.None));
	        if (token != null)
	        {
	            _dataRepository.Set(DataType.Token, token);

                try
	            {
	                await SecureStorage.SetAsync(StringConstants.Token, token);
                }
	            catch (Exception ex)
	            {
                    // Possible that device doesn't support secure storage on device.
	                // TODO: LOG IT

                    // TODO: Rework it
                    await UserNotificationAsync(ex.Message, Strings.Warning);
                }
                
                await NavigationService.NavigateAsync(new Uri($"app:///{nameof(MasterPage)}/{nameof(CustomNavigationPage)}/{nameof(HomePage)}", UriKind.Absolute));
            }
        }

	    private Task ExecuteSignUpCommand()
	    {
	        var navParams = new NavigationParameters
	        {
	            { StringConstants.PhoneNumber, PhoneNumber }
	        };

	        return NavigationService.GoBackAsync(navParams);
        }

	    private async Task ExecuteTermCommandAsync()
	    {
	        await UserNotificationAsync(Strings.NotImplemented);
	    }
    }
}
