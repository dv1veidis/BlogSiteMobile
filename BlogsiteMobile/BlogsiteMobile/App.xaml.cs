using BlogsiteMobile.Models;
using BlogsiteMobile.Services;
using BlogsiteMobile.Views;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BlogsiteMobile
{
    public partial class App : Application
    {
        private static UserRepository user;

        public static UserRepository UserRepository
        {
            get
            {
                    if (user == null)
                    {
                        user = new
                        UserRepository(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "User.db3"));
                    }
                    return user;
            }
        }
        private static BlogPostRepository blogPost;

        public static BlogPostRepository blogPostRepository
        {
            get
            {
                if (blogPostRepository == null)
                {
                    blogPost = new
                    BlogPostRepository(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "BlogPosts.db3"));
                }
                return blogPost;
            }
        }
        public App()
        {
            if (App.Current.Properties.ContainsKey("IsLoggedIn"))
            {

                if ((bool)App.Current.Properties["IsLoggedIn"])
                {
                    MainPage = new AppShell();
                }
                else
                {
                    MainPage = new LoginPage();
                }
            }
            else
            {
                App.Current.Properties.Add("IsLoggedIn", false);
                MainPage = new NavigationPage(new LoginPage());
            }

            App.Current.SavePropertiesAsync();
            InitializeComponent();
            DependencyService.Register<MockDataStore>();
            DependencyService.Register<BlogPostRepository>();
            //MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
