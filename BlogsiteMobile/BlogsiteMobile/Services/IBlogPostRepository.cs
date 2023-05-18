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
        Task<List<T>> GetAllFromCategory(string category);
        Task<List<T>> GetAllFromUser(int id);
        Task<List<T>> GetAllFromUserIdAndCategory(int id, string category);
        
        Task<List<T>> GetAllFromUserAndCategory(string id, string category);
        Task<List<T>> GetAllFromUserName(string id); 

        Task<T> GetFirstOrDefault(int id);
        Task<int> Remove(T entity);
        void Update(T entity);
    }
}
