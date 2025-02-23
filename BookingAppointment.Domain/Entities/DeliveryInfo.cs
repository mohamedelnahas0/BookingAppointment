using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAppointment.Domain.Entities
{
    public class DeliveryInfo : BaseEntity<int>
    {
        public int OrderId { get; set; }
        public string Address { get; set; }
        public string? TrackingNumber { get; set; }
        public DeliveryStatus Status { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }

        public virtual Order Order { get; set; }
    }
    public enum DeliveryStatus
    {
        Pending,
        InTransit,
        Delivered,
        Failed
    }
}
