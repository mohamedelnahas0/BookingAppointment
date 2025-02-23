using BookingAppointment.Application.DTOS.ServiceProvider;
using BookingAppointment.Domain.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingAppointment.Application.DTOS.Schedule;

namespace BookingAppointment.Application.IServices
{
    public interface IServiceProviderService
    {
        Task<ServiceProviderDetailDto> GetServiceProviderByIdAsync(int id);
        Task<IReadOnlyList<ServiceProviderDto>> GetServiceProvidersAsync(ServiceProviderSpecParams specParams);
        Task<ServiceProviderDetailDto> UpdateServiceProviderAsync(int id, UpdateServiceProviderDto updateServiceProviderDto);
        Task<bool> DeleteServiceProviderAsync(int id);
        Task<ScheduleDto> GetServiceProviderScheduleAsync(int id);
        Task<ScheduleDto> UpdateServiceProviderScheduleAsync(int id, ScheduleDto scheduleDto);
        Task<ServiceProviderDetailDto> CreateServiceProviderAsync(CreateServiceProviderDto createServiceProviderDto);

    }


}
