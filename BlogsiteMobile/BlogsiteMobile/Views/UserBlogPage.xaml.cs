using BlogsiteMobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BlogsiteMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserBlogPage : ContentPage
    {
        UserBlogViewModel _viewModel;
        public UserBlogPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new UserBlogViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}