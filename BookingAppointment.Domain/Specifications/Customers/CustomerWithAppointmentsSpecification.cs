using BookingAppointment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAppointment.Domain.Specifications.Customers
{
    public class CustomerWithAppointmentsSpecification : BaseSpecification<Customer>
    {
        public CustomerWithAppointmentsSpecification(int customerId)
            : base(c => c.Id == customerId)
        {
            AddIncludes(c => c.Appointments);
        }
    }
}
