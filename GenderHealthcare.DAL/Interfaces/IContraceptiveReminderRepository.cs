using GenderHealthcare.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GenderHealthcare.DAL.Interfaces
{
    public interface IContraceptiveReminderRepository
    {
        Task AddAsync(ContraceptiveReminder r);
        Task<List<ContraceptiveReminder>> GetAllActiveAsync();
        Task<List<ContraceptiveReminder>> GetByUserAsync(string userId);
        Task UpdateAsync(ContraceptiveReminder r);
    }
}
