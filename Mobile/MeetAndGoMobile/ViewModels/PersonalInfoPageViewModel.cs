using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using MeetAndGo.Shared.BusinessLogic.Responses;
using MeetAndGo.Shared.Managers;
using MeetAndGo.Shared.Models;
using MeetAndGoMobile.Infrastructure.Commands;
using MeetAndGoMobile.Infrastructure.DependencyServices;
using MeetAndGoMobile.Infrastructure.Resources;
using MeetAndGoMobile.Services;
using Prism.Ioc;
using Prism.Navigation;
using Xamarin.Forms;

namespace MeetAndGoMobile.ViewModels
{
    public class PersonalInfoPageViewModel : ViewModelBase
    {
        private readonly IAccountService _accountService;
        private MemoryStream _imageStream;
        private string _imageName;
        private string _imageFormat;
        private string _userPhoneNumber;

        private ImageSource _profileImageSource;
        public ImageSource ProfileImageSource
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
                if (!string.IsNullOrEmpty(userModel.CompressedPhotoUrl))
                {
                    SetProfileImageFromUri(userModel.CompressedPhotoUrl);
                }

                Name = userModel.FirstName;
                Email = userModel.Email;
                BirthdayDate = userModel.DateOfBirth;
                _userPhoneNumber = userModel.PhoneNumber;
            }
        }

        private async Task ExecuteSaveChangesCommand()
        {
            var validationManager = ValidationManager.Create();
            if (!validationManager
                .Validate(() => !string.IsNullOrWhiteSpace(Name), Strings.V_UserName)
                .ValidateEmail(Email, Strings.V_Email)
                .IsValid)
            {
                await UserNotificationAsync(validationManager.ToString(), Strings.Validation);
                return;
            }

            var userModel = new UserModel
            {
                FirstName = Name,
                Email = Email,
                DateOfBirth = BirthdayDate
            };

            var uploadImageTask = PerformDataRequestAsync(async() =>
            {
                if (_imageStream != null)
                {
                    return await _accountService.UploadUserPhotoAsync(
                        _userPhoneNumber,
                        _imageName,
                        _imageFormat,
                        _imageStream,
                        CancellationToken.None);
                }

                return new Response();
            });

            var updateUserInfoTask = PerformDataRequestAsync(() => _accountService.UpdateUserInfoAsync(userModel, CancellationToken.None));

            var success = await Task.WhenAll(updateUserInfoTask, uploadImageTask);
            if (success.All(x => x))
            {
                await NavigationService.GoBackAsync();
            }
        }

        private async Task ExecutePickImageCommand()
        {
            var imageFile = await DependencyService.Get<IPicturePicker>().PickImageAsync();

            if (imageFile.stream != null)
            {
                var memoryStream = new MemoryStream();
                await imageFile.stream.CopyToAsync(memoryStream);
                memoryStream.Position = 0;
                ProfileImageSource = ImageSource.FromStream(() => new MemoryStream(memoryStream.ToArray()));

                _imageStream = memoryStream;
                _imageFormat = imageFile.format;
                _imageName = imageFile.name;
            }
        }

        private void SetProfileImageFromUri(string uri)
        {
            var uriImageSource = new UriImageSource
            {
                Uri = new Uri(uri),
                CacheValidity = TimeSpan.Zero,
                CachingEnabled = false
            };
            
            ProfileImageSource = new StreamImageSource { Stream = async token => await uriImageSource.GetStreamAsync(token) };
        }
    }
}
