using GenderHealthcare.BLL.DTOs;
using GenderHealthcare.BLL.Interfaces;
using GenderHealthcare.DAL.Entities;
using GenderHealthcare.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenderHealthcare.BLL.Services
{
    public class ConsultantProfileService : IConsultantProfileService
    {
        private readonly IConsultantProfileRepository _consultantProfileRepository;

        public ConsultantProfileService(IConsultantProfileRepository consultantProfileRepository)
        {
            _consultantProfileRepository = consultantProfileRepository;
        }

        public async Task<IEnumerable<ConsultantProfileDTO>> GetAllConsultantProfilesAsync()
        {
            var profiles = await _consultantProfileRepository.GetAllAsync();
            return profiles.Select(cp => MapToDTO(cp));
        }

        public async Task<ConsultantProfileDTO> GetConsultantProfileByIdAsync(string consultantId)
        {
            var profile = await _consultantProfileRepository.GetByConsultantIdAsync(consultantId);
            return profile != null ? MapToDTO(profile) : null;
        }

        public async Task<ConsultantProfileDTO> CreateConsultantProfileAsync(ConsultantProfileDTO consultantProfileDto)
        {
            var profile = MapToEntity(consultantProfileDto);
            var createdProfile = await _consultantProfileRepository.AddAsync(profile);
            return MapToDTO(createdProfile);
        }

        public async Task<ConsultantProfileDTO> UpdateConsultantProfileAsync(ConsultantProfileDTO consultantProfileDto)
        {
            var profile = MapToEntity(consultantProfileDto);
            var updatedProfile = await _consultantProfileRepository.UpdateAsync(profile);
            return MapToDTO(updatedProfile);
        }

        public async Task<bool> DeleteConsultantProfileAsync(string consultantId)
        {
            return await _consultantProfileRepository.DeleteAsync(consultantId);
        }

        private ConsultantProfileDTO MapToDTO(ConsultantProfile profile)
        {
            return new ConsultantProfileDTO
            {
               
                ConsultantId = profile.ConsultantId,
                Specialization = profile.Specialization,
                Qualification = profile.Qualification,
                Experience = profile.Experience,
                ConsultationFee = profile.ConsultationFee,
                IsAvailable = profile.IsAvailable,
                ProfileStatus = profile.ProfileStatus,
                Status = profile.Status,
                UpdatedAt = profile.UpdatedAt
            };
        }

        private ConsultantProfile MapToEntity(ConsultantProfileDTO consultantProfileDto)
        {
            return new ConsultantProfile
            {
                ConsultantId = consultantProfileDto.ConsultantId,
                Specialization = consultantProfileDto.Specialization,
                Qualification = consultantProfileDto.Qualification,
                Experience = consultantProfileDto.Experience,
                ConsultationFee = consultantProfileDto.ConsultationFee,
                IsAvailable = consultantProfileDto.IsAvailable,
                ProfileStatus = consultantProfileDto.ProfileStatus,
                Status = consultantProfileDto.Status,
                UpdatedAt = consultantProfileDto.UpdatedAt
            };
        }
    }
}