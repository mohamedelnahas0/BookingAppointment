using BookingAppointment.Domain.Entities;

namespace BookingAppointment.Api.Extensions.RequestModelExtensions
{
    public class ProcessPaymentRequest
    {
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
