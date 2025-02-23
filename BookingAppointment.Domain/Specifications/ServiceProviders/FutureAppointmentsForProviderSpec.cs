using BookingAppointment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAppointment.Domain.Specifications.ServiceProviders
{
    public class FutureAppointmentsForProviderSpec : BaseSpecification<Appointment>
    {
        public FutureAppointmentsForProviderSpec(int providerId)
            : base(a => a.ServiceProviderId == providerId && a.AppointmentDateTime > DateTime.UtcNow)
        {
        }
    }
}
