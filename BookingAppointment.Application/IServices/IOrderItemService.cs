using BookingAppointment.Application.DTOS.OrderItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAppointment.Application.IServices
{
    public interface IOrderItemService
    {
        Task<OrderItemDetailDto> GetByIdAsync(int id);
        Task<IEnumerable<OrderItemDetailDto>> GetByOrderIdAsync(int orderId);
        Task<OrderItemDetailDto> CreateAsync(CreateOrderItemDto createDto);
        Task<OrderItemDetailDto> UpdateQuantityAsync(int id, UpdateOrderItemDto updateDto);
        Task<bool> DeleteAsync(int id);
        Task<decimal> CalculateOrderTotalAsync(int orderId);
    }
}
