using AutoMapper;
using BookingAppointment.Application.DTOS.OrderItems;
using BookingAppointment.Application.IServices;
using BookingAppointment.Domain.Entities;
using BookingAppointment.Domain.Specifications.OrderItems;
using BookingAppointment.Domain.Unitofwork;

namespace BookingAppointment.Api.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderItemService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OrderItemDetailDto> GetByIdAsync(int id)
        {
            var spec = new OrderItemSpecification(id);
            var orderItem = await _unitOfWork.Repository<OrderItem, int>().GetBySpecAsync(spec);
            return _mapper.Map<OrderItemDetailDto>(orderItem);
        }

        public async Task<IEnumerable<OrderItemDetailDto>> GetByOrderIdAsync(int orderId)
        {
            var spec = new OrderItemSpecification(orderId);
            var orderItems = await _unitOfWork.Repository<OrderItem, int>().ListAsync(spec);
            return _mapper.Map<IEnumerable<OrderItemDetailDto>>(orderItems);
        }

        public async Task<OrderItemDetailDto> CreateAsync(CreateOrderItemDto createDto)
        {
            try
            {
                // Verify Order exists and is in valid state
                var order = await _unitOfWork.Repository<Order, int>().GetByIdAsync(createDto.OrderId);
                if (order == null)
                    throw new Exception("Order not found");

                if (order.Status != OrderStatus.Pending)
                    throw new InvalidOperationException("Cannot modify order items for non-pending orders");

                // Get product and verify stock
                var product = await _unitOfWork.Repository<Product, int>().GetByIdAsync(createDto.ProductId);
                if (product == null)
                    throw new Exception("Product not found");

                if (product.StockQuantity < createDto.Quantity)
                    throw new InvalidOperationException("Insufficient stock available");

                // Create order item
                var orderItem = new OrderItem
                {
                    OrderId = createDto.OrderId,
                    ProductId = createDto.ProductId,
                    Quantity = createDto.Quantity,
                    UnitPrice = product.Price
                };

                // Update product stock
                product.StockQuantity -= createDto.Quantity;
                _unitOfWork.Repository<Product, int>().Update(product);

                // Add order item
                await _unitOfWork.Repository<OrderItem, int>().AddAsync(orderItem);

                // Update order total
                order.TotalAmount = await CalculateOrderTotalAsync(order.Id);
                _unitOfWork.Repository<Order, int>().Update(order);

                await _unitOfWork.CompleteAsync();
                await _unitOfWork.CommitTransactionAsync();

                return await GetByIdAsync(orderItem.Id);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<OrderItemDetailDto> UpdateQuantityAsync(int id, UpdateOrderItemDto updateDto)
        {
            try
            {
                var spec = new OrderItemSpecification(id);
                var orderItem = await _unitOfWork.Repository<OrderItem, int>().GetBySpecAsync(spec);
                if (orderItem == null)
                    throw new Exception("Order item not found");

                var order = await _unitOfWork.Repository<Order, int>().GetByIdAsync(orderItem.OrderId);
                if (order.Status != OrderStatus.Pending)
                    throw new InvalidOperationException("Cannot modify items for non-pending orders");

                var product = await _unitOfWork.Repository<Product, int>().GetByIdAsync(orderItem.ProductId);

                // Calculate stock difference
                var quantityDifference = updateDto.Quantity - orderItem.Quantity;
                if (quantityDifference > 0 && product.StockQuantity < quantityDifference)
                    throw new InvalidOperationException("Insufficient stock available");

                // Update product stock
                product.StockQuantity -= quantityDifference;
                _unitOfWork.Repository<Product, int>().Update(product);

                // Update order item
                orderItem.Quantity = updateDto.Quantity;
                _unitOfWork.Repository<OrderItem, int>().Update(orderItem);

                // Update order total
                order.TotalAmount = await CalculateOrderTotalAsync(order.Id);
                _unitOfWork.Repository<Order, int>().Update(order);

                await _unitOfWork.CompleteAsync();
                await _unitOfWork.CommitTransactionAsync();

                return await GetByIdAsync(orderItem.Id);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var spec = new OrderItemSpecification(id);
                var orderItem = await _unitOfWork.Repository<OrderItem, int>().GetBySpecAsync(spec);
                if (orderItem == null)
                    return false;

                var order = await _unitOfWork.Repository<Order, int>().GetByIdAsync(orderItem.OrderId);
                if (order.Status != OrderStatus.Pending)
                    throw new InvalidOperationException("Cannot delete items from non-pending orders");

                // Restore product stock
                var product = await _unitOfWork.Repository<Product, int>().GetByIdAsync(orderItem.ProductId);
                product.StockQuantity += orderItem.Quantity;
                _unitOfWork.Repository<Product, int>().Update(product);

                // Delete order item
                _unitOfWork.Repository<OrderItem, int>().Delete(orderItem);

                // Update order total
                order.TotalAmount = await CalculateOrderTotalAsync(order.Id);
                _unitOfWork.Repository<Order, int>().Update(order);

                await _unitOfWork.CompleteAsync();
                await _unitOfWork.CommitTransactionAsync();
                return true;
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<decimal> CalculateOrderTotalAsync(int orderId)
        {
            var spec = new OrderItemSpecification(orderId);
            var orderItems = await _unitOfWork.Repository<OrderItem, int>().ListAsync(spec);
            return orderItems.Sum(item => item.UnitPrice * item.Quantity);
        }
    }

   
   

}
