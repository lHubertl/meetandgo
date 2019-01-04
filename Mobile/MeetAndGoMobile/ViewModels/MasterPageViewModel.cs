using System;
using MeetAndGo.Shared.Enums;
using MeetAndGo.Shared.Models;
using MeetAndGoMobile.Infrastructure.Resources;
using Prism.Ioc;
using Prism.Navigation;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MeetAndGoMobile.Services.Contracts;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MeetAndGoMobile.ViewModels
{
    public class MasterPageViewModel : ViewModelBase
    {
        private readonly IMasterPageService _masterPageService;
        private readonly IAccountService _accountService;
        public string Version => $"v{VersionTracking.CurrentVersion}";

        private string _userName;
        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }

        private ImageSource _userImageSource;
        public ImageSource UserImageSource
        {
            get => _userImageSource;
            set => SetProperty(ref _userImageSource, value);
        }

        private int _memberRating = 0;
        public int MemberRating
        {
            get => _memberRating;
            set => SetProperty(ref _memberRating, value);
        }

        private int _organizerRating = 0;
        public int OrganizerRating
        {
            get => _organizerRating;
            set => SetProperty(ref _organizerRating, value);
        }

        private string _organizerVotesText;
        public string OrganizerVotesText
        {
            get => _organizerVotesText;
            set => SetProperty(ref _organizerVotesText, value);
        }
        
        public MasterPageViewModel(
            INavigationService navigationService, 
            IContainerProvider container,
            IAccountService accountService,
            IMasterPageService masterPageService) : base(navigationService, container)
        {
            _accountService = accountService;
            _masterPageService = masterPageService;
        }

        public override async void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            _masterPageService.Subscribe(ToggleListener);

            await UpdateUserModelAsync();
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);

            _masterPageService.Unsubscribe(ToggleListener);
        }

        private async void ToggleListener(ToggleActions action)
        {
            if (action == ToggleActions.UserUpdated)
            {
                await UpdateUserModelAsync();
            }
        }

        private void SetUpUserInfo(UserModel userModel)
        {
            if(userModel is null)
            {
                return;
            }

            UserName = string.Join(" ", userModel.FirstName, userModel.LastName);

            if (!string.IsNullOrEmpty(userModel.CompressedPhotoUrl))
            {
                SetProfileImageFromUri(userModel.CompressedPhotoUrl);
            }
            
            // TODO: TEST THIS CASE BECAUSE SERVER CAN NOT UPDATE THIS FIELDS
            MemberRating = (int)userModel.MemberRating;
            OrganizerRating = (int)userModel.OrganizerRating;

            var asOrganizerVoteCount = userModel.Votes.Count(x => x.RatingType == UserStatus.Organizer);
            var eventsText = asOrganizerVoteCount == 1 ? Strings.L_event : Strings.L_events;
            OrganizerVotesText = $"({asOrganizerVoteCount} {eventsText})";
        }

        private void SetProfileImageFromUri(string uri)
        {
            var uriImageSource = new UriImageSource
            {
                Uri = new Uri(uri),
                CacheValidity = TimeSpan.Zero,
                CachingEnabled = false
            };

            UserImageSource = new StreamImageSource { Stream = async token => await uriImageSource.GetStreamAsync(token) };
        }

        private async Task UpdateUserModelAsync()
        {
            var userModel = await PerformDataRequestAsync(() => _accountService.GetUserModelAsync(CancellationToken.None));

            if (userModel != null)
            {
                SetUpUserInfo(userModel);
            }
        }
    }
}
