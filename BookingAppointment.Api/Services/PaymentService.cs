using AutoMapper;
using BookingAppointment.Application.DTOS.Payments;
using BookingAppointment.Application.IServices;
using BookingAppointment.Domain.Entities;
using BookingAppointment.Domain.Specifications;
using BookingAppointment.Domain.Unitofwork;

namespace BookingAppointment.Api.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PaymentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaymentDto> GetByIdAsync(int id)
        {
            var payment = await _unitOfWork.Repository<Payment, int>().GetByIdAsync(id);
            return _mapper.Map<PaymentDto>(payment);
        }

        public async Task<List<PaymentDto>> GetOrderPaymentsAsync(int orderId)
        {
            var payments = await _unitOfWork.Repository<Payment, int>()
                .ListAsync(new BaseSpecification<Payment>(p => p.OrderId == orderId));
            return _mapper.Map<List<PaymentDto>>(payments);
        }

        public async Task<PaymentDto> ProcessPaymentAsync(int orderId, decimal amount, PaymentMethod method)
        {
            var payment = new Payment
            {
                OrderId = orderId,
                Amount = amount,
                PaymentMethod = method,
                PaymentDate = DateTime.UtcNow,
                Status = PaymentStatus.Pending,
                TransactionId = Guid.NewGuid().ToString()
            };

            try
            {
               
                payment.Status = PaymentStatus.Succeeded;

                await _unitOfWork.Repository<Payment, int>().AddAsync(payment);
                await _unitOfWork.CompleteAsync();

                return _mapper.Map<PaymentDto>(payment);
            }
            catch
            {
                payment.Status = PaymentStatus.Failed;
                await _unitOfWork.Repository<Payment, int>().AddAsync(payment);
                await _unitOfWork.CompleteAsync();
                throw;
            }
        }
    }
}
