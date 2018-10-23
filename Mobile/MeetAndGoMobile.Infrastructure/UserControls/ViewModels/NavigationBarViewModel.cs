using Prism.Mvvm;
using System.Windows.Input;

namespace MeetAndGoMobile.Infrastructure.UserControls.ViewModels
{
    public class NavigationBarViewModel : BindableBase
    {
        #region Public fields

        private bool _hasNavigationBar = true;
        public bool HasNavigationBar
        {
            get => _hasNavigationBar;
            set => SetProperty(ref _hasNavigationBar, value);
        }

        private string _leftText;
        public string LeftText
        {
            get => _leftText;
            set => SetProperty(ref _leftText, value);
        }

        private string _rightText;
        public string RightText
        {
            get => _rightText;
            set => SetProperty(ref _rightText, value);
        }

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string _leftImageSource;
        public string LeftImageSource
        {
            get => _leftImageSource;
            set => SetProperty(ref _leftImageSource, value);
        }

        private string _rightImageSource;
        public string RightImageSource
        {
            get => _rightImageSource;
            set => SetProperty(ref _rightImageSource, value);
        }

        private ICommand _leftTapCommand;
        public ICommand LeftTapCommand
        {
            get => _leftTapCommand;
            set => SetProperty(ref _leftTapCommand, value);
        }

        private ICommand _rightTapCommand;
        public ICommand RightTapCommand
        {
            get => _rightTapCommand;
            set => SetProperty(ref _rightTapCommand, value);
        }

        #endregion  Public fields
    }
}
