using GenderHealthcare.DAL.Entities;
using System.Threading.Tasks;

namespace GenderHealthcare.DAL.Interfaces
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<Role> GetByNameAsync(string name);
    }
}