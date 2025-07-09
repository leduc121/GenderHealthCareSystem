using GenderHealthcare.BLL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GenderHealthcare.BLL.Interfaces
{
    public interface IContraceptiveReminderService
    {
        Task CreateReminderAsync(ContraceptiveReminderDTO dto);
        Task<IEnumerable<ContraceptiveReminderDTO>> GetAllActiveRemindersAsync();
        Task<IEnumerable<ContraceptiveReminderDTO>> GetActiveRemindersByUserAsync(string userId);
        Task UpdateReminderAsync(ContraceptiveReminderDTO dto);
    }
}
