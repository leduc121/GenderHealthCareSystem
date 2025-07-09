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
    public class ContraceptiveReminderService : IContraceptiveReminderService
    {
        private readonly IContraceptiveReminderRepository _repo;
        public ContraceptiveReminderService(IContraceptiveReminderRepository repo) => _repo = repo;

        public async Task CreateReminderAsync(ContraceptiveReminderDTO dto)
        {
            var ent = new ContraceptiveReminder
            {
                UserId = dto.UserId,
                ContraceptiveType = dto.ContraceptiveType,
                ReminderTime = dto.ReminderTime,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Frequency = dto.Frequency,
                ReminderStatus = dto.ReminderStatus,
                ReminderMessage = dto.ReminderMessage,
                Status = dto.Status ?? true,
                UpdatedAt = dto.UpdatedAt ?? DateTime.UtcNow
            };
            await _repo.AddAsync(ent);
        }

        public async Task<IEnumerable<ContraceptiveReminderDTO>> GetAllActiveRemindersAsync()
            => (await _repo.GetAllActiveAsync()).Select(MapToDto);

        public async Task<IEnumerable<ContraceptiveReminderDTO>> GetActiveRemindersByUserAsync(string userId)
            => (await _repo.GetByUserAsync(userId)).Select(MapToDto);

        public async Task UpdateReminderAsync(ContraceptiveReminderDTO dto)
        {
            var ent = new ContraceptiveReminder
            {
                UserId = dto.UserId,
                ContraceptiveType = dto.ContraceptiveType,
                ReminderTime = dto.ReminderTime,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Frequency = dto.Frequency,
                ReminderStatus = dto.ReminderStatus,
                ReminderMessage = dto.ReminderMessage,
                Status = dto.Status,
                UpdatedAt = DateTime.UtcNow
            };
            await _repo.UpdateAsync(ent);
        }

        private static ContraceptiveReminderDTO MapToDto(ContraceptiveReminder e)
            => new ContraceptiveReminderDTO
            {
                UserId = e.UserId,
                ContraceptiveType = e.ContraceptiveType,
                ReminderTime = e.ReminderTime,
                StartDate = e.StartDate,
                EndDate = e.EndDate,
                Frequency = e.Frequency,
                ReminderStatus = e.ReminderStatus,
                ReminderMessage = e.ReminderMessage,
                Status = e.Status,
                UpdatedAt = e.UpdatedAt
            };
    }
}
