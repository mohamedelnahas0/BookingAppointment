using BookingAppointment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAppointment.Application.IServices
{
    public interface IScheduleService
    {
        Task<Schedule> GetScheduleByIdAsync(int id);
        Task<IEnumerable<Schedule>> GetServiceProviderScheduleAsync(int serviceProviderId);
        Task<Schedule> CreateScheduleAsync(Schedule schedule);
        Task UpdateScheduleAsync(Schedule schedule);
        Task DeleteScheduleAsync(int id);
        Task<bool> IsTimeSlotAvailableAsync(int serviceProviderId, DateTime dateTime);
        Task<IEnumerable<DateTime>> GetAvailableTimeSlotsAsync(int serviceProviderId, DateTime date);
    }
}
