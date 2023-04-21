using BlogsiteMobile.Models;
using BlogsiteMobile.ViewModels;
using BlogsiteMobile.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BlogsiteMobile.Views
{
    public partial class BlogPage : ContentPage
    {
        BlogViewModel _viewModel;

        public BlogPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new BlogViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}