using GenderHealthcare.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GenderHealthcare.DAL.Interfaces
{
    public interface IBlogRepository
    {
        Task<IEnumerable<Blog>> GetAllAsync();
        Task<Blog> GetByIdAsync(string id);
        Task<Blog> CreateAsync(Blog blog);
        Task<Blog> UpdateAsync(string id, Blog blog);
        Task<bool> DeleteAsync(string id);
    }
}