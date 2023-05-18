using BlogsiteMobile.Models;
using BlogsiteMobile.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BlogsiteMobile.ViewModels
{
    public class UserBlogViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public bool isReload = false;
        BlogPost changedBlog = new BlogPost();
        public ObservableCollection<BlogPost> BlogPostsView { get; }
        public Command LoadBlogPostsCommand { get; }
        public Command AddBlogPostCommand { get; }
        public Command<BlogPost> BlogPostTapped { get; }

        private BlogPost _blogPost;
        private string _selectedItem;
        private bool filter;
        private int _userId;
        public UserBlogViewModel()
        {

            LikeCommand = new Command(OnLike);
            DislikeCommand = new Command(OnDislike);
            FilterCommand = new Command(Filter);
            Title = "Your Blogs";
            BlogPostsView = new ObservableCollection<BlogPost>();
            LoadBlogPostsCommand = new Command(async () => await ExecuteLoadBlogPostsCommand());
            _userId = (int)App.Current.Properties["UserId"];

            BlogPostTapped = new Command<BlogPost>(OnBlogPostSelected);

            AddBlogPostCommand = new Command(OnAddBlogPost);
        }
        public Command FilterCommand { get; }
        public string SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    OnPropertyChanged(nameof(SelectedItem));
                }
            }
        }
        public Command LikeCommand { get; }
        public Command DislikeCommand { get; }
        private async void Filter()
        {
            filter = true;
            await ExecuteLoadBlogPostsCommand();
            SelectedItem = null;
            // This will pop the current page off the navigation stack
        }

        async Task ExecuteLoadBlogPostsCommand()
        {
            IsBusy = true;

            try
            {
                if (isReload)
                {
                    int initialCount = BlogPostsView.Count();
                    BlogPostsView.Clear();
                    bool removalSuccessful = BlogPostsView.Count < initialCount;
                    if (removalSuccessful)
                    {
                        IEnumerable<Reaction> reactions = App.reactionRepository.GetAllFromId(changedBlog.Id);
                        int likes = 0;
                        int dislikes = 0;
                        foreach (Reaction reaction in reactions)
                        {
                            if (reaction.Action == "Like")
                            {
                                likes++;
                            }
                            else
                            {
                                dislikes++;
                            }
                        }
                        changedBlog.Karma = likes - dislikes;
                        changedBlog.Author = "Created by " + changedBlog.Author;
                        BlogPostsView.Add(changedBlog);
                        isReload = false;
                        changedBlog = new BlogPost();
                    }
                }
                else
                {
                    if (filter && !String.IsNullOrWhiteSpace(SelectedItem))
                    {
                        List<BlogPost> BlogPosts = new List<BlogPost>();
                        BlogPosts = await BlogPostStore.GetAllFromUserIdAndCategory(_userId, _selectedItem);

                        int initialCount = BlogPostsView.Count();
                        BlogPostsView.Clear();
                        bool removalSuccessful = BlogPostsView.Count < initialCount;
                        if (removalSuccessful)
                        {

                            foreach (BlogPost blogPost in BlogPosts)
                            {
                                int likes = 0;
                                int dislikes = 0;
                                // This will push the ItemDetailPage onto the navigation stack

                                BlogDetailViewModel blogDetailViewModel = new BlogDetailViewModel();
                                IEnumerable<Reaction> reactions = App.reactionRepository.GetAllFromId(blogPost.Id);
                                foreach (Reaction reaction in reactions)
                                {
                                    if (reaction.Action == "Like")
                                    {
                                        likes++;
                                    }
                                    else
                                    {
                                        dislikes++;
                                    }
                                }
                                blogPost.Karma = likes - dislikes;
                                blogPost.Author = blogPost.Author;
                                BlogPostsView.Add(blogPost);
                            }
                        }
                    }
                    else
                    {
                        BlogPostsView.Clear();
                        List<BlogPost> BlogPosts = await BlogPostStore.GetAllFromUser(_userId);
                        foreach (BlogPost blogPost in BlogPosts)
                        {
                            int likes = 0;
                            int dislikes = 0;
                            // This will push the ItemDetailPage onto the navigation stack

                            BlogDetailViewModel blogDetailViewModel = new BlogDetailViewModel();
                            IEnumerable<Reaction> reactions = App.reactionRepository.GetAllFromId(blogPost.Id);
                            foreach (Reaction reaction in reactions)
                            {
                                if (reaction.Action == "Like")
                                {
                                    likes++;
                                }
                                else
                                {
                                    dislikes++;
                                }
                            }
                            blogPost.Karma = likes - dislikes;
                            blogPost.Author = blogPost.Author;
                            BlogPostsView.Add(blogPost);
                        }
                    }
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
        private Reaction findReaction(Object obj)
        {
            int blogPostId = (int)obj;
            int userId = (int)App.Current.Properties["UserId"];
            Reaction reaction = App.reactionRepository.GetFirstOrDefault(blogPostId, userId);
            return reaction;
        }

        private async void OnLike(Object obj)
        {
            Reaction reaction = findReaction(obj);
            if (reaction != null && reaction.Action != "Like")
            {
                reaction.Action = "Like";
                App.reactionRepository.Update(reaction);
            }
            else if (reaction != null)
            {
                int result = App.reactionRepository.Remove(reaction);
                Console.WriteLine(result);
            }
            else
            {
                Reaction newReaction = new Reaction();
                newReaction.Action = "Like";
                newReaction.ApplicationUserId = (int)App.Current.Properties["UserId"];
                newReaction.BlogPostId = (int)obj;
                App.reactionRepository.AddReaction(newReaction);
            }
            isReload = true;
            changedBlog = await BlogPostStore.GetFirstOrDefault((int)obj);
            await ExecuteLoadBlogPostsCommand();
        }
        private async void OnDislike(Object obj)
        {
            Reaction reaction = findReaction(obj);
            if (reaction != null && reaction.Action != "Dislike")
            {
                reaction.Action = "Dislike";
                App.reactionRepository.Update(reaction);
            }
            else if (reaction != null)
            {
                App.reactionRepository.Remove(reaction);
            }
            else
            {
                Reaction newReaction = new Reaction();
                newReaction.Action = "Dislike";
                newReaction.ApplicationUserId = (int)App.Current.Properties["UserId"];
                newReaction.BlogPostId = (int)obj;
                App.reactionRepository.AddReaction(newReaction);
            }
            isReload = true;
            changedBlog = await BlogPostStore.GetFirstOrDefault((int)obj);
            await ExecuteLoadBlogPostsCommand();

        }

        async void OnBlogPostSelected(BlogPost blogPost)
        {
            if (blogPost == null)
                return;
            int likes = 0;
            int dislikes = 0;
            // This will push the ItemDetailPage onto the navigation stack

            BlogDetailViewModel blogDetailViewModel = new BlogDetailViewModel();
            IEnumerable<Reaction> reactions = App.reactionRepository.GetAllFromId(blogPost.Id);
            foreach (Reaction reaction in reactions)
            {
                if (reaction.Action == "Like")
                {
                    likes++;
                }
                else
                {
                    dislikes++;
                }
            }
            // Set the Id property with the value from blogPost
            blogDetailViewModel.Id = blogPost.Id;
            blogDetailViewModel.BlogPostTitle = blogPost.BlogPostTitle;
            blogDetailViewModel.Text = blogPost.Text;
            blogDetailViewModel.Author = blogPost.Author;
            blogDetailViewModel.Category = blogPost.Category;
            blogDetailViewModel.Karma = likes - dislikes;

            //await Shell.Current.GoToAsync($"{nameof(BlogDetailPage)}?{nameof(BlogDetailViewModel.Id)}={blogPost.Id}");
            await Shell.Current.Navigation.PushAsync(new UserBlogDetailPage()
            {
                BindingContext = blogDetailViewModel
            });
        }
    }
}
