using GenderHealthcare.BLL.DTOs;
using GenderHealthcare.BLL.Interfaces;
using GenderHealthcare.DAL.Entities;
using GenderHealthcare.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenderHealthcare.BLL.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<IEnumerable<PaymentDTO>> GetAllPaymentsAsync()
        {
            var payments = await _paymentRepository.GetAllAsync();
            return payments.Select(p => MapToDTO(p));
        }

        public async Task<PaymentDTO> GetPaymentByIdAsync(string id)
        {
            var payment = await _paymentRepository.GetByIdAsync(id);
            return payment != null ? MapToDTO(payment) : null;
        }

        public async Task<PaymentDTO> CreatePaymentAsync(PaymentDTO paymentDto)
        {
            var payment = MapToEntity(paymentDto);
            var createdPayment = await _paymentRepository.AddAsync(payment);
            return MapToDTO(createdPayment);
        }

        public async Task<PaymentDTO> UpdatePaymentAsync(PaymentDTO paymentDto)
        {
            var payment = MapToEntity(paymentDto);
            var updatedPayment = await _paymentRepository.UpdateAsync(payment);
            return MapToDTO(updatedPayment);
        }

        public async Task<bool> DeletePaymentAsync(string id)
        {
            return await _paymentRepository.DeleteAsync(id);
        }

        private PaymentDTO MapToDTO(Payment payment)
        {
            return new PaymentDTO
            {
                Id = payment.Id,
                UserId = payment.UserId,
                AppointmentUserId = payment.AppointmentUserId,
                AppointmentConsultantId = payment.AppointmentConsultantId,
                AppointmentDate = payment.AppointmentDate,
                OfferId = payment.OfferId,
                OriginalAmount = payment.OriginalAmount,
                DiscountAmount = payment.DiscountAmount,
                Amount = payment.Amount,
                PaymentMethod = payment.PaymentMethod,
                PaymentStatus = payment.PaymentStatus,
                PaymentDate = payment.PaymentDate,
                Status = payment.Status,
                UpdatedAt = payment.UpdatedAt
            };
        }

        private Payment MapToEntity(PaymentDTO paymentDto)
        {
            return new Payment
            {
                Id = paymentDto.Id,
                UserId = paymentDto.UserId,
                AppointmentUserId = paymentDto.AppointmentUserId,
                AppointmentConsultantId = paymentDto.AppointmentConsultantId,
                AppointmentDate = paymentDto.AppointmentDate,
                OfferId = paymentDto.OfferId,
                OriginalAmount = paymentDto.OriginalAmount,
                DiscountAmount = paymentDto.DiscountAmount,
                Amount = paymentDto.Amount,
                PaymentMethod = paymentDto.PaymentMethod,
                PaymentStatus = paymentDto.PaymentStatus,
                PaymentDate = paymentDto.PaymentDate,
                Status = paymentDto.Status,
                UpdatedAt = paymentDto.UpdatedAt
            };
        }
    }
}