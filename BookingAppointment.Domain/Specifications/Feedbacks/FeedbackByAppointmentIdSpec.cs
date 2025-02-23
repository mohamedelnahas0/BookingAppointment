using BookingAppointment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAppointment.Domain.Specifications.Feedbacks
{
    public class FeedbackByAppointmentIdSpec : BaseSpecification<Feedback>
    {
        public FeedbackByAppointmentIdSpec(int appointmentId)
            : base(f => f.AppointmentId == appointmentId)
        {
            AddIncludes(f => f.Appointment);
        }
    }
}
