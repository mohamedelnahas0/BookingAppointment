using BookingAppointment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAppointment.Domain.Specifications.Customers
{
    public class CustomerNotificationsSpecification : BaseSpecification<Notification>
    {
        public CustomerNotificationsSpecification(int customerId, bool unreadOnly = false)
            : base(n => n.CustomerId == customerId && (!unreadOnly || !n.IsRead))
        {
            ApplyOrderByDescending(n => n.CreatedAt);
        }
    }
}
