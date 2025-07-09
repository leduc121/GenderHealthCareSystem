using GenderHealthcare.DAL.Entities;
using GenderHealthcare.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenderHealthcare.DAL.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly IDbContextFactory<GenderHealthcareContext> _factory;

        public AppointmentRepository(IDbContextFactory<GenderHealthcareContext> factory)
        {
            _factory = factory;
        }

        public async Task<List<Appointment>> GetAllAsync()
        {
            await using var ctx = _factory.CreateDbContext();
            return await ctx.Appointments.ToListAsync();
        }

        public async Task<Appointment?> GetByCompositeKeyAsync(string userId, string consultantId, DateTime date)
        {
            await using var ctx = _factory.CreateDbContext();
            return await ctx.Appointments.FindAsync(userId, consultantId, date);
        }

        public async Task<List<Appointment>> GetByUserIdAsync(string userId)
        {
            await using var ctx = _factory.CreateDbContext();
            return await ctx.Appointments
                            .Where(a => a.UserId == userId)
                            .ToListAsync();
        }

        public async Task<List<Appointment>> GetByConsultantIdAsync(string consultantId)
        {
            await using var ctx = _factory.CreateDbContext();
            return await ctx.Appointments
                            .Where(a => a.ConsultantId == consultantId)
                            .ToListAsync();
        }

        public async Task<List<Appointment>> GetByDateRangeAsync(DateTime start, DateTime end)
        {
            await using var ctx = _factory.CreateDbContext();
            return await ctx.Appointments
                            .Where(a => a.AppointmentDate >= start && a.AppointmentDate <= end)
                            .ToListAsync();
        }

        public async Task<Appointment> AddAsync(Appointment appt)
        {
            await using var ctx = _factory.CreateDbContext();
            var entry = await ctx.Appointments.AddAsync(appt);
            await ctx.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task<Appointment> UpdateAsync(Appointment appt)
        {
            await using var ctx = _factory.CreateDbContext();
            var entry = ctx.Appointments.Update(appt);
            await ctx.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task<bool> DeleteByCompositeKeyAsync(string userId, string consultantId, DateTime date)
        {
            await using var ctx = _factory.CreateDbContext();
            var appt = await ctx.Appointments.FindAsync(userId, consultantId, date);
            if (appt == null) return false;
            ctx.Appointments.Remove(appt);
            await ctx.SaveChangesAsync();
            return true;
        }
    }
}
