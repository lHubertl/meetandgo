﻿using System;
using System.Threading.Tasks;
using Google.Maps;
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
            if (!MapControl.MyLocationEnabled)
            {
                MapControl.MyLocationEnabled = await CheckPermission();
            }

            if (MapControl.MyLocationEnabled)
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
                    MapControl.InitialCameraUpdate =
                        CameraUpdateFactory.NewPositionZoom(new Position(location.Latitude, location.Longitude), 14d);
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
