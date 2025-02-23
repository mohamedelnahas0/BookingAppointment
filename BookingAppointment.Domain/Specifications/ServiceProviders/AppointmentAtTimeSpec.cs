using BookingAppointment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAppointment.Domain.Specifications.ServiceProviders
{
    public class AppointmentAtTimeSpec : BaseSpecification<Appointment>
    {
        public AppointmentAtTimeSpec(int providerId, DateTime dateTime)
            : base(a => a.ServiceProviderId == providerId &&
                   a.AppointmentDateTime == dateTime)
        {
        }
    }
}
