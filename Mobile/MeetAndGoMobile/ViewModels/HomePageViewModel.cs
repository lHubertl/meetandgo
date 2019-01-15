using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using MeetAndGoMobile.Infrastructure.Commands;
using MeetAndGoMobile.Services;
using MeetAndGoMobile.Services.Contracts;
using MeetAndGoMobile.UserControls.SelectLocation;
using Prism.Ioc;
using Prism.Navigation;
using Xamarin.Forms;

namespace MeetAndGoMobile.ViewModels
{
	public class HomePageViewModel : ViewModelBase
	{
        private readonly IMasterPageService _masterPageService;

        private ObservableCollection<LocationList> _startLocations;
        public ObservableCollection<LocationList> StartLocations
        {
            get => _startLocations;
            set => SetProperty(ref _startLocations, value);
        }

        private ObservableCollection<LocationList> _endLocations;
        public ObservableCollection<LocationList> EndLocations
        {
            get => _endLocations;
            set => SetProperty(ref _endLocations, value);
        }
        
        public ICommand ToggleMasterCommand => new Command(() => _masterPageService.ToggleMaster(ToggleActions.ToggleMenu));

        public ICommand StartLocationTextChangedCommand => new Command<string>(ExecuteStartLocationTextChangedCommand);

        public ICommand EndLocationTextChangedCommand => new Command<string>(ExecuteEndLocationTextChangedCommand);

        public ICommand SelectedStartLocationCommand => new Command<LocationViewModel>(ExecuteSelectedStartLocationCommand);

        public ICommand SelectedEndLocationCommand => new Command<LocationViewModel>(ExecuteSelectedEndLocationCommand);
        
        public ICommand CreateCommand => new SingleExecutionCommand(ExecuteCreateCommand);
        public ICommand SearchCommand => new SingleExecutionCommand(ExecuteSearchCommand);

        public HomePageViewModel(INavigationService navigationService, IContainerProvider container, IMasterPageService masterPageService) 
            : base(navigationService, container)
        {
            _masterPageService = masterPageService;

            EndLocations = StartLocations = new ObservableCollection<LocationList>
            {
                new LocationList("Saved Places", "saved_place.png")
                {
                    new LocationViewModel
                    {
                        ImageSource = "home.png",
                        Text = "Home"
                    },
                    new LocationViewModel
                    {
                        ImageSource = "work.png",
                        Text = "Work"
                    }
                },
                new LocationList("Shown on Map", "location_gray.png")
                {
                    new LocationViewModel
                    {
                        ImageSource = "time.png",
                        Text = "Park Street, 45 B"
                    },
                    new LocationViewModel
                    {
                        ImageSource = "time.png",
                        Text = "Catherine's Square, 49/2"
                    }
                }
            };
        }

        private async void ExecuteStartLocationTextChangedCommand(string text)
        {
            GoogleService test = new GoogleService();
            var s = await test.NearbyAsync(40.741895, -73.989308);
        }

        private void ExecuteEndLocationTextChangedCommand(string text)
        {

        }

        private void ExecuteSelectedStartLocationCommand(LocationViewModel location)
        {

        }

        private void ExecuteSelectedEndLocationCommand(LocationViewModel location)
        {

        }

        private async Task ExecuteCreateCommand()
        {

        }

        private async Task ExecuteSearchCommand()
        {

        }
    }
}
