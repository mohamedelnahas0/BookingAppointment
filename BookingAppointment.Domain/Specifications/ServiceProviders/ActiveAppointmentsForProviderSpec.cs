using BookingAppointment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAppointment.Domain.Specifications.ServiceProviders
{
    public class ActiveAppointmentsForProviderSpec : BaseSpecification<Appointment>
    {
        public ActiveAppointmentsForProviderSpec(int providerId)
            : base(a => a.ServiceProviderId == providerId &&
                   (a.Status == AppointmentStatus.Scheduled || a.Status == AppointmentStatus.Confirmed))
        {
        }
    }
}
