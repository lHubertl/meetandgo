using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Windows.Input;
using Prism.Modularity;
using Prism.Navigation;

namespace MeetAndGoMobile.Modules.LoginModule.ViewModels
{
    public class LoginPageViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;
        private readonly IModuleManager _moduleManager;

        public ICommand SignInCommand { get; set; } 

        public LoginPageViewModel(INavigationService navigationService, IModuleManager moduleManager)
        {
            _navigationService = navigationService;
            _moduleManager = moduleManager;
            SignInCommand = new DelegateCommand(ExecuteSignInCommand);
        }

        private async void ExecuteSignInCommand()
        {
            _moduleManager.LoadModule("MeetingModule");
            await _navigationService.NavigateAsync(new Uri("app:///SmartNavigationPage/MeetingPage", UriKind.Absolute));
        }
    }
}
