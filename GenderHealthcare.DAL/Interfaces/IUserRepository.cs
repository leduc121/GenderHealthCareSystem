using GenderHealthcare.DAL.Entities;
using System.Threading.Tasks;

namespace GenderHealthcare.DAL.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByUsernameAsync(string username);
        Task<User> GetByEmailAsync(string email);
        Task<bool> ValidateLoginAsync(string username, string password);

        Task<User?> GetByIdAsync(string id);
    }
}