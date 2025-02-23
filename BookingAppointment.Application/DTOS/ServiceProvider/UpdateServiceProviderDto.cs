using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAppointment.Application.DTOS.ServiceProvider
{
    public class UpdateServiceProviderDto
    {
       
        public List<int> ServiceIds { get; set; } = new();
    }
}
