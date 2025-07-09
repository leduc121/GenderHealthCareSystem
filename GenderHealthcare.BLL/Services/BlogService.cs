using GenderHealthcare.BLL.DTOs;
using GenderHealthcare.BLL.Interfaces;
using GenderHealthcare.DAL.Entities;
using GenderHealthcare.DAL.Repositories;
using GenderHealthcare.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenderHealthcare.BLL.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;

        public BlogService(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository ?? throw new ArgumentNullException(nameof(blogRepository));
        }

        public async Task<IEnumerable<BlogDTO>> GetAllBlogsAsync()
        {
            var blogs = await _blogRepository.GetAllAsync();
            return blogs.Select(b => new BlogDTO
            {
                Id = b.Id,
                Title = b.Title,
                Content = b.Content,
                AuthorId = b.AuthorId,
                PublishedDate = b.PublishedDate,
                Status = b.Status
            });
        }

        public async Task<BlogDTO> GetBlogByIdAsync(string id)
        {
            var blog = await _blogRepository.GetByIdAsync(id);
            if (blog == null)
            {
                return null;
            }
            return new BlogDTO
            {
                Id = blog.Id,
                Title = blog.Title,
                Content = blog.Content,
                AuthorId = blog.AuthorId,
                PublishedDate = blog.PublishedDate,
                Status = blog.Status
            };
        }

        public async Task<BlogDTO> CreateBlogAsync(BlogDTO blogDto)
        {
            var blog = new Blog
            {
                Id = Guid.NewGuid().ToString(),
                Title = blogDto.Title,
                Content = blogDto.Content,
                AuthorId = blogDto.AuthorId,
                PublishedDate = blogDto.PublishedDate,
                Status = blogDto.Status
            };
            var createdBlog = await _blogRepository.CreateAsync(blog);
            return new BlogDTO
            {
                Id = createdBlog.Id,
                Title = createdBlog.Title,
                Content = createdBlog.Content,
                AuthorId = createdBlog.AuthorId,
                PublishedDate = createdBlog.PublishedDate,
                Status = createdBlog.Status
            };
        }

        public async Task<BlogDTO> UpdateBlogAsync(string id, BlogDTO blogDto)
        {
            var blog = new Blog
            {
                Id = id,
                Title = blogDto.Title,
                Content = blogDto.Content,
                AuthorId = blogDto.AuthorId,
                PublishedDate = blogDto.PublishedDate,
                Status = blogDto.Status
            };
            var updatedBlog = await _blogRepository.UpdateAsync(id, blog);
            if (updatedBlog == null)
            {
                return null;
            }
            return new BlogDTO
            {
                Id = updatedBlog.Id,
                Title = updatedBlog.Title,
                Content = updatedBlog.Content,
                AuthorId = updatedBlog.AuthorId,
                PublishedDate = updatedBlog.PublishedDate,
                Status = updatedBlog.Status
            };
        }

        public async Task<bool> DeleteBlogAsync(string id)
        {
            return await _blogRepository.DeleteAsync(id);
        }
    }
}