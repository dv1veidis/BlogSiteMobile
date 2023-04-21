using BlogsiteMobile.Models;
using BlogsiteMobile.Services;
using BlogsiteMobile.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BlogsiteMobile.ViewModels
{
    public class BlogViewModel : BaseViewModel
    {

        public ObservableCollection<BlogPost> BlogPostsView { get; }
        public Command LoadBlogPostsCommand { get; }
        public Command AddBlogPostCommand { get; }
        public Command<BlogPost> BlogPostTapped { get; }

        private BlogPost _blogPost;

        public BlogViewModel()
        {
            Title = "Browse";
            BlogPostsView = new ObservableCollection<BlogPost>();
            LoadBlogPostsCommand = new Command(async () => await ExecuteLoadBlogPostsCommand());

            BlogPostTapped = new Command<BlogPost>(OnBlogPostSelected);

            AddBlogPostCommand = new Command(OnAddBlogPost);
        }

        async Task ExecuteLoadBlogPostsCommand()
        {
            IsBusy = true;

            try
            {
                BlogPostsView.Clear();
                List<BlogPost> BlogPosts = await BlogPostStore.GetAll();
                foreach(BlogPost blogPost in BlogPosts){
                    BlogPostsView.Add(blogPost);
                }
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedBlogPost = null;
        }

        public BlogPost SelectedBlogPost
        {
            get => _blogPost;
            set
            {
                SetProperty(ref _blogPost, value);
                OnBlogPostSelected(value);
            }
        }

        private async void OnAddBlogPost(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewBlogPage));
        }

        async void OnBlogPostSelected(BlogPost blogPost)
        {
            if (blogPost == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(BlogDetailPage)}?{nameof(BlogDetailViewModel.Id)}={blogPost.Id}");
        }
    }
}