using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using GenderHealthcare.DAL.Entities;
using GenderHealthcare.DAL.Interfaces;

namespace GenderHealthcare.DAL.Repositories
{
    public class OfferRepository : IOfferRepository
    {
        private readonly IDbContextFactory<GenderHealthcareContext> _factory;

        public OfferRepository(IDbContextFactory<GenderHealthcareContext> factory)
        {
            _factory = factory;
        }

        public async Task<List<Offer>> GetActiveOffersAsync()
        {
            await using var ctx = _factory.CreateDbContext();
            var now = DateTime.Now;
            return await ctx.Offers
                            .Where(o => o.Status == true
                                     && o.StartDate <= now
                                     && o.EndDate >= now)
                            .ToListAsync();
        }

        public async Task<Offer> GetByIdAsync(string id)
        {
            await using var ctx = _factory.CreateDbContext();
            return await ctx.Offers.FindAsync(id);
        }

        public async Task AddAsync(Offer entity)
        {
            await using var ctx = _factory.CreateDbContext();
            ctx.Offers.Add(entity);
            await ctx.SaveChangesAsync();
        }

        public async Task UpdateAsync(Offer entity)
        {
            await using var ctx = _factory.CreateDbContext();
            ctx.Offers.Update(entity);
            await ctx.SaveChangesAsync();
        }
    }
}