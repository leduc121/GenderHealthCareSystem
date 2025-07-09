using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenderHealthcare.BLL.DTOs;
using GenderHealthcare.BLL.Interfaces;
using GenderHealthcare.DAL.Entities;
using GenderHealthcare.DAL.Interfaces;

namespace GenderHealthcare.BLL.Services
{
    public class StdDiseaseService : IStdDiseaseService
    {
        private readonly IStdDiseaseRepository _repo;
        public StdDiseaseService(IStdDiseaseRepository repo) => _repo = repo;

        public async Task<IEnumerable<StdDiseaseDTO>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            return list.Select(d => new StdDiseaseDTO
            {
                Id = d.Id,
                DiseaseName = d.DiseaseName,
                TestPrice = d.TestPrice
            });
        }

        public async Task<StdDiseaseDTO> CreateAsync(StdDiseaseDTO dto)
        {
            var entity = new StdDisease { Id = dto.Id, DiseaseName = dto.DiseaseName, TestPrice = dto.TestPrice, Status = true, UpdatedAt = DateTime.Now };
            await _repo.AddAsync(entity);
            return dto;
        }

        public async Task<StdDiseaseDTO> UpdateAsync(string id, StdDiseaseDTO dto)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity != null)
            {
                entity.DiseaseName = dto.DiseaseName;
                entity.TestPrice = dto.TestPrice;
                entity.UpdatedAt = DateTime.Now;
                await _repo.UpdateAsync(entity);
                return dto;
            }
            return null;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity != null)
            {
                entity.Status = false;
                entity.UpdatedAt = DateTime.Now;
                await _repo.UpdateAsync(entity);
                return true;
            }
            return false;
        }
    }
}