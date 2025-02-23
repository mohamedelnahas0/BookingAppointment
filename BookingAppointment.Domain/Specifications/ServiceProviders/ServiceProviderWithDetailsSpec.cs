using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingAppointment.Domain.Entities;

namespace BookingAppointment.Domain.Specifications.ServiceProviders
{
    public class ServiceProviderWithDetailsSpec : BaseSpecification<BookingAppointment.Domain.Entities.ServiceProvider>
    {
        public ServiceProviderWithDetailsSpec(int id) : base(sp => sp.Id == id)
        {
            AddIncludes(sp => sp.Services);
            AddIncludes(sp => sp.Appointments);
            AddIncludes(sp => sp.Schedule);
        }
     
    }
}
