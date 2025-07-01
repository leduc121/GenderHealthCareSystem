using GenderHealthcare.DAL.Entities;
using System.Threading.Tasks;

namespace GenderHealthcare.DAL.Interfaces
{
    public interface IConsultantProfileRepository : IRepository<ConsultantProfile>
    {
        Task<ConsultantProfile> GetByConsultantIdAsync(string consultantId);
    }
}