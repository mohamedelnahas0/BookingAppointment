using BookingAppointment.Application.DTOS.Customer;
using BookingAppointment.Application.DTOS.Feedback;
using BookingAppointment.Application.DTOS.Service;
using BookingAppointment.Application.DTOS.ServiceProvider;
using BookingAppointment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAppointment.Application.DTOS.Appointment
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public AppointmentStatus Status { get; set; }
        public CustomerDto Customer { get; set; }
        public ServiceDto Service { get; set; }
        public ServiceProviderDto ServiceProvider { get; set; }
        public FeedbackDto Feedback { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
