using BookingAppointment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAppointment.Domain.Specifications.Schedules
{
    public class ScheduleForDateRangeSpecification : BaseSpecification<Schedule>
    {
        public ScheduleForDateRangeSpecification(DateTime startDate, DateTime endDate, int serviceProviderId)
            : base(s => s.ServiceProviderId == serviceProviderId &&
                        s.DayOfWeek >= startDate.DayOfWeek &&
                        s.DayOfWeek <= endDate.DayOfWeek)
        {
            AddIncludes(s => s.ServiceProvider);
            ApplyOrderBy(s => s.DayOfWeek);
        }
    }
}
