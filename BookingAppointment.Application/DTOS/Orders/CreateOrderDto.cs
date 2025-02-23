using BookingAppointment.Application.DTOS.DeliveryInfos;
using BookingAppointment.Application.DTOS.OrderItems;
using BookingAppointment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAppointment.Application.DTOS.Orderss
{
    public class CreateOrderDto
    {
        public int CustomerId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public List<OrderItemCreateDto> OrderItems { get; set; }
        public DeliveryInfoCreateDto DeliveryInfo { get; set; }
    }
}
