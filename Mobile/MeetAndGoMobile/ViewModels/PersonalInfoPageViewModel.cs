using MeetAndGo.Shared.BusinessLogic.Responses;
using MeetAndGo.Shared.Managers;
using MeetAndGo.Shared.Models;
using MeetAndGoMobile.Infrastructure.Commands;
using MeetAndGoMobile.Infrastructure.Resources;
using MeetAndGoMobile.Services;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Ioc;
using Prism.Navigation;
using Prism.Services;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MeetAndGoMobile.ViewModels
{
    public class PersonalInfoPageViewModel : ViewModelBase
    {
        private readonly IPageDialogService _pageDialogService;
        private readonly IMasterPageService _masterPageService;
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
            IAccountService accountService,
            IPageDialogService pageDialogService,
            IMasterPageService masterPageService)
            : base(navigationService, container)
        {
            _accountService = accountService;
            _pageDialogService = pageDialogService;
            _masterPageService = masterPageService;
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

            var uploadImageTask = PerformDataRequestAsync(async () =>
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
                _masterPageService.ToggleMaster(ToggleActions.UserUpdated);
                await NavigationService.GoBackAsync();
            }
        }

        private async Task ExecutePickImageCommand()
        {
            if (!CrossMedia.IsSupported)
            {
                await UserNotificationAsync(Strings.NotSupported, Strings.Error);
                return;
            }

            var mediaFile = await PickMediaFile();
            
            if (mediaFile == null)
            {
                // User canceled
                return;
            }

            var fileNameAndType = mediaFile.Path.Split('/').LastOrDefault()?.Split('.');
            var fileName = fileNameAndType?.FirstOrDefault();
            var fileType = fileNameAndType?.Length > 1 ? fileNameAndType.LastOrDefault() : default(string);

            var memoryStream = new MemoryStream();
            await mediaFile.GetStream().CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            ProfileImageSource = ImageSource.FromStream(() => new MemoryStream(memoryStream.ToArray()));

            _imageStream = memoryStream;
            _imageFormat = fileType;
            _imageName = fileName;
        }

        private async Task<MediaFile> PickMediaFile()
        {
            Task<MediaFile> mediaFileTask = null;

            var cameraOptions = new StoreCameraMediaOptions
            {
                DefaultCamera = CameraDevice.Front,
                AllowCropping = true
            };

            var storageOptions = new PickMediaOptions
            {
                CompressionQuality = 70
            };

            var cameraAction = ActionSheetButton.CreateButton(
                Strings.Camera,
                () => mediaFileTask = CrossMedia.Current.TakePhotoAsync(cameraOptions));

            var storageAction = ActionSheetButton.CreateButton(Strings.Storage,
                () => mediaFileTask = CrossMedia.Current.PickPhotoAsync(storageOptions));

            await _pageDialogService.DisplayActionSheetAsync(Strings.PickImageOption, cameraAction, storageAction);

            try
            {
                if (mediaFileTask != null)
                {
                    return await mediaFileTask;
                }
            }
            catch (Exception e)
            {
                await UserNotificationAsync(e.Message, Strings.Error);
            }

            return null;
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
