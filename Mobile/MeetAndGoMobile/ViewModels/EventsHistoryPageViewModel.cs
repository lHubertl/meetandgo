using System.Collections.Generic;
using MeetAndGo.Shared.Models;
using Prism.Ioc;
using Prism.Navigation;

namespace MeetAndGoMobile.ViewModels
{
    public class EventsHistoryPageViewModel : ViewModelBase
    {
        private List<EventModel> _events;
        public List<EventModel> Events
        {
            get => _events;
            set => SetProperty(ref _events, value);
        }

        public EventsHistoryPageViewModel(INavigationService navigationService, IContainerProvider container) : base(navigationService, container)
        {

        }
    }
}
