using BlogsiteMobile.Models;
using BlogsiteMobile.Services;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Diagnostics;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BlogsiteMobile.ViewModels
{
    [QueryProperty(nameof(BlogPostId), nameof(BlogPostId))]
    public class BlogDetailViewModel : BaseViewModel
    {
        public IBlogPostRepository<BlogPost> BlogPostStore => DependencyService.Get<IBlogPostRepository<BlogPost>>();

        private int blogPostId;
        private string text;
        private string blogPostTitle;
        private string author;
        private string category;
        private int karma;
        public int Id { get; set; }
        public BlogDetailViewModel()
        {
            EditCommand = new Command(OnEdit);
            DeleteCommand = new Command(OnDelete);
            this.PropertyChanged +=
                (_, __) => EditCommand.ChangeCanExecute();
        }
        public Command EditCommand { get; }
        public Command DeleteCommand { get; }

        private async void OnDelete()
        {
            BlogPost blogPost = await BlogPostStore.GetFirstOrDefault(Id);
            await BlogPostStore.Remove(blogPost);

            // This will pop the current page off the navigation stack
            await Shell.Current.Navigation.PopAsync();
        }

        private async void OnEdit()
        {
            BlogPost blogPost = new BlogPost()
            {
                Id = Id,
                Text = text,
                Author = author,
                Category = category,
                BlogPostTitle= blogPostTitle
            };
            BlogPostStore.Update(blogPost);
            await Shell.Current.Navigation.PopAsync();
        }
        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string BlogPostTitle
        {
            get => blogPostTitle;
            set => SetProperty(ref blogPostTitle, value);
        }
        public string Author
        {
            get => author;
            set => SetProperty(ref author, value);
        }
        public string Category
        {
            get => category;
            set => SetProperty(ref category, value);
        }
        public int Karma
        {
            get => karma;
            set => SetProperty(ref karma, value);
        }


        public int BlogPostId
        {
            get
            {
                return blogPostId;
            }
            set
            {
                blogPostId = value;
                LoadBlogPostId(value);
            }
        }

        public async void LoadBlogPostId(int blogPostId)
        {
            try
            {
                BlogPost BlogPost = await BlogPostStore.GetFirstOrDefault(blogPostId);
                BlogPostId = BlogPost.Id;
                BlogPostTitle = BlogPost.BlogPostTitle;
                Text = BlogPost.Text;
                Author = BlogPost.Author;
                Category = BlogPost.Category;
                Karma = BlogPost.Karma;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
