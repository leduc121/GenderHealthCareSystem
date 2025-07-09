using System.Collections.Generic;
using System.Threading.Tasks;
using GenderHealthcare.BLL.DTOs;

namespace GenderHealthcare.BLL.Interfaces
{
    public interface IStdDiseaseService
    {
        Task<IEnumerable<StdDiseaseDTO>> GetAllAsync();
        Task<StdDiseaseDTO> CreateAsync(StdDiseaseDTO dto);
        Task<StdDiseaseDTO> UpdateAsync(string id, StdDiseaseDTO dto);
        Task<bool> DeleteAsync(string id);
    }
}