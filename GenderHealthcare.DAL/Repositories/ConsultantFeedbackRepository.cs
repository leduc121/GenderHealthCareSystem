using GenderHealthcare.DAL.Entities;
using GenderHealthcare.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace GenderHealthcare.DAL.Repositories
{
    public class ConsultantFeedbackRepository : Repository<ConsultantFeedback>, IConsultantFeedbackRepository
    {
        public ConsultantFeedbackRepository(GenderHealthcareContext context) : base(context) { }

        public override async Task<ConsultantFeedback> GetByIdAsync(string id)
        {
            var keys = id.Split(',');
            if (keys.Length != 3) return null;
            var consultantId = keys[0];
            var userId = keys[1];
            if (!DateTime.TryParse(keys[2], out DateTime feedbackDate)) return null;
            return await _dbSet.FirstOrDefaultAsync(f => f.ConsultantId == consultantId && f.UserId == userId && f.FeedbackDate == feedbackDate);
        }

        public override async Task<bool> DeleteAsync(string id)
        {
            var keys = id.Split(',');
            if (keys.Length != 3) return false;
            var consultantId = keys[0];
            var userId = keys[1];
            if (!DateTime.TryParse(keys[2], out DateTime feedbackDate)) return false;

            var feedback = await _dbSet.FirstOrDefaultAsync(f => f.ConsultantId == consultantId && f.UserId == userId && f.FeedbackDate == feedbackDate);
            if (feedback == null) return false;

            _dbSet.Remove(feedback);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}