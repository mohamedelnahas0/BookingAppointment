using BookingAppointment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAppointment.Domain.Specifications.ServiceProviders
{
    public class FutureAppointmentsForProviderAndServiceSpec : BaseSpecification<Appointment>
    {
        public FutureAppointmentsForProviderAndServiceSpec(int providerId, int serviceId)
            : base(a => a.ServiceProviderId == providerId &&
                   a.ServiceId == serviceId &&
                   a.AppointmentDateTime > DateTime.UtcNow)
        {
        }
    }

}
