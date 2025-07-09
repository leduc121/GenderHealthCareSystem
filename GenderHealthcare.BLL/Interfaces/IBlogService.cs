using GenderHealthcare.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenderHealthcare.BLL.Interfaces
{
    public interface IBlogService
    {
        Task<IEnumerable<BlogDTO>> GetAllBlogsAsync();
        Task<BlogDTO> GetBlogByIdAsync(string id);
        Task<BlogDTO> CreateBlogAsync(BlogDTO blogDTO);
        Task<BlogDTO> UpdateBlogAsync(string id, BlogDTO blogDTO);
        Task<bool> DeleteBlogAsync(string id);
    }
}
