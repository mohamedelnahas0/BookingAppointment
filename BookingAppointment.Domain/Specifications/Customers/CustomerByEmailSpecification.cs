using BookingAppointment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAppointment.Domain.Specifications.Customers
{
    public class CustomerByEmailSpecification : BaseSpecification<Customer>
    {
        public CustomerByEmailSpecification(string email)
            : base(c => c.Email == email)
        {
        }
    }
}
