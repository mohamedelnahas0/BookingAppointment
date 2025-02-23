using BookingAppointment.Application.DTOS.Appointment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAppointment.Application.DTOS.Feedback
{
    public class FeedbackDetailDto
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Appointment { get; set; }
    }
}
