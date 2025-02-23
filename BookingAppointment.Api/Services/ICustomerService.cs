using BookingAppointment.Application.DTOS.Customer;
using BookingAppointment.Application.DTOS.Customers;

namespace BookingAppointment.Api.Services
{
    public interface ICustomerService
    {
        Task<CustomerDto> GetByIdAsync(int id);
        Task<CustomerDto> GetByEmailAsync(string email);
        Task<CustomerDto> CreateAsync(CreateCustomerDto customerDto);
        Task<CustomerDto> UpdateAsync(int id, UpdateCustomerDto customerDto);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateNotificationPreferencesAsync(int id, bool enableEmail, bool enableSms);
    }
}
