using AutoMapper;
using BookingAppointment.Application.DTOS.Service;
using BookingAppointment.Application.DTOS.ServiceProvider;
using BookingAppointment.Application.IServices;
using BookingAppointment.Domain.Entities;
using BookingAppointment.Domain.Parameters;
using BookingAppointment.Domain.Specifications.ServiceProvider;
using BookingAppointment.Domain.Unitofwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAppointment.Application.DTOS.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ServiceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ServiceDetailDto> CreateServiceAsync(CreateServiceDto createServiceDto)
        {
            var service = _mapper.Map<BookingAppointment.Domain.Entities.Service>(createServiceDto);
            await _unitOfWork.Repository<BookingAppointment.Domain.Entities.Service, int>().AddAsync(service);
            await _unitOfWork.CompleteAsync();

            var spec = new ServiceWithDetailsSpec(service.Id);
            var createdService = await _unitOfWork.Repository<BookingAppointment.Domain.Entities.Service, int>().GetBySpecAsync(spec);

            return _mapper.Map<ServiceDetailDto>(createdService);
        }

        public async Task<ServiceDto> GetServiceByIdAsync(int id)
        {
            var spec = new ServiceWithDetailsSpec(id);
            var service = await _unitOfWork.Repository<BookingAppointment.Domain.Entities.Service, int>().GetBySpecAsync(spec);

            if (service == null)
                throw new Exception($"Service with ID {id} not found");

            var result = _mapper.Map<ServiceDto>(service);
            return result;
        }


       
        public async Task<IReadOnlyList<ServiceDto>> GetServicesAsync(ServiceSpecParams specParams)
        {
            var spec = new ServiceListSpec(specParams);
            var services = await _unitOfWork.Repository<BookingAppointment.Domain.Entities.Service, int>().ListAsync(spec);

            return _mapper.Map<IReadOnlyList<ServiceDto>>(services);
        }

        public async Task<ServiceDetailDto> UpdateServiceAsync(int id, UpdateServiceDto updateServiceDto)
        {
            var spec = new ServiceWithDetailsSpec(id);
            var service = await _unitOfWork.Repository<BookingAppointment.Domain.Entities.Service, int>().GetBySpecAsync(spec);

            if (service == null)
                throw new Exception($"Service with ID {id} not found");

            _mapper.Map(updateServiceDto, service);
            _unitOfWork.Repository<BookingAppointment.Domain.Entities.Service, int>().Update(service);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<ServiceDetailDto>(service);
        }

        public async Task<bool> DeleteServiceAsync(int id)
        {
            var service = await _unitOfWork.Repository<BookingAppointment.Domain.Entities.Service, int>().GetByIdAsync(id);

            if (service == null)
                throw new Exception($"Service with ID {id} not found");

            _unitOfWork.Repository<BookingAppointment.Domain.Entities.Service, int>().Delete(service);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }

}
