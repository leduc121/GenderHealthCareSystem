using GenderHealthcare.DAL.Entities;
using GenderHealthcare.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GenderHealthcare.DAL.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private readonly IDbContextFactory<GenderHealthcareContext> _contextFactory;

        public BlogRepository(IDbContextFactory<GenderHealthcareContext> contextFactory)
        {
            _contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
        }

        public async Task<IEnumerable<Blog>> GetAllAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Blogs.ToListAsync();
        }

        public async Task<Blog> GetByIdAsync(string id)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Blogs.FindAsync(id);
        }

        public async Task<Blog> CreateAsync(Blog blog)
        {
            using var context = _contextFactory.CreateDbContext();
            blog.Id = Guid.NewGuid().ToString();
            await context.Blogs.AddAsync(blog);
            await context.SaveChangesAsync();
            return blog;
        }

        public async Task<Blog> UpdateAsync(string id, Blog blog)
        {
            using var context = _contextFactory.CreateDbContext();
            var existingBlog = await context.Blogs.FindAsync(id);
            if (existingBlog == null)
            {
                return null;
            }

            existingBlog.Title = blog.Title;
            existingBlog.Content = blog.Content;
            existingBlog.AuthorId = blog.AuthorId;
            existingBlog.PublishedDate = blog.PublishedDate;
            existingBlog.Status = blog.Status;

            context.Blogs.Update(existingBlog);
            await context.SaveChangesAsync();
            return existingBlog;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            using var context = _contextFactory.CreateDbContext();
            var blog = await context.Blogs.FindAsync(id);
            if (blog == null)
            {
                return false;
            }

            context.Blogs.Remove(blog);
            await context.SaveChangesAsync();
            return true;
        }
    }
}