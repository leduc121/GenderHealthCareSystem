using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenderHealthcare.BLL.DTOs;
using GenderHealthcare.BLL.Interfaces;
using GenderHealthcare.DAL.Entities;
using GenderHealthcare.DAL.Interfaces;

namespace GenderHealthcare.BLL.Services
{
    public class OfferService : IOfferService
    {
        private readonly IOfferRepository _repo;
        public OfferService(IOfferRepository repo) => _repo = repo;

        public async Task<IEnumerable<OfferDTO>> GetActiveOffersAsync()
        {
            var list = await _repo.GetActiveOffersAsync();
            return list.Select(o => new OfferDTO
            {
                Id = o.Id,
                OfferName = o.OfferName,
                OfferType = o.OfferType,
                DiscountValue = o.DiscountValue ?? 0,
                MinAmount = o.MinAmount ?? 0,
                MaxDiscount = o.MaxDiscount ?? 0,
                StartDate = o.StartDate,
                EndDate = o.EndDate,
                ApplicableServices = o.ApplicableServices ?? string.Empty
            });
        }

        public async Task<OfferDTO> CreateAsync(OfferDTO dto)
        {
            var entity = new Offer
            {
                Id = dto.Id,
                OfferName = dto.OfferName,
                OfferType = dto.OfferType,
                DiscountValue = dto.DiscountValue,
                MinAmount = dto.MinAmount,
                MaxDiscount = dto.MaxDiscount,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                ApplicableServices = dto.ApplicableServices,
                Status = true,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            await _repo.AddAsync(entity);
            return dto;
        }

        public async Task<OfferDTO> UpdateAsync(string id, OfferDTO dto)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity != null)
            {
                entity.OfferName = dto.OfferName;
                entity.OfferType = dto.OfferType;
                entity.DiscountValue = dto.DiscountValue;
                entity.MinAmount = dto.MinAmount;
                entity.MaxDiscount = dto.MaxDiscount;
                entity.StartDate = dto.StartDate;
                entity.EndDate = dto.EndDate;
                entity.ApplicableServices = dto.ApplicableServices;
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