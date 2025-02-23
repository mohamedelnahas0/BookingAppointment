using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAppointment.Domain.Entities
{
    public class Feedback : BaseEntity<int>
    {
        public int AppointmentId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }

        public virtual Appointment Appointment { get; set; }
    }
}
