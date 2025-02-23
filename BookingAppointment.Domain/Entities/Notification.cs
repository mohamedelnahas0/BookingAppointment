using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAppointment.Domain.Entities
{
    public class Notification : BaseEntity<int>
    {
        public int CustomerId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public NotificationType Type { get; set; }
        public bool IsRead { get; set; }
        public DateTime? ReadAt { get; set; }

        public virtual Customer Customer { get; set; }
    }
    public enum NotificationType
    {
        AppointmentReminder,
        AppointmentConfirmation,
        AppointmentCancellation,
        OrderStatus,
        DeliveryUpdate,
        SpecialOffer,
        GeneralAnnouncement
    }
}
