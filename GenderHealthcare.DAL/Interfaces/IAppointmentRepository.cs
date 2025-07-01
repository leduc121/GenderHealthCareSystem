using GenderHealthcare.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GenderHealthcare.DAL.Interfaces
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
        Task<IEnumerable<Appointment>> GetByUserIdAsync(string userId);
        Task<IEnumerable<Appointment>> GetByConsultantIdAsync(string consultantId);
        Task<IEnumerable<Appointment>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<Appointment> GetByCompositeKeyAsync(string userId, string consultantId, DateTime appointmentDate);
        Task<bool> DeleteByCompositeKeyAsync(string userId, string consultantId, DateTime appointmentDate);
    }
}