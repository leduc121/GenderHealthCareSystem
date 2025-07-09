using GenderHealthcare.DAL.Entities;
using GenderHealthcare.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GenderHealthcare.DAL.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly IDbContextFactory<GenderHealthcareContext> _factory;

        public RoleRepository(IDbContextFactory<GenderHealthcareContext> factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            await using var ctx = _factory.CreateDbContext();
            return await ctx.Roles.ToListAsync();
        }

        public async Task<Role> GetByIdAsync(string id)
        {
            await using var ctx = _factory.CreateDbContext();
            return await ctx.Roles.FindAsync(id);
        }

        public async Task<IEnumerable<Role>> FindAsync(Expression<Func<Role, bool>> predicate)
        {
            await using var ctx = _factory.CreateDbContext();
            return await ctx.Roles.Where(predicate).ToListAsync();
        }

        public async Task<Role> AddAsync(Role entity)
        {
            await using var ctx = _factory.CreateDbContext();
            var e = await ctx.Roles.AddAsync(entity);
            await ctx.SaveChangesAsync();
            return e.Entity;
        }

        public async Task<Role> UpdateAsync(Role entity)
        {
            await using var ctx = _factory.CreateDbContext();
            var existing = await ctx.Roles.FindAsync(entity.Id);
            if (existing == null) throw new Exception("Role not found");
            ctx.Entry(existing).CurrentValues.SetValues(entity);
            await ctx.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            await using var ctx = _factory.CreateDbContext();
            var role = await ctx.Roles.FindAsync(id);
            if (role == null) return false;
            ctx.Roles.Remove(role);
            await ctx.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(string id)
        {
            await using var ctx = _factory.CreateDbContext();
            return await ctx.Roles.AnyAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Role>> GetRolesByUserIdAsync(string userId)
        {
            await using var ctx = _factory.CreateDbContext();
            return await ctx.UserRoles
                .Where(ur => ur.UserId == userId)
                .Include(ur => ur.Role)
                .Select(ur => ur.Role)
                .ToListAsync();
        }
    }
}