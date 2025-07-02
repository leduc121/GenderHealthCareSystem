using GenderHealthcare.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenderHealthcare.DAL.Repositories
{
    public class MenstrualCycleRepository
    {
        private readonly GenderHealthcareContext _context;

        public MenstrualCycleRepository(GenderHealthcareContext context)
        {
            _context = context;
        }

        public async Task AddAsync(MenstrualCycle cycle)
        {
            // Gán giá trị mặc định nếu cần
            cycle.UpdatedAt = DateTime.Now;
            cycle.Status = true;
            await _context.MenstrualCycles.AddAsync(cycle);
            await _context.SaveChangesAsync();
        }

        // Thay đổi kiểu dữ liệu của userId thành string
        public async Task<List<MenstrualCycle>> GetByUserIdAsync(string userId)
        {
            return await _context.MenstrualCycles
                                 .Where(c => c.UserId == userId && c.Status == true)
                                 .OrderByDescending(c => c.CycleStartDate)
                                 .ToListAsync();
        }
    }
}