using System;

namespace GenderHealthcare.BLL.DTOs
{
    public class ConsultantFeedbackDTO
    {
        public string ConsultantId { get; set; }
        public string UserId { get; set; }
        public DateTime FeedbackDate { get; set; }
        public int Rating { get; set; }
        public string FeedbackContent { get; set; }
        public bool? Status { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}