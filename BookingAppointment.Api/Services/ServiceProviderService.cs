using AutoMapper;
using BookingAppointment.Application.DTOS.ServiceProvider;
using BookingAppointment.Application.IServices;
using BookingAppointment.Domain.Entities;
using BookingAppointment.Domain.Parameters;
using BookingAppointment.Domain.Specifications.ServiceProviders;
using BookingAppointment.Domain.Unitofwork;
using BookingAppointment.Application.DTOS.Schedule;
using System.ComponentModel.DataAnnotations;

namespace BookingAppointment.Api.Services
{
    public class ServiceProviderService : IServiceProviderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ServiceProviderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ServiceProviderDetailDto> CreateServiceProviderAsync(CreateServiceProviderDto createServiceProviderDto)
        {
            var serviceProvider = _mapper.Map<Domain.Entities.ServiceProvider>(createServiceProviderDto);

            if (createServiceProviderDto.ServiceIds?.Any() == true)
            {
                foreach (var serviceId in createServiceProviderDto.ServiceIds)
                {
                    var service = await _unitOfWork.Repository<Service, int>().GetByIdAsync(serviceId);
                    if (service != null)
                    {
                        serviceProvider.Services.Add(service);
                    }
                }
            }

            await _unitOfWork.Repository<Domain.Entities.ServiceProvider, int>().AddAsync(serviceProvider);
            await _unitOfWork.CompleteAsync();

            var spec = new ServiceProviderWithDetailsSpec(serviceProvider.Id);
            var createdServiceProvider = await _unitOfWork.Repository<Domain.Entities.ServiceProvider, int>().GetBySpecAsync(spec);

            return _mapper.Map<ServiceProviderDetailDto>(createdServiceProvider);
        }


     

        public async Task<ServiceProviderDetailDto> GetServiceProviderByIdAsync(int id)
        {
            var spec = new ServiceProviderWithDetailsSpec(id);
            var serviceProvider = await _unitOfWork.Repository<Domain.Entities.ServiceProvider, int>().GetBySpecAsync(spec);

            if (serviceProvider == null)
                throw new Exception($"ServiceProvider with ID {id} not found");

            return _mapper.Map<ServiceProviderDetailDto>(serviceProvider);
        }

        public async Task<IReadOnlyList<ServiceProviderDto>> GetServiceProvidersAsync(ServiceProviderSpecParams specParams)
        {
            var spec = new ServiceProviderListSpec(specParams);
            var serviceProviders = await _unitOfWork.Repository<Domain.Entities.ServiceProvider, int>().ListAsync(spec);

            return _mapper.Map<IReadOnlyList<ServiceProviderDto>>(serviceProviders);
        }




        public async Task<ServiceProviderDetailDto> UpdateServiceProviderAsync(int id, UpdateServiceProviderDto updateServiceProviderDto)
        {
            // Get service provider with existing details
            var spec = new ServiceProviderWithDetailsSpec(id);
            var serviceProvider = await _unitOfWork.Repository<Domain.Entities.ServiceProvider, int>().GetBySpecAsync(spec);

            if (serviceProvider == null)
                throw new ($"ServiceProvider with ID {id} not found");

            // Update services if new serviceIds are provided
            if (updateServiceProviderDto.ServiceIds?.Any() == true)
            {
                // Clear existing services
                serviceProvider.Services.Clear();

                // Get each service individually since we don't have a specification for multiple IDs
                foreach (var serviceId in updateServiceProviderDto.ServiceIds)
                {
                    var service = await _unitOfWork.Repository<Service, int>().GetByIdAsync(serviceId);
                    if (service == null)
                    {
                        throw new ValidationException($"Service with ID {serviceId} not found");
                    }
                    serviceProvider.Services.Add(service);
                }
            }

            // Save changes
            _unitOfWork.Repository<Domain.Entities.ServiceProvider, int>().Update(serviceProvider);
            await _unitOfWork.CompleteAsync();

            // Fetch updated service provider with all details
            var updatedSpec = new ServiceProviderWithDetailsSpec(id);
            var updatedServiceProvider = await _unitOfWork.Repository<Domain.Entities.ServiceProvider, int>()
                .GetBySpecAsync(updatedSpec);

            return _mapper.Map<ServiceProviderDetailDto>(updatedServiceProvider);
        }

        public async Task<bool> DeleteServiceProviderAsync(int id)
        {
            var serviceProvider = await _unitOfWork.Repository<Domain.Entities.ServiceProvider, int>().GetByIdAsync(id);

            if (serviceProvider == null)
                throw new Exception($"ServiceProvider with ID {id} not found");

            _unitOfWork.Repository<Domain.Entities.ServiceProvider, int>().Delete(serviceProvider);
            await _unitOfWork.CompleteAsync();

            return true;
        }

        public async Task<ScheduleDto> GetServiceProviderScheduleAsync(int id)
        {
            var spec = new ServiceProviderWithDetailsSpec(id);
            var serviceProvider = await _unitOfWork.Repository<Domain.Entities.ServiceProvider, int>().GetBySpecAsync(spec);

            if (serviceProvider == null)
                throw new Exception($"ServiceProvider with ID {id} not found");

            return _mapper.Map<ScheduleDto>(serviceProvider.Schedule);
        }

        public async Task<ScheduleDto> UpdateServiceProviderScheduleAsync(int id, ScheduleDto scheduleDto)
        {
            var spec = new ServiceProviderWithDetailsSpec(id);
            var serviceProvider = await _unitOfWork.Repository<Domain.Entities.ServiceProvider, int>().GetBySpecAsync(spec);

            if (serviceProvider == null)
                throw new Exception($"ServiceProvider with ID {id} not found");

            var schedule = _mapper.Map<Schedule>(scheduleDto);
            serviceProvider.Schedule = schedule;

            _unitOfWork.Repository<Domain.Entities.ServiceProvider, int>().Update(serviceProvider);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<ScheduleDto>(serviceProvider.Schedule);
        }
    }
}