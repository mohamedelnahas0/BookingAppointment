using BookingAppointment.Application.DTOS.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAppointment.Application.IServices
{
    public interface INotificationService
    {
        Task<NotificationDto> GetByIdAsync(int id);
        Task<List<NotificationDto>> GetCustomerNotificationsAsync(int customerId, bool unreadOnly = false);
        Task<NotificationDto> CreateAsync(CreateNotificationDto notificationDto);
        Task<NotificationDto> MarkAsReadAsync(int id);
        Task<int> MarkAllAsReadAsync(int customerId);
        Task<bool> DeleteAsync(int id);
    }
}
