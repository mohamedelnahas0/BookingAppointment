using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAppointment.Application.DTOS.Schedule
{
    public class ScheduleDto
    {
        public int Id { get; set; }  // Add this if not present
        public int ServiceProviderId { get; set; }
        public int DayOfWeek { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }

    }
}
