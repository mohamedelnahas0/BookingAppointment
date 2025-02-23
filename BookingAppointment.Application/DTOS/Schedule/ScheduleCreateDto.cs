using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAppointment.Application.DTOS.Schedule
{
    public class ScheduleCreateDto
    {
        [Required]
        public int ServiceProviderId { get; set; }

        [Required]
        [Range(0, 6)] // DayOfWeek is 0-6
        public DayOfWeek DayOfWeek { get; set; }

        [Required]
        [RegularExpression(@"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]:[0-5][0-9]$")]
        public TimeSpan StartTime { get; set; }

        [Required]
        [RegularExpression(@"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]:[0-5][0-9]$")]
        public TimeSpan EndTime { get; set; }
    }
}
