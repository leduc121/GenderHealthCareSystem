using GenderHealthcare.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GenderHealthcare.BLL.Interfaces
{
    public interface ICycleService
    {
        Task<IEnumerable<MenstrualCycleDTO>> GetMyCyclesAsync();
        Task RecordCycleAsync(DateOnly? cycleStartDate, DateOnly? cycleEndDate, string notes);
    }
}