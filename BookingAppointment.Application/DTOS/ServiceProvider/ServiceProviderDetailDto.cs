using BookingAppointment.Application.DTOS.Schedule;
using BookingAppointment.Application.DTOS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAppointment.Application.DTOS.ServiceProvider
{
    public class ServiceProviderDetailDto : ServiceProviderDto
    {
        public ICollection<ServiceDto> Services { get; set; } = new List<ServiceDto>();
        public ScheduleDto Schedule { get; set; }
        public int TotalAppointments { get; set; }
        public int CompletedAppointments { get; set; }
        public decimal Rating { get; set; }
    }
}
