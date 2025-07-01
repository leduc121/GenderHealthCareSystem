using GenderHealthcare.DAL.Entities;
using GenderHealthcare.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GenderHealthcare.DAL.Repositories
{
    public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(GenderHealthcareContext context) : base(context) { }

        public override async Task<Appointment> GetByIdAsync(string id)
        {
            // Logic mặc định, có thể không sử dụng cho composite key
            return await _dbSet.FindAsync(id);
        }

        public override async Task<bool> DeleteAsync(string id)
        {
            var appointment = await GetByIdAsync(id);
            if (appointment == null) return false;
            _dbSet.Remove(appointment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Appointment>> GetByUserIdAsync(string userId)
        {
            return await _dbSet.Where(a => a.UserId == userId)
                              .Include(a => a.User)
                              .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetByConsultantIdAsync(string consultantId)
        {
            return await _dbSet.Where(a => a.ConsultantId == consultantId)
                              .Include(a => a.User)
                              .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbSet.Where(a => a.AppointmentDate >= startDate && a.AppointmentDate <= endDate)
                              .Include(a => a.User)
                              .ToListAsync();
        }

        public async Task<Appointment> GetByCompositeKeyAsync(string userId, string consultantId, DateTime appointmentDate)
        {
            return await _dbSet
                .FirstOrDefaultAsync(a => a.UserId == userId && a.ConsultantId == consultantId && a.AppointmentDate == appointmentDate);
        }

        public async Task<bool> DeleteByCompositeKeyAsync(string userId, string consultantId, DateTime appointmentDate)
        {
            var appointment = await GetByCompositeKeyAsync(userId, consultantId, appointmentDate);
            if (appointment == null) return false;

            _dbSet.Remove(appointment);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}