using BookingAppointment.Api.Erros;
using BookingAppointment.Application.IServices;
using BookingAppointment.Domain.Entities;
using BookingAppointment.Domain.Specifications.Schedules;
using BookingAppointment.Domain.Unitofwork;

namespace BookingAppointment.Api.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ScheduleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Schedule> GetScheduleByIdAsync(int id)
        {
            return await _unitOfWork.Repository<Schedule, int>().GetByIdAsync(id);
        }

        public async Task<IEnumerable<Schedule>> GetServiceProviderScheduleAsync(int serviceProviderId)
        {
            var spec = new ScheduleByServiceProviderSpecification(serviceProviderId);
            return await _unitOfWork.Repository<Schedule, int>().ListAsync(spec);
        }

        public async Task<Schedule> CreateScheduleAsync(Schedule schedule)
        {
            await _unitOfWork.Repository<Schedule, int>().AddAsync(schedule);
            await _unitOfWork.CompleteAsync();
            return schedule;
        }

        
        public async Task UpdateScheduleAsync(Schedule schedule)
        {
            _unitOfWork.Repository<Schedule, int>().Update(schedule);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteScheduleAsync(int id)
        {
            var schedule = await GetScheduleByIdAsync(id);
            if (schedule != null)
            {
                _unitOfWork.Repository<Schedule, int>().Delete(schedule);
                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task<bool> IsTimeSlotAvailableAsync(int serviceProviderId, DateTime dateTime)
        {
            var schedule = await GetServiceProviderScheduleAsync(serviceProviderId);
            var daySchedule = schedule.FirstOrDefault(s => s.DayOfWeek == dateTime.DayOfWeek);

            if (daySchedule == null) return false;

            var timeSpan = TimeSpan.FromHours(dateTime.Hour).Add(TimeSpan.FromMinutes(dateTime.Minute));
            return timeSpan >= daySchedule.StartTime && timeSpan <= daySchedule.EndTime;
        }

        public async Task<IEnumerable<DateTime>> GetAvailableTimeSlotsAsync(int serviceProviderId, DateTime date)
        {
            var schedule = await GetServiceProviderScheduleAsync(serviceProviderId);
            var daySchedule = schedule.FirstOrDefault(s => s.DayOfWeek == date.DayOfWeek);

            if (daySchedule == null) return new List<DateTime>();

            var slots = new List<DateTime>();
            var currentTime = date.Date.Add(daySchedule.StartTime);
            var endTime = date.Date.Add(daySchedule.EndTime);

            while (currentTime <= endTime)
            {
                slots.Add(currentTime);
                currentTime = currentTime.AddMinutes(30); // 30-minute intervals
            }

            return slots;
        }
    }
}
