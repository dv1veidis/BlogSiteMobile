using BlogsiteMobile.ViewModels;
using BlogsiteMobile.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace BlogsiteMobile
{
    public partial class AppShell : Xamarin.Forms.Shell, INotifyPropertyChanged
    {
        public static readonly BindableProperty IsLoggedInProperty =
        BindableProperty.Create(nameof(IsLoggedIn), typeof(bool), typeof(AppShell), false);

        public bool IsLoggedIn
        {
            get { return (bool)GetValue(IsLoggedInProperty); }
            set { SetValue(IsLoggedInProperty, value); }
        }
        protected override async void OnNavigated(ShellNavigatedEventArgs args)
        {
            base.OnNavigated(args);

            // Check if the IsLoggedIn property has changed
            if (Application.Current.Properties.ContainsKey("IsLoggedIn"))
            {
                bool isLoggedIn = (bool)Application.Current.Properties["IsLoggedIn"];

            }
        }
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(BlogDetailPage), typeof(BlogDetailPage));
            Routing.RegisterRoute(nameof(NewBlogPage), typeof(NewBlogPage));
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));

        }
    }
}
