using BookingAppointment.Application.DTOS.Payments;
using BookingAppointment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAppointment.Application.IServices
{
    public interface IPaymentService
    {
        Task<PaymentDto> GetByIdAsync(int id);
        Task<List<PaymentDto>> GetOrderPaymentsAsync(int orderId);
        Task<PaymentDto> ProcessPaymentAsync(int orderId, decimal amount, PaymentMethod method);
    }
}
