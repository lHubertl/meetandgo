using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Internals;

namespace MeetAndGoMobile.Controls
{
    public class CustomMap : Map
    {
        public static readonly BindableProperty MatchedPinsProperty = BindableProperty.Create(
            nameof(MatchedPins),
            typeof(ObservableCollection<Pin>),
            typeof(CustomMap),
            propertyChanged: MatchedPinsPropertyChanged);

        public ObservableCollection<Pin> MatchedPins
        {
            get => (ObservableCollection<Pin>)GetValue(MatchedPinsProperty);
            set => SetValue(MatchedPinsProperty, value);
        }
        
        private static void MatchedPinsPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (!(bindable is CustomMap control))
            {
                return;
            }

            if (newvalue is ObservableCollection<Pin> pins)
            {
                pins.ForEach(pin => control.Pins.Add(pin));
                pins.CollectionChanged += control.Pins_CollectionChanged;
            }
        }

        private void MatchPins()
        {

        }

        private void Pins_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:

                    var newStartIndex = e.NewStartingIndex;

                    foreach (var newItem in e.NewItems)
                    {
                        Pins.Insert(newStartIndex++, newItem as Pin);
                    }

                    break;

                case NotifyCollectionChangedAction.Remove:

                    foreach (var oldItem in e.OldItems)
                    {
                        Pins.Remove(oldItem as Pin);
                    }
                    
                    break;

                case NotifyCollectionChangedAction.Move:

                    var moveItem = Pins[e.OldStartingIndex];

                    Pins.RemoveAt(e.OldStartingIndex);
                    Pins.Insert(e.NewStartingIndex, moveItem);

                    break;

                case NotifyCollectionChangedAction.Replace:

                    Pins.RemoveAt(e.OldStartingIndex);
                    Pins.Insert(e.NewStartingIndex, Pins[e.NewStartingIndex]);

                    break;

                case NotifyCollectionChangedAction.Reset:

                    Pins.Clear();

                    foreach (var item in e.NewItems)
                    {
                        Pins.Add(item as Pin);
                    }

                    break;
            }
        }
    }
}
