using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MeetAndGoMobile.UserControls.SelectLocation
{
    public class LocationList : ObservableCollection<LocationViewModel>
    {
        public string Title { get; set; }
        public string ImageSource { get; set; }

        public LocationList(string title, string imageSource)
        {
            Title = title;
            ImageSource = imageSource;
            All = Items;
        }

        public IList<LocationViewModel> All { get; }
    }
}
