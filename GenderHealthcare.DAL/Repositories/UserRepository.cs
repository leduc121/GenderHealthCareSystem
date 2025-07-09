using GenderHealthcare.DAL.Entities;
using GenderHealthcare.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GenderHealthcare.DAL.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(GenderHealthcareContext context) : base(context) { }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> ValidateLoginAsync(string username, string password)
        {
            var user = await GetByUsernameAsync(username);
            return user != null && user.Password == password; // Nên hash password
        }


    }
}