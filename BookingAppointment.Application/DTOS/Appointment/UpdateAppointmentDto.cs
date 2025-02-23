using BookingAppointment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAppointment.Application.DTOS.Appointment
{
    public class UpdateAppointmentDto
    {
        public DateTime AppointmentDateTime { get; set; }
        public AppointmentStatus Status { get; set; }
    }
}
