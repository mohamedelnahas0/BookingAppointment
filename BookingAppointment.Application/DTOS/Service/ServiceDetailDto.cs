using BookingAppointment.Application.DTOS.ServiceProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAppointment.Application.DTOS.Service
{
    public class ServiceDetailDto : ServiceDto
    {
        public ICollection<ServiceProviderDto> ServiceProviders { get; set; }
        public int TotalAppointments { get; set; }
    }
}
