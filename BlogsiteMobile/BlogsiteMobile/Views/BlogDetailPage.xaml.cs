using BlogsiteMobile.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace BlogsiteMobile.Views
{
    public partial class BlogDetailPage : ContentPage
    {
        public BlogDetailPage()
        {
            InitializeComponent();
            BindingContext = new BlogDetailViewModel();
        }
    }
}