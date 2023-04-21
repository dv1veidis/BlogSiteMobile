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

        public Task<BlogPost> GetFirstOrDefault(int id)
        {
            AsyncTableQuery<BlogPost> query = _connection.Table<BlogPost>().Where(u => u.Id == id);


            return query.FirstOrDefaultAsync();
        }

        public Task<int> Remove(BlogPost entity)
        {
             return _connection.DeleteAsync(entity);
        }
    }
}
