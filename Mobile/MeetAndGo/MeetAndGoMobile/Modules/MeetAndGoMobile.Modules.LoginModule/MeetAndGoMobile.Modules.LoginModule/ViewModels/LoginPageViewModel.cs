using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Windows.Input;
using Prism.Navigation;

namespace MeetAndGoMobile.Modules.LoginModule.ViewModels
{
    public class LoginPageViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;

        public ICommand SignInCommand { get; set; } 

        public LoginPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            SignInCommand = new DelegateCommand(ExecuteSignInCommand);
        }

        private async void ExecuteSignInCommand()
        {
            await _navigationService.NavigateAsync(new Uri("app:///SmartNavigationPage/MainPage", UriKind.Absolute));
        }
    }
}
