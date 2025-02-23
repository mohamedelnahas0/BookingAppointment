using BookingAppointment.Application.DTOS.Orderss;
using BookingAppointment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAppointment.Application.IServices
{
  
        public interface IOrderService
        {
            Task<OrderDto> GetByIdAsync(int id);
            Task<List<OrderDto>> GetCustomerOrdersAsync(int customerId);
            Task<OrderDto> CreateOrderAsync(CreateOrderDto orderDto);
            Task<OrderDto> UpdateOrderStatusAsync(int id, OrderStatus status);
            Task<OrderDto> ProcessPaymentAsync(int orderId, decimal amount);
            Task<OrderDto> UpdateDeliveryStatusAsync(int orderId, DeliveryStatus status, string trackingNumber);
        }


    }
