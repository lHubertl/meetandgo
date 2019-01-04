using System.Collections.Generic;
using System.Threading;
using MeetAndGo.Shared.Models;
using MeetAndGoMobile.Services.Contracts;
using Prism.Ioc;
using Prism.Navigation;

namespace MeetAndGoMobile.ViewModels
{
    public class EventsHistoryPageViewModel : ViewModelBase
    {
        private readonly IEventService _eventService;

        private List<EventModel> _events;
        public List<EventModel> Events
        {
            get => _events;
            set => SetProperty(ref _events, value);
        }

        public EventsHistoryPageViewModel(
            INavigationService navigationService, 
            IContainerProvider container,
            IEventService eventService) : base(navigationService, container)
        {
            _eventService = eventService;
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            IsBusy = true;

            var eventModels = await PerformDataRequestAsync(() => _eventService.GetEventsHistoryAsync(CancellationToken.None));
            Events = eventModels;

            IsBusy = false;
        }
    }
}
