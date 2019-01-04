using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using MeetAndGoMobile.Infrastructure.Commands;
using MeetAndGoMobile.Services;
using MeetAndGoMobile.UserControls.ViewModels;
using Prism.Ioc;
using Prism.Navigation;
using Xamarin.Forms;

namespace MeetAndGoMobile.ViewModels
{
	public class HomePageViewModel : ViewModelBase
	{
        private readonly IMasterPageService _masterPageService;

        private ObservableCollection<LocationUserControlViewModel> _startLocations;
        public ObservableCollection<LocationUserControlViewModel> StartLocations
        {
            get => _startLocations;
            set => SetProperty(ref _startLocations, value);
        }

        private ObservableCollection<LocationUserControlViewModel> _endLocations;
        public ObservableCollection<LocationUserControlViewModel> EndLocations
        {
            get => _endLocations;
            set => SetProperty(ref _endLocations, value);
        }
        
        public ICommand ToggleMasterCommand => new Command(() => _masterPageService.ToggleMaster(ToggleActions.ToggleMenu));

        public ICommand StartLocationTextChangedCommand => new Command<string>(ExecuteStartLocationTextChangedCommand);

        public ICommand EndLocationTextChangedCommand => new Command<string>(ExecuteEndLocationTextChangedCommand);

        public ICommand SelectedStartLocationCommand => new Command<LocationUserControlViewModel>(ExecuteSelectedStartLocationCommand);

        public ICommand SelectedEndLocationCommand => new Command<LocationUserControlViewModel>(ExecuteSelectedEndLocationCommand);
        
        public ICommand CreateCommand => new SingleExecutionCommand(ExecuteCreateCommand);
        public ICommand SearchCommand => new SingleExecutionCommand(ExecuteSearchCommand);

        public HomePageViewModel(INavigationService navigationService, IContainerProvider container, IMasterPageService masterPageService) 
            : base(navigationService, container)
        {
            _masterPageService = masterPageService;
            
            StartLocations = new ObservableCollection<LocationUserControlViewModel>
            {
                new LocationUserControlViewModel
                {
                    ImageSource = "pen.png",
                    PlaceName = "lol"
                },
                new LocationUserControlViewModel
                {
                    PlaceName = "lol"
                }
            };

            EndLocations = new ObservableCollection<LocationUserControlViewModel>
            {
                new LocationUserControlViewModel
                {
                    ImageSource = "pen.png",
                    PlaceName = "lo222SDSDASDl"
                },
                new LocationUserControlViewModel
                {
                    ImageSource = "pen.png",
                    PlaceName = "lo222l"
                }
            };
        }

        private void ExecuteStartLocationTextChangedCommand(string text)
        {

        }

        private void ExecuteEndLocationTextChangedCommand(string text)
        {

        }

        private void ExecuteSelectedStartLocationCommand(LocationUserControlViewModel location)
        {

        }

        private void ExecuteSelectedEndLocationCommand(LocationUserControlViewModel location)
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
