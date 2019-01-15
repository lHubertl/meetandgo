using System;
using System.Collections.ObjectModel;
using MeetAndGoMobile.UserControls.SelectLocation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeetAndGoMobile.UserControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectLocationUserControl : Grid
    {
        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
            nameof(Placeholder),
            typeof(string),
            typeof(SelectLocationUserControl));

        public static readonly BindableProperty TextChangedCommandProperty = BindableProperty.Create(
            nameof(TextChangedCommand),
            typeof(Command<string>),
            typeof(SelectLocationUserControl));

        public static readonly BindableProperty SelectedLocationCommandProperty = BindableProperty.Create(
            nameof(SelectedLocationCommand),
            typeof(Command<LocationViewModel>),
            typeof(SelectLocationUserControl));

        public static readonly BindableProperty LocationsProperty = BindableProperty.Create(
            nameof(Locations),
            typeof(ObservableCollection<LocationList>),
            typeof(SelectLocationUserControl));
        
        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        public Command<string> TextChangedCommand
        {
            get => (Command<string>)GetValue(TextChangedCommandProperty);
            set => SetValue(TextChangedCommandProperty, value);
        }

        public Command<LocationViewModel> SelectedLocationCommand
        {
            get => (Command<LocationViewModel>)GetValue(SelectedLocationCommandProperty);
            set => SetValue(SelectedLocationCommandProperty, value);
        }

        public ObservableCollection<LocationList> Locations
        {
            get => (ObservableCollection<LocationList>)GetValue(LocationsProperty);
            set => SetValue(LocationsProperty, value);
        }
        
        public SelectLocationUserControl()
        {
            InitializeComponent();

            LocationEntry.TextChanged += LocationEntryOnTextChanged;
            LocationEntry.Unfocused += (sender, args) => { ListViewPart.IsVisible = false; };
            LocationEntry.Focused += (sender, args) => { ListViewPart.IsVisible = true; };
        }

        private void LocationEntryOnTextChanged(object sender, TextChangedEventArgs e)
        {
            TextChangedCommand?.Execute(e.NewTextValue);
        }

        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            LocationEntry.Focus();
        }
    }
}