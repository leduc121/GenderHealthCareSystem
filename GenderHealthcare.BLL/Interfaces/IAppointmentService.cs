using GenderHealthcare.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GenderHealthcare.BLL.Interfaces
{
    public interface IAppointmentService
    {
        Task<IEnumerable<AppointmentDTO>> GetAllAppointmentsAsync();
        Task<AppointmentDTO> GetAppointmentByIdAsync(string userId, string consultantId, DateTime appointmentDate);
        Task<IEnumerable<AppointmentDTO>> GetAppointmentsByUserIdAsync(string userId);
        Task<IEnumerable<AppointmentDTO>> GetAppointmentsByConsultantIdAsync(string consultantId);
        Task<AppointmentDTO> CreateAppointmentAsync(AppointmentDTO appointmentDto);
        Task<AppointmentDTO> UpdateAppointmentAsync(AppointmentDTO appointmentDto);
        Task<bool> DeleteAppointmentAsync(string userId, string consultantId, DateTime appointmentDate);
        Task<IEnumerable<AppointmentDTO>> GetAppointmentsByDateRangeAsync(DateTime startDate, DateTime endDate);
    }
}