using AutoMapper;
using BookingAppointment.Application.DTOS.DeliveryInfos;
using BookingAppointment.Application.IServices;
using BookingAppointment.Domain.Entities;
using BookingAppointment.Domain.Unitofwork;

namespace BookingAppointment.Api.Services
{
    public class DeliveryService : IDeliveryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeliveryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DeliveryInfoDto> GetByIdAsync(int id)
        {
            var delivery = await _unitOfWork.Repository<DeliveryInfo, int>().GetByIdAsync(id);
            return _mapper.Map<DeliveryInfoDto>(delivery);
        }

        public async Task<DeliveryInfoDto> UpdateStatusAsync(int id, DeliveryStatus status)
        {
            var delivery = await _unitOfWork.Repository<DeliveryInfo, int>().GetByIdAsync(id);
            if (delivery == null) throw new Exception("Delivery info not found");

            delivery.Status = status;
            _unitOfWork.Repository<DeliveryInfo, int>().Update(delivery);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<DeliveryInfoDto>(delivery);
        }
    }

}
