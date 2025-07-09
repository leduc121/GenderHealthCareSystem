using GenderHealthcare.DAL.Entities;
using GenderHealthcare.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenderHealthcare.DAL.Repositories
{
    public class ContraceptiveReminderRepository : IContraceptiveReminderRepository
    {
        private readonly GenderHealthcareContext _ctx;
        public ContraceptiveReminderRepository(GenderHealthcareContext ctx) => _ctx = ctx;

        public async Task AddAsync(ContraceptiveReminder r)
        {
            await _ctx.AddAsync(r);
            await _ctx.SaveChangesAsync();
        }

        public async Task<List<ContraceptiveReminder>> GetAllActiveAsync()
            => await _ctx.Set<ContraceptiveReminder>()
                         .Where(x => x.Status == true)
                         .ToListAsync();

        public async Task<List<ContraceptiveReminder>> GetByUserAsync(string userId)
            => await _ctx.Set<ContraceptiveReminder>()
                         .Where(x => x.UserId == userId && x.Status == true)
                         .ToListAsync();

        public async Task UpdateAsync(ContraceptiveReminder r)
        {
            _ctx.Update(r);
            await _ctx.SaveChangesAsync();
        }
    }
}
