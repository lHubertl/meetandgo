using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using MeetAndGoMobile.Infrastructure.Commands;
using Prism.Ioc;
using Prism.Navigation;

namespace MeetAndGoMobile.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
    {
        private List<string> _languages;
        public List<string> Languages
        {
            get => _languages;
            set => SetProperty(ref _languages, value);
        }

        private string _selectedLanguage;
        public string SelectedLanguage
        {
            get => _selectedLanguage;
            set => SetProperty(ref _selectedLanguage, value);
        }


        private List<string> _currencies;
        public List<string> Currencies
        {
            get => _currencies;
            set => SetProperty(ref _currencies, value);
        }

        private string _selectedCurrency;
        public string SelectedCurrency
        {
            get => _selectedCurrency;
            set => SetProperty(ref _selectedCurrency, value);
        }

        private bool _pushNotificationTurnedOn;
        public bool PushNotificationTurnedOn
        {
            get => _pushNotificationTurnedOn;
            set => SetProperty(ref _pushNotificationTurnedOn, value);
        }

        private string _phoneNumber;
        public string PhoneNumber
        {
            get => _phoneNumber;
            set => SetProperty(ref _phoneNumber, value);
        }

        private string _newPassword;
        public string NewPassword
        {
            get => _newPassword;
            set => SetProperty(ref _newPassword, value);
        }

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => SetProperty(ref _confirmPassword, value);
        }

        public ICommand SaveChangesCommand => new SingleExecutionCommand(ExecuteSaveChangesCommand);

        public SettingsPageViewModel(INavigationService navigationService, IContainerProvider container) : base(navigationService, container)
        {

        }

        private async Task ExecuteSaveChangesCommand()
        {

        }
    }
}
