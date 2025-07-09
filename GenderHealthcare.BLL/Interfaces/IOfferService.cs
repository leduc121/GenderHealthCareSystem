using System.Collections.Generic;
using System.Threading.Tasks;
using GenderHealthcare.BLL.DTOs;

namespace GenderHealthcare.BLL.Interfaces
{
    public interface IOfferService
    {
        Task<IEnumerable<OfferDTO>> GetActiveOffersAsync();
        Task<OfferDTO> CreateAsync(OfferDTO dto);
        Task<OfferDTO> UpdateAsync(string id, OfferDTO dto);
        Task<bool> DeleteAsync(string id);
    }
}