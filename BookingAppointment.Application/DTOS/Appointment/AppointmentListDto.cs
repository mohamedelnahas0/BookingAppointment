using BookingAppointment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAppointment.Application.DTOS.Appointment
{
    public class AppointmentListDto
    {
        public int Id { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public AppointmentStatus Status { get; set; }
        public string CustomerName { get; set; }
        public string ServiceName { get; set; }
        public string ServiceProviderName { get; set; }
    }
}
