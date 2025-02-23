using BookingAppointment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAppointment.Domain.Specifications.OrderItems
{
    public class OrderItemSpecification : BaseSpecification<OrderItem>
    {
        public OrderItemSpecification(int id)
            : base(oi => oi.Id == id)
        {
            AddIncludes(oi => oi.Product);
            AddIncludes(oi => oi.Order);
        }

        public OrderItemSpecification(int orderId, bool activeOnly = true)
            : base(oi => oi.OrderId == orderId && (!activeOnly || oi.IsActive))
        {
            AddIncludes(oi => oi.Product);
            ApplyOrderBy(oi => oi.CreatedAt);
        }
    }
}
