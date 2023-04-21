using BlogsiteMobile.Models;
using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BlogsiteMobile.ViewModels
{
    [QueryProperty(nameof(BlogPostId), nameof(BlogPostId))]
    public class BlogDetailViewModel : BaseViewModel
    {
        private int blogPostId;
        private string text;
        private string blogPostTitle;
        private string author;
        public int Id { get; set; }

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
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
