using System.Collections.Generic;
using System.Threading.Tasks;
using GenderHealthcare.DAL.Entities;

namespace GenderHealthcare.DAL.Repositories.Interfaces
{
    public interface IMenstrualCycleRepository
    {
        /// <summary>
        /// Lấy tất cả bản ghi chu kỳ của user
        /// </summary>
        Task<List<MenstrualCycle>> GetByUserIdAsync(string userId);

        /// <summary>
        /// Thêm mới một bản ghi chu kỳ
        /// </summary>
        Task AddAsync(MenstrualCycle cycle);


        Task AddOrUpdateAsync(MenstrualCycle cycle);
    }
}
