using BookingAppointment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAppointment.Domain.Specifications.Feedbacks
{
    public class FeedbackWithAppointmentSpec : BaseSpecification<Feedback>
    {
        public FeedbackWithAppointmentSpec(int id)
            : base(f => f.Id == id)
        {
            AddIncludes(f => f.Appointment);
        }
    }
}
