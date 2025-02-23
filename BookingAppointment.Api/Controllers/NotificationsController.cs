using BookingAppointment.Application.DTOS.Notification;
using BookingAppointment.Application.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingAppointment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NotificationDto>> GetById(int id)
        {
            var notification = await _notificationService.GetByIdAsync(id);
            if (notification == null) return NotFound();
            return Ok(notification);
        }

        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<List<NotificationDto>>> GetCustomerNotifications(
            int customerId,
            [FromQuery] bool unreadOnly = false)
        {
            var notifications = await _notificationService.GetCustomerNotificationsAsync(customerId, unreadOnly);
            return Ok(notifications);
        }

        [HttpPost]
        public async Task<ActionResult<NotificationDto>> Create(CreateNotificationDto notificationDto)
        {
            var notification = await _notificationService.CreateAsync(notificationDto);
            return CreatedAtAction(nameof(GetById), new { id = notification.Id }, notification);
        }

        [HttpPut("{id}/read")]
        public async Task<ActionResult<NotificationDto>> MarkAsRead(int id)
        {
            try
            {
                var notification = await _notificationService.MarkAsReadAsync(id);
                return Ok(notification);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPut("customer/{customerId}/read-all")]
        public async Task<ActionResult<int>> MarkAllAsRead(int customerId)
        {
            var count = await _notificationService.MarkAllAsReadAsync(customerId);
            return Ok(count);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _notificationService.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
