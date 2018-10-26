using Prism.Navigation;
using System.Windows.Input;
using Prism.Commands;
using System.Threading.Tasks;
using System;

namespace MeetAndGoMobile.ViewModels
{
    public class SignUpPageViewModel : ViewModelBase
    {
        private string _phoneNumberText;
        public string PhoneNumberText
        {
            get { return _phoneNumberText; }
            set { SetProperty(ref _phoneNumberText, value); }
        }
        
        public ICommand SignInCommand => new DelegateCommand(async () => await ExecuteSignInCommandAsync());
        
        public ICommand TermCommand => new DelegateCommand(async() => await ExecuteTermCommandAsync());

        public ICommand ContinueCommand => new DelegateCommand(async () => await ExecuteContinueCommandAsync());

        public SignUpPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
        }

        private Task ExecuteContinueCommandAsync()
        {
            throw new NotImplementedException();
        }

        private Task ExecuteSignInCommandAsync()
        {
            throw new NotImplementedException();
        }

        private Task ExecuteTermCommandAsync()
        {
            throw new NotImplementedException();
        }

    }
}
