using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAppointment.Application.DTOS.Appointment
{
    public class CreateAppointmentDto
    {
        public int CustomerId { get; set; }
        public int ServiceId { get; set; }
        public int ServiceProviderId { get; set; }
        public DateTime AppointmentDateTime { get; set; }
    }
}
