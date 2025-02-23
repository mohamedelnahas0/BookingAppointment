using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAppointment.Domain.Entities
{
    public class Appointment : BaseEntity<int>
    {
        public int CustomerId { get; set; }
        public int ServiceId { get; set; }
        public int ServiceProviderId { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public AppointmentStatus Status { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Service Service { get; set; }
        public virtual ServiceProvider ServiceProvider { get; set; }
        public virtual Feedback? Feedback { get; set; }

    }

    public enum AppointmentStatus
    {
        Scheduled,
        Confirmed,
        Completed,
        Cancelled
    }
}
