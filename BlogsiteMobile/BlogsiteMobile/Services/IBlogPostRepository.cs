using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogsiteMobile.Services
{
    public interface IBlogPostRepository<T>
    {
        Task<int> AddBlogPost(T blogPost);
        Task<List<T>> GetAll(string? includeProperties = null);
        Task<List<T>> GetAllFromId(int id);
        Task<T> GetFirstOrDefault(int id);
        Task<int> Remove(T entity);
    }
}
