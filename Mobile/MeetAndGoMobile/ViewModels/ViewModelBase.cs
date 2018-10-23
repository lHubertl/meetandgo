using MeetAndGoMobile.Infrastructure.UserControls.ViewModels;
using Prism.Mvvm;
using Prism.Navigation;

namespace MeetAndGoMobile.ViewModels
{
    public class ViewModelBase : BindableBase, INavigationAware, IDestructible
    {
        protected INavigationService NavigationService { get; private set; }

        private NavigationBarViewModel _navigationBar;
        public NavigationBarViewModel NavigationBar
        {
            get { return _navigationBar; }
            set { SetProperty(ref _navigationBar, value); }
        }

        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatingTo(INavigationParameters parameters)
        {

        }

        public virtual void Destroy()
        {

        }
    }
}
