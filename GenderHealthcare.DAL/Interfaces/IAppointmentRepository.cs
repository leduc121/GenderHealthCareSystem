using GenderHealthcare.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GenderHealthcare.DAL.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<List<Appointment>> GetAllAsync();
        Task<Appointment?> GetByCompositeKeyAsync(string userId, string consultantId, DateTime date);
        Task<List<Appointment>> GetByUserIdAsync(string userId);
        Task<List<Appointment>> GetByConsultantIdAsync(string consultantId);
        Task<List<Appointment>> GetByDateRangeAsync(DateTime start, DateTime end);
        Task<Appointment> AddAsync(Appointment appt);
        Task<Appointment> UpdateAsync(Appointment appt);
        Task<bool> DeleteByCompositeKeyAsync(string userId, string consultantId, DateTime date);
    }
}
