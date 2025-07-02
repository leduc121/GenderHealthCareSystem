using GenderHealthcare.BLL.Interfaces;
using GenderHealthcare.DAL.Entities;
using GenderHealthcare.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GenderHealthcare.BLL.Services
{
    // Đảm bảo lớp này implement ICycleService
    public class CycleService : ICycleService
    {
        private readonly MenstrualCycleRepository _cycleRepository;

        // Constructor này nhận Repository từ DI container
        public CycleService(MenstrualCycleRepository cycleRepository)
        {
            _cycleRepository = cycleRepository;
        }

        public async Task<List<MenstrualCycle>> GetMyCyclesAsync()
        {
            var userId = AuthService.CurrentUserId;
            if (string.IsNullOrEmpty(userId))
            {
                return new List<MenstrualCycle>();
            }
            return await _cycleRepository.GetByUserIdAsync(userId);
        }

        public async Task RecordCycleAsync(DateOnly cycleStartDate, DateOnly? cycleEndDate, string notes)
        {
            var userId = AuthService.CurrentUserId;
            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("Yêu cầu đăng nhập để thực hiện chức năng này.");
            }

            var cycle = new MenstrualCycle
            {
                UserId = userId,
                CycleStartDate = cycleStartDate,
                CycleEndDate = cycleEndDate,
                Notes = notes,
                UpdatedAt = DateTime.Now,
                Status = true
            };

            await _cycleRepository.AddAsync(cycle);
        }
    }
}