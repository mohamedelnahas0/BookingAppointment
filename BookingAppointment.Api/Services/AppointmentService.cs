using AutoMapper;
using BookingAppointment.Application.DTOS.Appointment;
using BookingAppointment.Application.IServices;
using BookingAppointment.Application.Parameters;
using BookingAppointment.Domain.Entities;
using BookingAppointment.Domain.Specifications.Appointments;
using BookingAppointment.Domain.Unitofwork;

namespace BookingAppointment.Api.Services
{

    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AppointmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AppointmentDto> CreateAppointmentAsync(CreateAppointmentDto createAppointmentDto)
        {
            // Validate customer exists
            var customer = await _unitOfWork.Repository<Customer, int>()
                .GetByIdAsync(createAppointmentDto.CustomerId);
            if (customer == null)
                throw new Exception($"Customer with ID {createAppointmentDto.CustomerId} not found");

            // Validate service exists
            var service = await _unitOfWork.Repository<Service, int>()
                .GetByIdAsync(createAppointmentDto.ServiceId);
            if (service == null)
                throw new Exception($"Service with ID {createAppointmentDto.ServiceId} not found");

            // Validate service provider exists
            var serviceProvider = await _unitOfWork.Repository<Domain.Entities.ServiceProvider, int>()
                .GetByIdAsync(createAppointmentDto.ServiceProviderId);
            if (serviceProvider == null)
                throw new Exception($"Service Provider with ID {createAppointmentDto.ServiceProviderId} not found");

            // Check for conflicting appointments
            var conflictingAppointment = await _unitOfWork.Repository<Appointment, int>()
                .AnyAsync(a =>
                    a.ServiceProviderId == createAppointmentDto.ServiceProviderId &&
                    a.AppointmentDateTime == createAppointmentDto.AppointmentDateTime &&
                    a.Status != AppointmentStatus.Cancelled);

            if (conflictingAppointment)
                throw new Exception("The selected time slot is not available");

            var appointment = _mapper.Map<Appointment>(createAppointmentDto);
            appointment.Status = AppointmentStatus.Scheduled;

            await _unitOfWork.Repository<Appointment, int>().AddAsync(appointment);
            await _unitOfWork.CompleteAsync();

            var spec = new AppointmentWithDetailsSpec(appointment.Id);
            var createdAppointment = await _unitOfWork.Repository<Appointment, int>()
                .GetBySpecAsync(spec);

            return _mapper.Map<AppointmentDto>(createdAppointment);
        }

        public async Task<AppointmentDto> GetAppointmentByIdAsync(int id)
        {
            var spec = new AppointmentWithDetailsSpec(id);
            var appointment = await _unitOfWork.Repository<Appointment, int>()
                .GetBySpecAsync(spec);

            if (appointment == null)
                throw new Exception($"Appointment with ID {id} not found");

            return _mapper.Map<AppointmentDto>(appointment);
        }

        public async Task<IReadOnlyList<AppointmentListDto>> GetAppointmentsAsync(AppointmentSpecParams specParams)
        {
            var spec = new AppointmentListSpec(specParams);
            var appointments = await _unitOfWork.Repository<Appointment, int>()
                .ListAsync(spec);

            return _mapper.Map<IReadOnlyList<AppointmentListDto>>(appointments);
        }

        public async Task<AppointmentDto> UpdateAppointmentAsync(int id, UpdateAppointmentDto updateAppointmentDto)
        {
            var spec = new AppointmentWithDetailsSpec(id);
            var appointment = await _unitOfWork.Repository<Appointment, int>()
                .GetBySpecAsync(spec);

            if (appointment == null)
                throw new Exception($"Appointment with ID {id} not found");

            _mapper.Map(updateAppointmentDto, appointment);

            _unitOfWork.Repository<Appointment, int>().Update(appointment);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<AppointmentDto>(appointment);
        }

        public async Task<AppointmentDto> UpdateAppointmentStatusAsync(int id, AppointmentStatus status)
        {
            var spec = new AppointmentWithDetailsSpec(id);
            var appointment = await _unitOfWork.Repository<Appointment, int>()
                .GetBySpecAsync(spec);

            if (appointment == null)
                throw new Exception($"Appointment with ID {id} not found");

            appointment.Status = status;

            _unitOfWork.Repository<Appointment, int>().Update(appointment);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<AppointmentDto>(appointment);
        }

        public async Task<bool> DeleteAppointmentAsync(int id)
        {
            var appointment = await _unitOfWork.Repository<Appointment, int>()
                .GetByIdAsync(id);

            if (appointment == null)
                throw new Exception($"Appointment with ID {id} not found");

            _unitOfWork.Repository<Appointment, int>().Delete(appointment);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
