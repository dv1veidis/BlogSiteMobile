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
    public partial class UserBlogDetailPage : ContentPage
    {
        public UserBlogDetailPage()
        {
            InitializeComponent();
            BindingContext = new BlogDetailViewModel();
        }
    }
}