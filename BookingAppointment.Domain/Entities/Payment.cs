using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAppointment.Domain.Entities
{
    public class Payment : BaseEntity<int>
    {
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public PaymentStatus Status { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public DateTime PaymentDate { get; set; }
        public string? TransactionId { get; set; }

        public virtual Order Order { get; set; }
    }
    public enum PaymentStatus
    {
        Pending,
        Succeeded,
        Failed,
        Refunded,
        Cancelled
    }

    public enum PaymentMethod
    {
        Cash,
        CreditCard,
        DebitCard,
        OnlinePayment
    }

}
