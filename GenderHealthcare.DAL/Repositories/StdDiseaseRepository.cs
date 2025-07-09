using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using GenderHealthcare.DAL.Entities;
using GenderHealthcare.DAL.Interfaces;

namespace GenderHealthcare.DAL.Repositories
{
    public class StdDiseaseRepository : IStdDiseaseRepository
    {
        private readonly IDbContextFactory<GenderHealthcareContext> _factory;

        public StdDiseaseRepository(IDbContextFactory<GenderHealthcareContext> factory)
        {
            _factory = factory;
        }

        public async Task<List<StdDisease>> GetAllAsync()
        {
            await using var ctx = _factory.CreateDbContext();
            return await ctx.StdDiseases
                            .Where(d => d.Status == true)
                            .ToListAsync();
        }

        public async Task<StdDisease> GetByIdAsync(string id)
        {
            await using var ctx = _factory.CreateDbContext();
            return await ctx.StdDiseases.FindAsync(id);
        }

        public async Task AddAsync(StdDisease entity)
        {
            await using var ctx = _factory.CreateDbContext();
            ctx.StdDiseases.Add(entity);
            await ctx.SaveChangesAsync();
        }

        public async Task UpdateAsync(StdDisease entity)
        {
            await using var ctx = _factory.CreateDbContext();
            ctx.StdDiseases.Update(entity);
            await ctx.SaveChangesAsync();
        }
    }
}