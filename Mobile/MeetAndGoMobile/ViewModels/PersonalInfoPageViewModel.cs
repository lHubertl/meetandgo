using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using MeetAndGoMobile.Infrastructure.Commands;
using MeetAndGoMobile.Infrastructure.DependencyServices;
using MeetAndGoMobile.Services;
using Prism.Ioc;
using Prism.Navigation;
using Xamarin.Forms;

namespace MeetAndGoMobile.ViewModels
{
    public class PersonalInfoPageViewModel : ViewModelBase
    {
        private readonly IAccountService _accountService;

        private string _profileImageSource;
        public string ProfileImageSource
        {
            get => _profileImageSource;
            set => SetProperty(ref _profileImageSource, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private DateTime _birthdayDate;
        public DateTime BirthdayDate
        {
            get => _birthdayDate;
            set => SetProperty(ref _birthdayDate, value);
        }

        public ICommand SaveChangesCommand => new SingleExecutionCommand(ExecuteSaveChangesCommand);

        public ICommand PickImageCommand => new SingleExecutionCommand(ExecutePickImageCommand);
        
        public PersonalInfoPageViewModel(
            INavigationService navigationService, 
            IContainerProvider container,
            IAccountService accountService)
            : base(navigationService, container)
        {
            _accountService = accountService;
        }

        public override async void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            var userModel = await PerformDataRequestAsync(() => _accountService.GetUserModelAsync(CancellationToken.None));
            if (userModel != null)
            {
                ProfileImageSource = userModel.CompressedPhotoUrl;
                Name = string.Join(" ", userModel.LastName, userModel.FirstName);
                Email = userModel.Email;
                BirthdayDate = userModel.DateOfBirth;
            }
        }

        private async Task ExecuteSaveChangesCommand()
        {

        }

        private async Task ExecutePickImageCommand()
        {
            var stream = await DependencyService.Get<IPicturePicker>().GetImageStreamAsync();
            
            //TODO: 
        }
    }
}
