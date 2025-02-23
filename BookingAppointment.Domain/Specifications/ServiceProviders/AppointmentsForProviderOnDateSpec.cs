using BookingAppointment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAppointment.Domain.Specifications.ServiceProviders
{
    public class AppointmentsForProviderOnDateSpec : BaseSpecification<Appointment>
    {
        public AppointmentsForProviderOnDateSpec(int providerId, DateTime date)
            : base(a => a.ServiceProviderId == providerId &&
                   a.AppointmentDateTime.Date == date.Date)
        {
        }
    }
}
