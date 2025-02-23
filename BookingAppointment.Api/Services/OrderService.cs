using AutoMapper;
using BookingAppointment.Application.DTOS.Orderss;
using BookingAppointment.Application.IServices;
using BookingAppointment.Domain.Entities;
using BookingAppointment.Domain.Specifications.Orders;
using BookingAppointment.Domain.Unitofwork;

namespace BookingAppointment.Api.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OrderDto> GetByIdAsync(int id)
        {
            var spec = new OrderWithDetailsSpecification(id);
            var order = await _unitOfWork.Repository<Order, int>().GetBySpecAsync(spec);
            return _mapper.Map<OrderDto>(order);
        }

        public async Task<List<OrderDto>> GetCustomerOrdersAsync(int customerId)
        {
            var spec = new OrderWithDetailsSpecification(customerId);
            var orders = await _unitOfWork.Repository<Order, int>().ListAsync(spec);
            return _mapper.Map<List<OrderDto>>(orders);
        }

        public async Task<OrderDto> CreateOrderAsync(CreateOrderDto orderDto)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var order = new Order
                {
                    CustomerId = orderDto.CustomerId,
                    OrderDate = DateTime.UtcNow,
                    Status = OrderStatus.Pending,
                    PaymentMethod = orderDto.PaymentMethod
                };

                var orderItems = new List<OrderItem>();
                decimal totalAmount = 0;

                foreach (var item in orderDto.OrderItems)
                {
                    var product = await _unitOfWork.Repository<Product, int>()
                        .GetByIdAsync(item.ProductId);

                    if (product == null || product.StockQuantity < item.Quantity)
                        throw new InvalidOperationException($"Insufficient stock for product {item.ProductId}");

                    var orderItem = new OrderItem
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = product.Price
                    };

                    orderItems.Add(orderItem);
                    totalAmount += orderItem.UnitPrice * orderItem.Quantity;

                    product.StockQuantity -= item.Quantity;
                    _unitOfWork.Repository<Product, int>().Update(product);
                }

                order.OrderItems = orderItems;
                order.TotalAmount = totalAmount;

                var deliveryInfo = new DeliveryInfo
                {
                    Address = orderDto.DeliveryInfo.Address,
                    Status = DeliveryStatus.Pending,
                    EstimatedDeliveryDate = DateTime.UtcNow.AddDays(5)
                };
                order.DeliveryInfo = deliveryInfo;

                await _unitOfWork.Repository<Order, int>().AddAsync(order);
                await _unitOfWork.CompleteAsync();
                await _unitOfWork.CommitTransactionAsync();

                return await GetByIdAsync(order.Id);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<OrderDto> UpdateOrderStatusAsync(int id, OrderStatus status)
        {
            var order = await _unitOfWork.Repository<Order, int>().GetByIdAsync(id);
            if (order == null) throw new Exception("Order not found");

            order.Status = status;
            _unitOfWork.Repository<Order, int>().Update(order);
            await _unitOfWork.CompleteAsync();

            return await GetByIdAsync(id);
        }

        public async Task<OrderDto> ProcessPaymentAsync(int orderId, decimal amount)
        {
            var order = await _unitOfWork.Repository<Order, int>().GetByIdAsync(orderId);
            if (order == null) throw new Exception("Order not found");

            var payment = new Payment
            {
                OrderId = orderId,
                Amount = amount,
                PaymentMethod = order.PaymentMethod,
                PaymentDate = DateTime.UtcNow,
                Status = PaymentStatus.Succeeded,
                TransactionId = Guid.NewGuid().ToString()
            };

            order.Status = OrderStatus.Paid;

            await _unitOfWork.Repository<Payment, int>().AddAsync(payment);
            _unitOfWork.Repository<Order, int>().Update(order);
            await _unitOfWork.CompleteAsync();

            return await GetByIdAsync(orderId);
        }

        public async Task<OrderDto> UpdateDeliveryStatusAsync(int orderId, DeliveryStatus status, string trackingNumber)
        {
            var order = await _unitOfWork.Repository<Order, int>().GetByIdAsync(orderId);
            if (order == null) throw new Exception("Order not found");

            var deliveryInfo = await _unitOfWork.Repository<DeliveryInfo, int>()
                .FirstOrDefaultAsync(d => d.OrderId == orderId);

            if (deliveryInfo == null) throw new Exception("Delivery info not found");

            deliveryInfo.Status = status;
            deliveryInfo.TrackingNumber = trackingNumber;

            _unitOfWork.Repository<DeliveryInfo, int>().Update(deliveryInfo);
            await _unitOfWork.CompleteAsync();

            return await GetByIdAsync(orderId);
        }
    }
}
