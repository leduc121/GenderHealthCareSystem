using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenderHealthcare.DAL.Entities;
using GenderHealthcare.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GenderHealthcare.DAL.Repositories
{
    public class MenstrualCycleRepository : IMenstrualCycleRepository
    {
        private readonly GenderHealthcareContext _context;

        public MenstrualCycleRepository(GenderHealthcareContext context)
        {
            _context = context;
        }

        public async Task<List<MenstrualCycle>> GetByUserIdAsync(string userId)
        {
            return await _context.MenstrualCycles
                .Where(c => c.UserId == userId)
                .OrderByDescending(c => c.CycleStartDate)
                .ToListAsync();
        }

        public async Task AddAsync(MenstrualCycle cycle)
        {
            _context.MenstrualCycles.Add(cycle);
            await _context.SaveChangesAsync();
        }


        public async Task AddOrUpdateAsync(MenstrualCycle cycle)
        {
            var existing = await _context.MenstrualCycles
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == cycle.Id);

            if (existing != null)
            {
                // Nếu record đã tồn tại, update tất cả các field
                _context.MenstrualCycles.Update(cycle);
            }
            else
            {
                // Nếu chưa có, thêm mới
                _context.MenstrualCycles.Add(cycle);
            }

            await _context.SaveChangesAsync();
        }
    }
}
