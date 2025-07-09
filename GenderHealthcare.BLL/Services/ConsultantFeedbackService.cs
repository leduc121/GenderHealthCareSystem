using GenderHealthcare.BLL.DTOs;
using GenderHealthcare.BLL.Interfaces;
using GenderHealthcare.DAL.Entities;
using GenderHealthcare.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenderHealthcare.BLL.Services
{
    public class ConsultantFeedbackService : IConsultantFeedbackService
    {
        private readonly IConsultantFeedbackRepository _feedbackRepository;

        public ConsultantFeedbackService(IConsultantFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        public async Task<IEnumerable<ConsultantFeedbackDTO>> GetAllFeedbacksAsync()
        {
            var feedbacks = await _feedbackRepository.GetAllAsync();
            return feedbacks.Select(f => MapToDTO(f));
        }

        public async Task<ConsultantFeedbackDTO> GetFeedbackByIdAsync(string consultantId, string userId, DateTime feedbackDate)
        {
            var key = $"{consultantId},{userId},{feedbackDate:yyyy-MM-dd HH:mm:ss}";
            var feedback = await _feedbackRepository.GetByIdAsync(key);
            return feedback != null ? MapToDTO(feedback) : null;
        }

        public async Task<ConsultantFeedbackDTO> CreateFeedbackAsync(ConsultantFeedbackDTO feedbackDto)
        {
            var feedback = MapToEntity(feedbackDto);
            var createdFeedback = await _feedbackRepository.AddAsync(feedback);
            return MapToDTO(createdFeedback);
        }

        public async Task<ConsultantFeedbackDTO> UpdateFeedbackAsync(ConsultantFeedbackDTO feedbackDto)
        {
            var feedback = MapToEntity(feedbackDto);
            var updatedFeedback = await _feedbackRepository.UpdateAsync(feedback);
            return MapToDTO(updatedFeedback);
        }

        public async Task<bool> DeleteFeedbackAsync(string consultantId, string userId, DateTime feedbackDate)
        {
            var key = $"{consultantId},{userId},{feedbackDate:yyyy-MM-dd HH:mm:ss}";
            bool? deleted = await _feedbackRepository.DeleteAsync(key);
            return deleted ?? false;
        }

        // Triển khai phương thức SubmitFeedbackAsync
        public async Task<bool> SubmitFeedbackAsync(ConsultantFeedbackDTO feedbackDto)
        {
            // Map DTO sang Entity
            var entity = MapToEntity(feedbackDto);
            // Gọi repository.AddAsync trả về entity vừa tạo
            var created = await _feedbackRepository.AddAsync(entity);
            // Thành công nếu repository trả về non-null
            return created != null;
        }

        private ConsultantFeedbackDTO MapToDTO(ConsultantFeedback feedback)
        {
            return new ConsultantFeedbackDTO
            {
                ConsultantId = feedback.ConsultantId,
                UserId = feedback.UserId,
                FeedbackDate = feedback.FeedbackDate,
                Rating = feedback.Rating,
                FeedbackContent = feedback.FeedbackContent,
                Status = feedback.Status ?? false,
                UpdatedAt = feedback.UpdatedAt
            };
        }

        private ConsultantFeedback MapToEntity(ConsultantFeedbackDTO feedbackDto)
        {
            return new ConsultantFeedback
            {
                ConsultantId = feedbackDto.ConsultantId,
                UserId = feedbackDto.UserId,
                FeedbackDate = feedbackDto.FeedbackDate,
                Rating = feedbackDto.Rating,
                FeedbackContent = feedbackDto.FeedbackContent,
                Status = feedbackDto.Status,
                UpdatedAt = feedbackDto.UpdatedAt
            };
        }
    }
}
