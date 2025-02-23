using BookingAppointment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAppointment.Domain.Specifications.Appointments
{
    public class AppointmentWithDetailsSpec : BaseSpecification<Appointment>
    {
        public AppointmentWithDetailsSpec(int id) : base(a => a.Id == id)
        {
            AddIncludes(a => a.Customer);
            AddIncludes(a => a.Service);
            AddIncludes(a => a.ServiceProvider);
            AddIncludes(a => a.Feedback);
        }
    }
}
