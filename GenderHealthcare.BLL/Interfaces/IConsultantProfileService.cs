using GenderHealthcare.BLL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GenderHealthcare.BLL.Interfaces
{
    public interface IConsultantProfileService
    {
        Task<IEnumerable<ConsultantProfileDTO>> GetAllConsultantProfilesAsync();
        Task<ConsultantProfileDTO> GetConsultantProfileByIdAsync(string consultantId);
        Task<ConsultantProfileDTO> CreateConsultantProfileAsync(ConsultantProfileDTO consultantProfileDto);
        Task<ConsultantProfileDTO> UpdateConsultantProfileAsync(ConsultantProfileDTO consultantProfileDto);
        Task<bool> DeleteConsultantProfileAsync(string consultantId);
    }
}