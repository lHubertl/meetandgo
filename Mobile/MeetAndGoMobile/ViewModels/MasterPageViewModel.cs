using MeetAndGo.Shared.Enums;
using MeetAndGo.Shared.Models;
using MeetAndGoMobile.Infrastructure.Commands;
using MeetAndGoMobile.Infrastructure.Resources;
using Prism.Ioc;
using Prism.Navigation;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;

namespace MeetAndGoMobile.ViewModels
{
    public class MasterPageViewModel : ViewModelBase
    {
        public string Version => $"v{VersionTracking.CurrentVersion}";

        private string _userName;
        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }

        private string _userImageSource;
        public string UserImageSource
        {
            get => _userImageSource;
            set => SetProperty(ref _userImageSource, value);
        }

        private int _memberRating = 1;
        public int MemberRating
        {
            get => _memberRating;
            set => SetProperty(ref _memberRating, value);
        }

        private int _organizerRating = 1;
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

        public ICommand NavigateCommand => new SingleExecutionCommand(ExecuteNavigateCommand);

        public MasterPageViewModel(INavigationService navigationService, IContainerProvider container) : base(navigationService, container)
        {
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            // TODO: SET REAL DATA
            var userModel = new UserModel
            {
                CompressedPhotoUrl = @"https://cdn1.iconfinder.com/data/icons/hawcons/32/698838-icon-111-search-128.png",
                FirstName = "Valerii Sovytskyi",
                LastName = null,
                Votes = new List<VoteModel>
                {
                    new VoteModel { Rating = 2, RatingType = UserStatus.Member },
                    new VoteModel { Rating = 3, RatingType = UserStatus.Member },
                    new VoteModel { Rating = 5, RatingType = UserStatus.Member }
                },
                OrganizerRating = 4,
                MemberRating = 2
            };

            SetUpUserInfo(userModel);
        }

        private async Task ExecuteNavigateCommand(object pageNameObj)
        {
            if(pageNameObj is string pageName)
            {
                await NavigationService.NavigateAsync(pageName);
            }
        }

        private void SetUpUserInfo(UserModel userModel)
        {
            if(userModel is null)
            {
                return;
            }

            UserName = string.Join(" ", userModel.FirstName, userModel.LastName);
            UserImageSource = userModel.CompressedPhotoUrl;
            
            MemberRating = (int)userModel.MemberRating;
            OrganizerRating = (int)userModel.OrganizerRating;

            var asOrganizerVoteCount = userModel?.Votes.Where(x => x.RatingType == UserStatus.Organizer).Count();
            var eventsText = asOrganizerVoteCount == 1 ? Strings.L_event : Strings.L_events;
            OrganizerVotesText = $"({asOrganizerVoteCount} {eventsText})";
        }
    }
}
