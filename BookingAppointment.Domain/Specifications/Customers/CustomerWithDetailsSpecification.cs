using BookingAppointment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAppointment.Domain.Specifications.Customers
{
    public class CustomerWithDetailsSpecification : BaseSpecification<Customer>
    {
        public CustomerWithDetailsSpecification(int id)
            : base(c => c.Id == id)
        {
            AddIncludes(c => c.Notifications);
            AddIncludes(c => c.Orders);
            AddInclude("Orders.OrderItems");
            AddInclude("Orders.DeliveryInfo");
        }

        public CustomerWithDetailsSpecification(string email)
            : base(c => c.Email == email)
        {
            AddIncludes(c => c.Notifications);
        }
    }

}
