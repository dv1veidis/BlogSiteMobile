using BlogsiteMobile.Models;
using BlogsiteMobile.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BlogsiteMobile.Views
{
    public partial class NewBlogPage : ContentPage
    {
        public BlogPost BlogPost{ get; set; }
        public NewBlogPage()
        {
            InitializeComponent();
            BindingContext = new NewBlogViewModel();
        }
    }
}