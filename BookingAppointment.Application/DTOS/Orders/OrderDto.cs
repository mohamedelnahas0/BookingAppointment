using BookingAppointment.Application.DTOS.DeliveryInfos;
using BookingAppointment.Application.DTOS.OrderItems;
using BookingAppointment.Application.DTOS.Payments;
using BookingAppointment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAppointment.Application.DTOS.Orderss
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalAmount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
        public DeliveryInfoDto DeliveryInfo { get; set; }
        public List<PaymentDto> Payments { get; set; }
    }
}
