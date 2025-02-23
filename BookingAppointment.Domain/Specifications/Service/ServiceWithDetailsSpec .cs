using BookingAppointment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAppointment.Domain.Specifications.ServiceProvider
{

    public class ServiceWithDetailsSpec : BaseSpecification<Service>
    {
        public ServiceWithDetailsSpec(int id) : base(s => s.Id == id)
        {
            

            AddIncludes(s => s.ServiceProviders);
            AddIncludes(s => s.Appointments);
        }

        //public ServiceWithDetailsSpec(List<int> ids)
        //{
        //    Criteria = x => ids.Contains(x.Id);
        //}

    }

}
