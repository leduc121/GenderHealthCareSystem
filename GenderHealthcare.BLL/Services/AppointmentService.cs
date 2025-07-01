using GenderHealthcare.BLL.DTOs;
using GenderHealthcare.BLL.Interfaces;
using GenderHealthcare.DAL.Entities;
using GenderHealthcare.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenderHealthcare.BLL.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<IEnumerable<AppointmentDTO>> GetAllAppointmentsAsync()
        {
            var appointments = await _appointmentRepository.GetAllAsync();
            return appointments.Select(a => MapToDTO(a));
        }

        public async Task<AppointmentDTO> GetAppointmentByIdAsync(string userId, string consultantId, DateTime appointmentDate)
        {
            var appointment = await _appointmentRepository.GetByCompositeKeyAsync(userId, consultantId, appointmentDate);
            return appointment != null ? MapToDTO(appointment) : null;
        }

        public async Task<IEnumerable<AppointmentDTO>> GetAppointmentsByUserIdAsync(string userId)
        {
            var appointments = await _appointmentRepository.GetByUserIdAsync(userId);
            return appointments.Select(a => MapToDTO(a));
        }

        public async Task<IEnumerable<AppointmentDTO>> GetAppointmentsByConsultantIdAsync(string consultantId)
        {
            var appointments = await _appointmentRepository.GetByConsultantIdAsync(consultantId);
            return appointments.Select(a => MapToDTO(a));
        }

        public async Task<AppointmentDTO> CreateAppointmentAsync(AppointmentDTO appointmentDto)
        {
            var appointment = MapToEntity(appointmentDto);
            var createdAppointment = await _appointmentRepository.AddAsync(appointment);
            return MapToDTO(createdAppointment);
        }

        public async Task<AppointmentDTO> UpdateAppointmentAsync(AppointmentDTO appointmentDto)
        {
            var appointment = MapToEntity(appointmentDto);
            var updatedAppointment = await _appointmentRepository.UpdateAsync(appointment);
            return MapToDTO(updatedAppointment);
        }

        public async Task<bool> DeleteAppointmentAsync(string userId, string consultantId, DateTime appointmentDate)
        {
            return await _appointmentRepository.DeleteByCompositeKeyAsync(userId, consultantId, appointmentDate);
        }

        public async Task<IEnumerable<AppointmentDTO>> GetAppointmentsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var appointments = await _appointmentRepository.GetByDateRangeAsync(startDate, endDate);
            return appointments.Select(a => MapToDTO(a));
        }

        private AppointmentDTO MapToDTO(Appointment appointment)
        {
            return new AppointmentDTO
            {
                UserId = appointment.UserId,
                ConsultantId = appointment.ConsultantId,
                AppointmentDate = appointment.AppointmentDate,
                AppointmentStatus = appointment.AppointmentStatus,
                AppointmentLocation = appointment.AppointmentLocation,
                StartTime = appointment.StartTime,
                EndTime = appointment.EndTime,
                FixedPrice = appointment.FixedPrice,
                Status = appointment.Status,
                UpdatedAt = appointment.UpdatedAt
            };
        }

        private Appointment MapToEntity(AppointmentDTO appointmentDto)
        {
            return new Appointment
            {
                UserId = appointmentDto.UserId,
                ConsultantId = appointmentDto.ConsultantId,
                AppointmentDate = appointmentDto.AppointmentDate,
                AppointmentStatus = appointmentDto.AppointmentStatus,
                AppointmentLocation = appointmentDto.AppointmentLocation,
                StartTime = appointmentDto.StartTime,
                EndTime = appointmentDto.EndTime,
                FixedPrice = appointmentDto.FixedPrice,
                Status = appointmentDto.Status,
                UpdatedAt = appointmentDto.UpdatedAt
            };
        }
    }
}