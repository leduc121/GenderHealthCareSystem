using GenderHealthcare.DAL.Entities;
using GenderHealthcare.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GenderHealthcare.DAL.Repositories
{
    public class ConsultantProfileRepository : Repository<ConsultantProfile>, IConsultantProfileRepository
    {
        public ConsultantProfileRepository(GenderHealthcareContext context) : base(context) { }

        public async Task<ConsultantProfile> GetByConsultantIdAsync(string consultantId)
        {
            return await _dbSet.FirstOrDefaultAsync(cp => cp.ConsultantId == consultantId);
        }
    }
}