using GenderHealthcare.DAL.Entities;
using GenderHealthcare.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GenderHealthcare.DAL.Repositories
{
    public class ConsultantProfileRepository : Repository<ConsultantProfile>, IConsultantProfileRepository
    {
        private readonly IDbContextFactory<GenderHealthcareContext> _contextFactory;

        public ConsultantProfileRepository(GenderHealthcareContext context, IDbContextFactory<GenderHealthcareContext> contextFactory)
            : base(context)
        {
            _contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
        }

        public async Task<ConsultantProfile> GetByConsultantIdAsync(string consultantId)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.ConsultantProfiles.FirstOrDefaultAsync(cp => cp.ConsultantId == consultantId);
        }
    }
}