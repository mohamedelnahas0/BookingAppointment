using BookingAppointment.Application.DTOS.Service;
using BookingAppointment.Application.DTOS.ServiceProvider;
using BookingAppointment.Domain.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAppointment.Application.IServices
{
    public interface IServiceService
    {
        Task<ServiceDetailDto> CreateServiceAsync(CreateServiceDto createServiceDto);
        Task<ServiceDto> GetServiceByIdAsync(int id);
        Task<IReadOnlyList<ServiceDto>> GetServicesAsync(ServiceSpecParams specParams);
        Task<ServiceDetailDto> UpdateServiceAsync(int id, UpdateServiceDto updateServiceDto);
        Task<bool> DeleteServiceAsync(int id);
    }
}
