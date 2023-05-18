using BlogsiteMobile.Models;
using Microsoft.EntityFrameworkCore;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BlogsiteMobile.Services
{
    public class BlogPostRepository : IBlogPostRepository<BlogPost>
    {
        private readonly SQLiteAsyncConnection _connection;

        public BlogPostRepository()
        {
            _connection = new SQLiteAsyncConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "BlogPosts.db3"));
            _connection.CreateTableAsync<BlogPost>();
        }
        public BlogPostRepository(string dbPath)
        {
            _connection = new SQLiteAsyncConnection(dbPath);
            _connection.CreateTableAsync<BlogPost>();
        }


        public Task<int> AddBlogPost(BlogPost blogPost)
        {
            return _connection.InsertAsync(blogPost);
        }

        public Task<List<BlogPost>> GetAll(string? includeProperties = null)
        {
            return _connection.Table<BlogPost>().ToListAsync();

        }
        public async Task<List<BlogPost>> GetAllFromId(int id)
        {
            AsyncTableQuery<BlogPost> query = null;
            query = _connection.Table<BlogPost>().Where(u => u.Id == id);
            List<BlogPost> matchingPosts = await query.ToListAsync();

            return matchingPosts;
        }
        public async Task<List<BlogPost>> GetAllFromCategory(string category)
        {
            AsyncTableQuery<BlogPost> query = null;
            query = _connection.Table<BlogPost>().Where(u => u.Category == category);
            List<BlogPost> matchingPosts = await query.ToListAsync();

            return matchingPosts;
        }
        public async Task<List<BlogPost>> GetAllFromUser(int id)
        {
            AsyncTableQuery<BlogPost> query = null;
            query = _connection.Table<BlogPost>().Where(u => u.ApplicationUserId == id);
            List<BlogPost> matchingPosts = await query.ToListAsync();

            return matchingPosts;
        }
        public async Task<List<BlogPost>> GetAllFromUserIdAndCategory(int id, string category)
        {
            AsyncTableQuery<BlogPost> query = null;
            query = _connection.Table<BlogPost>().Where(u => u.ApplicationUserId == id && u.Category == category);
            List<BlogPost> matchingPosts = await query.ToListAsync();

            return matchingPosts;
        }
        public async Task<List<BlogPost>> GetAllFromUserAndCategory(string username, string category)
        {
            AsyncTableQuery<BlogPost> query = null;
            query = _connection.Table<BlogPost>().Where(u => u.Author == username && u.Category == category);
            List<BlogPost> matchingPosts = await query.ToListAsync();

            return matchingPosts;
        }
        public async Task<List<BlogPost>> GetAllFromUserName(string username)
        {
            AsyncTableQuery<BlogPost> query = null;
            query = _connection.Table<BlogPost>().Where(u => u.Author == username);
            List<BlogPost> matchingPosts = await query.ToListAsync();

            return matchingPosts;
        }

        public Task<BlogPost> GetFirstOrDefault(int id)
        {
            AsyncTableQuery<BlogPost> query = _connection.Table<BlogPost>().Where(u => u.Id == id);


            return query.FirstOrDefaultAsync();
        }

        public Task<int> Remove(BlogPost entity)
        {
             return _connection.DeleteAsync(entity);
        }
        public async void Update(BlogPost obj)
        {
            try
            {
                BlogPost blogPost = await GetFirstOrDefault(obj.Id);
                if (blogPost != null)
                {

                    blogPost.BlogPostTitle = obj.BlogPostTitle;
                    blogPost.Text = obj.Text;
                    blogPost.Category = obj.Category;
                    await _connection.UpdateAsync(blogPost);
                }

            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
