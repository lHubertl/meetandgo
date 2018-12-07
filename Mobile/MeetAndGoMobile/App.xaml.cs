﻿using System;
using System.Threading.Tasks;
using MeetAndGoMobile.Constants;
using MeetAndGoMobile.Infrastructure.BusinessLogic.Repositories;
using Prism;
using Prism.Ioc;
using MeetAndGoMobile.ViewModels;
using MeetAndGoMobile.Views;
using Xamarin.Forms.Xaml;
using MeetAndGoMobile.Infrastructure.Resources;
using MeetAndGoMobile.Services;
using MeetAndGoMobile.Services.FakeServices;
using Plugin.Multilingual;
using Xamarin.Essentials;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MeetAndGoMobile
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            // Set current device language to application
            Strings.Culture = CrossMultilingual.Current.CurrentCultureInfo;

            if (await CheckIfTokenExist())
            {
                // TODO: TO MAP PAGE
                await NavigationService.NavigateAsync($"{nameof(CustomNavigationPage)}/{nameof(SignUpPage)}");
            }
            else
            {
                await NavigationService.NavigateAsync($"{nameof(CustomNavigationPage)}/{nameof(SignUpPage)}");
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<CustomNavigationPage, CustomNavigationPageViewModel>();
            containerRegistry.RegisterForNavigation<CreateAccountPage, CreateAccountPageViewModel>();
            containerRegistry.RegisterForNavigation<ConfirmPhonePage, ConfirmPhonePageViewModel>();
            containerRegistry.RegisterForNavigation<SignUpPage, SignUpPageViewModel>();
            containerRegistry.RegisterForNavigation<SignInPage, SignInPageViewModel>();
            containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>();

            containerRegistry.Register<IAccountService, AccountService>();

            containerRegistry.RegisterSingleton<IDataRepository, DataRepository>();

            containerRegistry.RegisterInstance(Container);
        }

        private async Task<bool> CheckIfTokenExist()
        {
            try
            {
                var token = await SecureStorage.GetAsync(StringConstants.Token);
                if (!string.IsNullOrEmpty(token))
                {
                    var dataRepository = Container.Resolve<IDataRepository>();
                    dataRepository.Set(DataType.Token, token);
                    return true;
                }
            }
            catch (Exception e)
            {
                // TODO: log it
            }

            return false;
        }
    }
}
