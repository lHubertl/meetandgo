using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using MeetAndGoMobile.Infrastructure.Constants;
using MeetAndGoMobile.Infrastructure.Extensions;
using Xamarin.Forms;

namespace MeetAndGoMobile.Infrastructure.Controls
{

    public class RepeaterView : ContentView
    {
        private readonly StackLayout _itemsStack = new StackLayout { Spacing = 0 };
        private ActivityIndicator _activityIndicator;
        private View _footerView;
        private View _headerView;
        private View _emptySourceView;

        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.Create(
                nameof(ItemTemplate),
                typeof(DataTemplate),
                typeof(RepeaterView),
                default(DataTemplate));

        public static readonly BindableProperty HeaderTemplateProperty =
            BindableProperty.Create(
                nameof(HeaderTemplate),
                typeof(DataTemplate),
                typeof(RepeaterView),
                default(DataTemplate));

        public static readonly BindableProperty FooterTemplateProperty =
            BindableProperty.Create(
                nameof(FooterTemplate),
                typeof(DataTemplate),
                typeof(RepeaterView),
                default(DataTemplate));

        public static readonly BindableProperty SeparatorTemplateProperty =
            BindableProperty.Create(
                nameof(SeparatorTemplate),
                typeof(DataTemplate),
                typeof(RepeaterView),
                default(DataTemplate));

        public static readonly BindableProperty EmptyTextTemplateProperty =
            BindableProperty.Create(
                nameof(EmptyTextTemplate),
                typeof(DataTemplate),
                typeof(RepeaterView),
                default(DataTemplate));

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(
                nameof(ItemsSource),
                typeof(ICollection),
                typeof(RepeaterView),
                propertyChanged: ItemsSourcePropertyChanged);

        public static readonly BindableProperty EmptyTextProperty =
            BindableProperty.Create(
                nameof(EmptyText),
                typeof(string),
                typeof(RepeaterView),
                string.Empty);

        public static readonly BindableProperty SelectedItemCommandProperty =
            BindableProperty.Create(
                nameof(SelectedItemCommand),
                typeof(ICommand),
                typeof(RepeaterView),
                default(ICommand));
        
        public static readonly BindableProperty IsLoadIndicatorShownProperty = BindableProperty.Create(
            nameof(IsLoadIndicatorShown),
            typeof(bool),
            typeof(RepeaterView),
            false,
            propertyChanged: IsLoadIndicatorShownPropertyChanged);

        public static readonly BindableProperty ContainerPaddingProperty = BindableProperty.Create(
            nameof(ContainerPadding),
            typeof(Thickness),
            typeof(RepeaterView),
            new Thickness(0));
        
        public ICollection ItemsSource
        {
            get => (ICollection)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }

        public DataTemplate HeaderTemplate
        {
            get => (DataTemplate)GetValue(HeaderTemplateProperty);
            set => SetValue(HeaderTemplateProperty, value);
        }

        public DataTemplate FooterTemplate
        {
            get => (DataTemplate)GetValue(FooterTemplateProperty);
            set => SetValue(FooterTemplateProperty, value);
        }

        public DataTemplate SeparatorTemplate
        {
            get => (DataTemplate)GetValue(SeparatorTemplateProperty);
            set => SetValue(SeparatorTemplateProperty, value);
        }

        public DataTemplate EmptyTextTemplate
        {
            get => (DataTemplate)GetValue(EmptyTextTemplateProperty);
            set => SetValue(EmptyTextTemplateProperty, value);
        }

        public string EmptyText
        {
            get => (string)GetValue(EmptyTextProperty);
            set => SetValue(EmptyTextProperty, value);
        }

        public ICommand SelectedItemCommand
        {
            get => (ICommand)GetValue(SelectedItemCommandProperty);
            set => SetValue(SelectedItemCommandProperty, value);
        }
        
        public bool IsLoadIndicatorShown
        {
            get => (bool)GetValue(IsLoadIndicatorShownProperty);
            set => SetValue(IsLoadIndicatorShownProperty, value);
        }

        public Thickness ContainerPadding
        {
            get => (Thickness)GetValue(ContainerPaddingProperty);
            set => SetValue(ContainerPaddingProperty, value);
        }

        public bool ShowSeparator { get; set; } = true;

        public bool AllowScroll { get; set; } = true;

        public RepeaterView()
        {
            _itemsStack.BindingContext = BindingContext;
            _itemsStack.SetBinding(PaddingProperty, nameof(ContainerPadding));
            
            if (AllowScroll)
            {
                var scrollView = GetScrollView();
                scrollView.Content = _itemsStack;
                Content = scrollView;
            }
            else
            {
                Content = _itemsStack;
            }
        }
        
        private static void ItemsSourcePropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (!(bindable is RepeaterView control))
            {
                return;
            }

            // any way if items source collection changed then clear all
            var repeater = control._itemsStack;
            repeater.Children.Clear();
            
            if (!(newvalue is IList itemsSource))
            {
                return;
            }

            // Set header

            var headerTemplate = control.HeaderTemplate;
            if (headerTemplate != null)
            {
                control._headerView = headerTemplate.GetViewFromTemplate(control.BindingContext);
                repeater.Children.Add(control._headerView);
            }

            // Set Items

            if (itemsSource.Count == 0)
            {
                var view = control.GetEmptyItemSourceView();
                if (view != null)
                {
                    repeater.Children.Add(view);
                }
            }
            else
            {
                var dataTemplate = control.ItemTemplate;

                if (dataTemplate != null)
                {
                    control.InitializeItemsContent(itemsSource, dataTemplate);
                }

                // Set footer

                var footerTemplate = control.FooterTemplate;
                if (footerTemplate != null)
                {
                    control._footerView = footerTemplate.GetViewFromTemplate(control.BindingContext);
                    repeater.Children.Add(control._footerView);
                }
            }

            if (itemsSource is INotifyCollectionChanged itemsSourceObservableCollection)
            {
                itemsSourceObservableCollection.CollectionChanged += (o, e) => ItemsSourceOnCollectionChanged(control, e);
            }
        }

        private static void ItemsSourceOnCollectionChanged(RepeaterView control, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var view = control.GetEmptyItemSourceView();
                if (view != null)
                {
                    control.TryRemoveViewFromContent(view);
                }

                var dataTemplate = control.ItemTemplate;

                if (dataTemplate != null)
                {
                    control.TryRemoveViewFromContent(control._footerView);

                    control._itemsStack.Children.Add(control.GetSeparator());

                    control.InitializeItemsContent(e.NewItems, dataTemplate);

                    if (control._footerView != null)
                    {
                        control._itemsStack.Children.Add(control._footerView);
                    }
                }
            }

            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                var content = control._itemsStack.Children;

                var viewsToRemove = new List<View>();

                for (int i = 0; i < content.Count; i++)
                {
                    if (e.OldItems.Contains(content[i].BindingContext))
                    {
                        viewsToRemove.Add(content[i]);
                        if (control.ShowSeparator && i < content.Count - 1 )
                        {
                            viewsToRemove.Add(content[i + 1]);
                        }
                    }
                }
                viewsToRemove.ForEach(view => control._itemsStack.Children.Remove(view));
                viewsToRemove.Clear();
            }

            else if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                control._itemsStack.Children.Clear();
            }

            if (control.ItemsSource == null || control.ItemsSource.Count == 0)
            {
                control.TryRemoveViewFromContent(control._headerView);
                control.TryRemoveViewFromContent(control._footerView);
                var view = control.GetEmptyItemSourceView();
                if (view != null)
                {
                    control._itemsStack.Children.Add(view);
                }
            }
        }

        private void InitializeItemsContent(IList list, DataTemplate template)
        {
            for (int i = 0; i < list.Count; i++)
            {
                var item = list[i];

                var view = template.GetViewFromTemplate(item);

                if (SelectedItemCommand != null)
                {
                    var tapGestureRecognizer = new TapGestureRecognizer();

                    tapGestureRecognizer.Tapped += (sender, ev) => { SelectedItemCommand?.Execute(item); };

                    view.GestureRecognizers.Add(tapGestureRecognizer);
                }

                view.BindingContext = item;

                _itemsStack.Children.Add(view);

                if (ShowSeparator && i < list.Count - 1)
                {
                    _itemsStack.Children.Add(GetSeparator());
                }
            }
        }

        private View GetSeparator()
        {
            if (SeparatorTemplate != null)
            {
                return SeparatorTemplate.GetViewFromTemplate(BindingContext);
            }

            return new BoxView
            {
                BackgroundColor = Colors.Gray,
                HeightRequest = 1
            };
        }
        
        private static void IsLoadIndicatorShownPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is RepeaterView control))
            {
                return;
            }

            if (!(newValue is bool isActive))
            {
                return;
            }

            // Initialize loader
            if (control._activityIndicator == null)
            {
                control._activityIndicator = new ActivityIndicator
                {
                    IsRunning = true,
                    HeightRequest = 50,
                    Margin = 30,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.Start
                };
            }

            if (isActive)
            {
                control._itemsStack.Children.Add(control._activityIndicator);
            }
            else
            {
                control._itemsStack.Children.Remove(control._activityIndicator);
            }
        }

        private View GetEmptyItemSourceView()
        {
            if (_emptySourceView == null)
            {
                if (EmptyTextTemplate != null)
                {
                    _emptySourceView = EmptyTextTemplate.GetViewFromTemplate(BindingContext);
                }
                else if (!string.IsNullOrEmpty(EmptyText))
                {
                    _emptySourceView = new Label { Text = EmptyText };
                }
            }

            return _emptySourceView;
        }

        private void TryRemoveViewFromContent(View view)
        {
            if (view != null && _itemsStack.Children.Contains(view))
            {
                _itemsStack.Children.Remove(view);
            }
        }

        private ScrollView GetScrollView()
        {
            return new ScrollView {BindingContext = this};
        }
    }
}
