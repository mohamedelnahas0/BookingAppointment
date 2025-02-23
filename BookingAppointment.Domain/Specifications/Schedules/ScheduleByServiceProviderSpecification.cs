using BookingAppointment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAppointment.Domain.Specifications.Schedules
{
    public class ScheduleByServiceProviderSpecification : BaseSpecification<Schedule>
    {
        public ScheduleByServiceProviderSpecification(int serviceProviderId)
            : base(s => s.ServiceProviderId == serviceProviderId)
        {
            AddIncludes(s => s.ServiceProvider);
        }
    }
}
