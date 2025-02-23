using BookingAppointment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAppointment.Domain.Specifications.Orders
{
    public class OrderWithDetailsSpecification : BaseSpecification<Order>
    {
        public OrderWithDetailsSpecification(int id)
            : base(o => o.Id == id)
        {
            AddIncludes(o => o.Customer);
            AddIncludes(o => o.OrderItems);
            AddIncludes(o => o.DeliveryInfo);
            AddIncludes(o => o.Payments);
            AddInclude("OrderItems.Product");
        }

        public OrderWithDetailsSpecification(int customerId, bool onlyActive = true)
            : base(o => o.CustomerId == customerId && (!onlyActive || o.IsActive))
        {
            ApplyOrderByDescending(o => o.OrderDate);
            AddIncludes(o => o.OrderItems);
            AddIncludes(o => o.DeliveryInfo);
            AddInclude("OrderItems.Product");
        }
    }

}
