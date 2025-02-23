using AutoMapper;
using BookingAppointment.Application.DTOS.Appointment;
using BookingAppointment.Application.DTOS.Customer;
using BookingAppointment.Application.DTOS.Customers;
using BookingAppointment.Application.IServices;
using BookingAppointment.Domain.Entities;
using BookingAppointment.Domain.Specifications.Customers;
using BookingAppointment.Domain.Unitofwork;

namespace BookingAppointment.Api.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CustomerDto> GetByIdAsync(int id)
        {
            var spec = new CustomerWithDetailsSpecification(id);
            var customer = await _unitOfWork.Repository<Customer, int>().GetBySpecAsync(spec);
            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<CustomerDto> GetByEmailAsync(string email)
        {
            var spec = new CustomerWithDetailsSpecification(email);
            var customer = await _unitOfWork.Repository<Customer, int>().GetBySpecAsync(spec);
            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<CustomerDto> CreateAsync(CreateCustomerDto customerDto)
        {
            // Check if email is already in use
            var exists = await _unitOfWork.Repository<Customer, int>()
                .AnyAsync(c => c.Email == customerDto.Email);

            if (exists)
                throw new InvalidOperationException("Email is already in use");

            var customer = _mapper.Map<Customer>(customerDto);
            await _unitOfWork.Repository<Customer, int>().AddAsync(customer);
            await _unitOfWork.CompleteAsync();

            return await GetByIdAsync(customer.Id);
        }

        public async Task<CustomerDto> UpdateAsync(int id, UpdateCustomerDto customerDto)
        {
            var customer = await _unitOfWork.Repository<Customer, int>().GetByIdAsync(id);
            if (customer == null)
                throw new Exception("Customer not found");

            _mapper.Map(customerDto, customer);
            _unitOfWork.Repository<Customer, int>().Update(customer);
            await _unitOfWork.CompleteAsync();

            return await GetByIdAsync(id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var customer = await _unitOfWork.Repository<Customer, int>().GetByIdAsync(id);
            if (customer == null)
                return false;

            customer.IsActive = false;
            _unitOfWork.Repository<Customer, int>().Update(customer);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> UpdateNotificationPreferencesAsync(int id, bool enableEmail, bool enableSms)
        {
            var customer = await _unitOfWork.Repository<Customer, int>().GetByIdAsync(id);
            if (customer == null)
                return false;

            customer.EnableEmailNotifications = enableEmail;
            customer.EnableSmsNotifications = enableSms;
            _unitOfWork.Repository<Customer, int>().Update(customer);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}