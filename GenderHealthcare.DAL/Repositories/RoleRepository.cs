using GenderHealthcare.DAL.Entities;
using GenderHealthcare.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GenderHealthcare.DAL.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(GenderHealthcareContext context) : base(context) { }

        public async Task<Role> GetByNameAsync(string name)
        {
            return await _dbSet.FirstOrDefaultAsync(r => r.Name == name);
        }
    }
}