using GenderHealthcare.DAL.Entities; // Hoặc DTO nếu bạn tạo DTO cho Cycle
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GenderHealthcare.BLL.Interfaces
{
    public interface ICycleService
    {
        Task<List<MenstrualCycle>> GetMyCyclesAsync();
        Task RecordCycleAsync(DateOnly cycleStartDate, DateOnly? cycleEndDate, string notes);
    }
}