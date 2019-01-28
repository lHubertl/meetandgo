using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows.Input;
using Google.Maps;
using MeetAndGo.Shared.Models;
using MeetAndGoMobile.Constants;
using MeetAndGoMobile.Infrastructure.Resources;
using MeetAndGoMobile.Models;
using MeetAndGoMobile.Services.Contracts;
using Prism.Commands;
using Prism.Ioc;
using Prism.Navigation;
using Xamarin.Forms.Internals;

namespace MeetAndGoMobile.ViewModels
{
    public class LocationsPopupPageViewModel : ViewModelBase
    {
        private CancellationTokenSource _searchTokenSource = new CancellationTokenSource();
        private readonly IGoogleService _googleService;
        private List<LocationViewModel> _baseLocationViewModels;
        private LatLng _currentLocation;

        /// <summary>
        /// Contains all saved 
        /// </summary>
        private readonly LocationList _saved;
        private readonly LocationList _searched;

        private ObservableCollection<LocationList> _locations;
        public ObservableCollection<LocationList> Locations
        {
            get => _locations;
            set => SetProperty(ref _locations, value);
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }

        public ICommand SelectedLocationCommand => new DelegateCommand<LocationViewModel>(ExecuteSelectedLocationCommand);
        public ICommand SearchTextChangedCommand => new DelegateCommand(ExecuteSearchTextChangedCommand);

        public LocationsPopupPageViewModel(INavigationService navigationService, IContainerProvider container, IGoogleService googleService) : base(navigationService, container)
        {
            _googleService = googleService;

            _saved = new LocationList(Strings.SavedPlaces, "saved_place.png");
            _searched = new LocationList(Strings.Recently, "location_gray.png");
            Locations = new ObservableCollection<LocationList> {_saved, _searched};
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            if (parameters.TryGetValue(StringConstants.Location, out LatLng location))
            {
                _currentLocation = location;
            }

            if (parameters.TryGetValue(StringConstants.DefaultPoints, out List<PointModel> points))
            {
                _baseLocationViewModels = ToLocationList(points);
                
                // Fill saved and searched collection with default values
                _baseLocationViewModels.ForEach(item =>
                {
                    if (item.Type == PointType.Home || item.Type == PointType.Saved || item.Type == PointType.Work)
                    {
                        _saved.Add(item);
                    }
                    else
                    {
                        _searched.Add(item);
                    }
                });
            }
        }

        private void ExecuteSelectedLocationCommand(LocationViewModel location)
        {
            var parameters = new NavigationParameters
            {
                {StringConstants.Location, location}
            };

            NavigationService.GoBackAsync(parameters);
        }

        private async void ExecuteSearchTextChangedCommand()
        {
            var lowerSearchText = SearchText.ToLower();

            // Filter existing source
            var savedToRemove = _saved.Where(x => !x.Name.ToLower().Contains(lowerSearchText)).ToList();
            savedToRemove.ForEach(x => _saved.Remove(x));

            var searchedToRemove = _searched.Where(x => !x.Name.Contains(lowerSearchText)).ToList();
            searchedToRemove.ForEach(x => _searched.Remove(x));


            // Get filtered base source
            var filteredValues = _baseLocationViewModels.Where(x => x.Name.ToLower().Contains(lowerSearchText));

            filteredValues.ForEach(item =>
            {
                if (item.Type == PointType.Home || item.Type == PointType.Saved || item.Type == PointType.Work)
                {
                    var doesNotHaveThisItem = _saved.Any(x => !IsPointsEqual(item, x));
                    if (doesNotHaveThisItem)
                    {
                        _saved.Add(item);
                    }
                }
                else
                {
                    var doesNotHaveThisItem = _searched.Any(x => !IsPointsEqual(item, x));
                    if (doesNotHaveThisItem)
                    {
                        _searched.Add(item);
                    }
                }
            });

            if (lowerSearchText?.Length < 3)
            {
                return;
            }

            if (_searchTokenSource.IsCancellationRequested)
            {
                _searchTokenSource = new CancellationTokenSource();
            }

            //TODO add task cancellation

            var result = await _googleService.GetPointsByInputText(lowerSearchText, _currentLocation);
            if (result.Success)
            {
                var locations = result.Data.Select(place => new LocationViewModel
                {
                    Lat = place.Lat,
                    Long = place.Long,
                    Name = place.Name,
                    Type = place.Type
                });

                var ordered = locations.OrderBy(x => x.Name);
            }
        }

        private List<LocationList> GroupPointsToLocations(List<PointModel> points)
        {
            var ordered = points.OrderBy(x => x.Type);

            var savedList = new LocationList(Strings.SavedPlaces, "saved_place.png");
            var recentlyList = new LocationList(Strings.Recently, "location_gray.png");

            ordered.ForEach(place =>
            {
                var location = new LocationViewModel
                {
                    Lat = place.Lat,
                    Long = place.Long,
                    Name = place.Name,
                    Type = place.Type
                };

                switch (place.Type)
                {
                    case PointType.Home:
                        location.ImageSource = "home.png";
                        location.Name = Strings.Home;
                        savedList.Add(location);
                        break;
                    case PointType.Work:
                        location.ImageSource = "work.png";
                        location.Name = Strings.Work;
                        savedList.Add(location);
                        break;
                    case PointType.Saved:
                        location.ImageSource = "saved_place.png";
                        savedList.Add(location);
                        break;
                    case PointType.Recently:
                        location.ImageSource = "time.png";
                        recentlyList.Add(location);
                        break;
                    case PointType.Searched:
                        recentlyList.Add(location);
                        break;
                }
            });

            var result = new List<LocationList>
            {
                savedList,
                recentlyList
            };

            return result;
        }
        

        private void FilterLocations(string text)
        {

        }

        private bool IsPointsEqual(PointModel a, PointModel b)
        {
            return a.Name == b.Name && a.Lat == b.Lat && a.Long == b.Lat && a.Type == b.Type;
        }

        private List<LocationViewModel> ToLocationList(List<PointModel> points)
        {
            return points.Select(place =>
            {
                var location = new LocationViewModel
                {
                    Lat = place.Lat,
                    Long = place.Long,
                    Name = place.Name,
                    Type = place.Type
                };

                switch (place.Type)
                {
                    case PointType.Home:
                        location.ImageSource = "home.png";
                        location.Name = Strings.Home;
                        break;
                    case PointType.Work:
                        location.ImageSource = "work.png";
                        location.Name = Strings.Work;
                        break;
                    case PointType.Saved:
                        location.ImageSource = "saved_place.png";
                        break;
                    case PointType.Recently:
                        location.ImageSource = "time.png";
                        break;
                }

                return location;
            }).OrderBy(x => x.Name).ToList();
        }
    }
}
