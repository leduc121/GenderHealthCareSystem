using System.Collections.Generic;
using System.Threading.Tasks;
using GenderHealthcare.DAL.Entities;

namespace GenderHealthcare.DAL.Interfaces
{
    public interface IOfferRepository
    {
        Task<List<Offer>> GetActiveOffersAsync();
        Task<Offer> GetByIdAsync(string id);
        Task AddAsync(Offer entity);
        Task UpdateAsync(Offer entity);
    }
}