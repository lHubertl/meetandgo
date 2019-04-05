using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Google.Maps;
using MeetAndGo.Shared.Models;
using MeetAndGoMobile.Constants;
using MeetAndGoMobile.Infrastructure.Commands;
using MeetAndGoMobile.Infrastructure.Resources;
using MeetAndGoMobile.Services;
using MeetAndGoMobile.Services.Contracts;
using MeetAndGoMobile.Models;
using MeetAndGoMobile.Views;
using Prism.Ioc;
using Prism.Navigation;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Location = Xamarin.Essentials.Location;

namespace MeetAndGoMobile.ViewModels
{
	public class HomePageViewModel : ViewModelBase
	{
        private readonly IMasterPageService _masterPageService;
        private List<PointModel> _defaultPoints;
        private bool _isNavigatedToEndLocation;
        private Location _currentLocation;

        private PointModel _startPoint;
        public PointModel StartPoint
        {
            get => _startPoint;
            set
            {
                SetProperty(ref _startPoint, value);
                InsertPin(value, 0);
            }
        }

        private PointModel _endPoint;
        public PointModel EndPoint
        {
            get => _endPoint;
            set
            {
                SetProperty(ref _endPoint, value);
                InsertPin(value, 1);
            }
        }

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

        private ObservableCollection<Pin> _pins;
        public ObservableCollection<Pin> Pins
        {
            get => _pins;
            set => SetProperty(ref _pins, value);
        }

        public ICommand ToggleMasterCommand => new Command(() => _masterPageService.ToggleMaster(ToggleActions.ToggleMenu));
        
        public ICommand CreateCommand => new SingleExecutionCommand(ExecuteCreateCommand);

        public ICommand SearchCommand => new SingleExecutionCommand(ExecuteSearchCommand);

        public ICommand NavigateToLocation => new SingleExecutionCommand<string>(ExecuteNavigateToLocation);
        
        public HomePageViewModel(INavigationService navigationService, IContainerProvider container, IMasterPageService masterPageService) 
            : base(navigationService, container)
        {
            _masterPageService = masterPageService;
        }

        public override async void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
            
            if (parameters.TryGetValue(StringConstants.Location, out LocationViewModel location))
            {
                // TODO: GET POINT
                if (!_isNavigatedToEndLocation)
                {
                    StartPoint = location;
                }
                else
                {
                    EndPoint = location;
                }
            }

            await Task.WhenAll(InitializeStartPoint(), GetDefaultPoints());
        }

        private async Task InitializeStartPoint()
        {
            _currentLocation = await GetCurrentLocationAsync();
            if (_currentLocation != null)
            {
                StartPoint = new PointModel
                {
                    Lat = _currentLocation.Latitude,
                    Long = _currentLocation.Longitude,
                    Name = Strings.CurrentLocation
                };

                InsertPin(StartPoint, 0);
            }
        }

        private async Task GetDefaultPoints()
        {
            // TODO: IMPLEMENT FROM SERVER
            _defaultPoints = new List<PointModel>
            {
                new PointModel { Name = "Home", Type = PointType.Home },
                new PointModel { Name = "Work", Type = PointType.Work},
                new PointModel { Name = "Lalala", Type = PointType.Recently}
            };
        }
        
        private async Task ExecuteCreateCommand()
        {

        }

        private async Task ExecuteSearchCommand()
        {

        }

        private Task ExecuteNavigateToLocation(string key)
        {
            _isNavigatedToEndLocation = key != "start";

            var parameters = new NavigationParameters();
            if (_currentLocation != null)
            {
                parameters.Add(StringConstants.Location, new LatLng(_currentLocation.Latitude, _currentLocation.Longitude));
            }
            if (_defaultPoints != null)
            {
                parameters.Add(StringConstants.DefaultPoints, _defaultPoints);

            }

            return NavigationService.NavigateAsync(nameof(LocationsPopupPage), parameters);
        }

        private async Task<Location> GetCurrentLocationAsync()
        {
            try
            {
                return await Geolocation.GetLocationAsync(new GeolocationRequest());
            }

            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }

            return null;
        }

        private void InsertPin(PointModel point, int index)
        {
            if (point == null)
            {
                return;
            }

            var pin = new Pin {Position = new Position(point.Lat, point.Long), Label = point.Name};

            if (Pins == null)
            {
                Pins = new ObservableCollection<Pin> { pin };
            }
            else
            {
                Pins.Insert(index, pin);
            }
        }
    }
}
