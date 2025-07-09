// GenderHealthcare.BLL.Services/CycleService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenderHealthcare.BLL.DTOs;
using GenderHealthcare.BLL.Interfaces;
using GenderHealthcare.DAL.Entities;
using GenderHealthcare.DAL.Repositories.Interfaces;

namespace GenderHealthcare.BLL.Services
{
    public class CycleService : ICycleService
    {
        private readonly IMenstrualCycleRepository _repo;
        private readonly ICurrentUserService _userContext;

        public CycleService(
            IMenstrualCycleRepository repo,
            ICurrentUserService userContext)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
        }

        public async Task<IEnumerable<MenstrualCycleDTO>> GetMyCyclesAsync()
        {
            var userId = _userContext.UserId
                ?? throw new InvalidOperationException("Chưa xác định UserId.");

            var cycles = await _repo.GetByUserIdAsync(userId);
            return cycles.Select(c => new MenstrualCycleDTO
            {
                Id = c.Id,
                UserId = c.UserId,
                CycleStartDate = c.CycleStartDate,
                CycleEndDate = c.CycleEndDate,
                CycleLength = c.CycleLength,
                PeriodLength = c.PeriodLength,
                FlowIntensity = c.FlowIntensity,
                PainLevel = c.PainLevel,
                Notes = c.Notes
            });
        }

        public async Task RecordCycleAsync(DateOnly? cycleStartDate,
                                           DateOnly? cycleEndDate,
                                           string notes)
        {
            var userId = _userContext.UserId
                ?? throw new InvalidOperationException("Chưa xác định UserId.");

            var cycle = new MenstrualCycle
            {
                Id = Guid.NewGuid().ToString(),
                UserId = userId,
                CycleStartDate = cycleStartDate,
                CycleEndDate = cycleEndDate,
                Notes = notes,
                UpdatedAt = DateTime.Now
            };

            // đây là chỗ bạn dùng AddOrUpdate
            await _repo.AddOrUpdateAsync(cycle);
        }
    }
}
