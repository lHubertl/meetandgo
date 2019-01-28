using System;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace MeetAndGoMobile.Views
{
    public partial class LocationsPopupPage : PopupPage
    {
        public LocationsPopupPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            LocationEntry.Focus();
        }
        
        private void EntryFrameTapped(object sender, EventArgs e)
        {
            LocationEntry.Focus();
        }

        private void ScrollView_OnScrolled(object sender, ScrolledEventArgs e)
        {
            LocationEntry.Unfocus();
        }
    }
}
