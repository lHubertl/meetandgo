using System;
using System.Threading.Tasks;
using MeetAndGoMobile.Infrastructure.DependencyServices;
using Plugin.Permissions.Abstractions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace MeetAndGoMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            DependencyService.Get<IStatusBarController>()?.SetVisibility(false);

            Device.BeginInvokeOnMainThread(async () => await SetMyLocation());
        }

        private async Task SetMyLocation()
        {
            if (!mapControl.MyLocationEnabled)
            {
                mapControl.MyLocationEnabled = await CheckPermission();
            }

            if (mapControl.MyLocationEnabled)
            {
                await MoveCameraToUserPosition();
            }
        }

        private async Task<bool> CheckPermission()
        {
            try
            {
                var locationPermission =
                    await Plugin.Permissions.CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (locationPermission != PermissionStatus.Granted)
                {
                    var result =
                        await Plugin.Permissions.CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);

                    return result[Permission.Location] == PermissionStatus.Granted;
                }

                return true;
            }

            catch (TaskCanceledException e)
            {

            }

            return false;
        }

        private async Task MoveCameraToUserPosition()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    // For animate camera
                    // TODO: CONFIGURE THIS PARAMETER FOR COMFORTABLE ANIMATION
                    await Task.Delay(1000);
                    await mapControl.AnimateCamera(CameraUpdateFactory.NewPositionZoom(new Position(location.Latitude, location.Longitude), 14d));
                }
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
        }
    }
}
