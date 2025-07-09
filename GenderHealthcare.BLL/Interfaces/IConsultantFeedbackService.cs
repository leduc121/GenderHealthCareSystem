using GenderHealthcare.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GenderHealthcare.BLL.Interfaces
{
    public interface IConsultantFeedbackService
    {
        Task<IEnumerable<ConsultantFeedbackDTO>> GetAllFeedbacksAsync();
        Task<ConsultantFeedbackDTO> GetFeedbackByIdAsync(string consultantId, string userId, DateTime feedbackDate);
        Task<ConsultantFeedbackDTO> CreateFeedbackAsync(ConsultantFeedbackDTO feedbackDto);
        Task<ConsultantFeedbackDTO> UpdateFeedbackAsync(ConsultantFeedbackDTO feedbackDto);
        Task<bool> DeleteFeedbackAsync(string consultantId, string userId, DateTime feedbackDate);

        // Thêm phương thức này để UI có thể gọi SubmitFeedbackAsync
        Task<bool> SubmitFeedbackAsync(ConsultantFeedbackDTO feedbackDto);
    }
}
