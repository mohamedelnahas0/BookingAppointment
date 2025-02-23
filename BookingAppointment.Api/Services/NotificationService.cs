using AutoMapper;
using BookingAppointment.Application.DTOS.Notification;
using BookingAppointment.Application.IServices;
using BookingAppointment.Domain.Entities;
using BookingAppointment.Domain.Specifications.Customers;
using BookingAppointment.Domain.Unitofwork;

namespace BookingAppointment.Api.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public NotificationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<NotificationDto> GetByIdAsync(int id)
        {
            var notification = await _unitOfWork.Repository<Notification, int>().GetByIdAsync(id);
            return _mapper.Map<NotificationDto>(notification);
        }

        public async Task<List<NotificationDto>> GetCustomerNotificationsAsync(int customerId, bool unreadOnly = false)
        {
            var spec = new CustomerNotificationsSpecification(customerId, unreadOnly);
            var notifications = await _unitOfWork.Repository<Notification, int>().ListAsync(spec);
            return _mapper.Map<List<NotificationDto>>(notifications);
        }

        public async Task<NotificationDto> CreateAsync(CreateNotificationDto notificationDto)
        {
            var notification = _mapper.Map<Notification>(notificationDto);
            await _unitOfWork.Repository<Notification, int>().AddAsync(notification);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<NotificationDto>(notification);
        }

        public async Task<NotificationDto> MarkAsReadAsync(int id)
        {
            var notification = await _unitOfWork.Repository<Notification, int>().GetByIdAsync(id);
            if (notification == null)
                throw new Exception("Notification not found");

            notification.IsRead = true;
            notification.ReadAt = DateTime.UtcNow;
            _unitOfWork.Repository<Notification, int>().Update(notification);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<NotificationDto>(notification);
        }

        public async Task<int> MarkAllAsReadAsync(int customerId)
        {
            var spec = new CustomerNotificationsSpecification(customerId, true);
            var notifications = await _unitOfWork.Repository<Notification, int>().ListAsync(spec);

            foreach (var notification in notifications)
            {
                notification.IsRead = true;
                notification.ReadAt = DateTime.UtcNow;
            }

            _unitOfWork.Repository<Notification, int>().UpdateRange(notifications);
            await _unitOfWork.CompleteAsync();

            return notifications.Count();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var notification = await _unitOfWork.Repository<Notification, int>().GetByIdAsync(id);
            if (notification == null)
                return false;

            _unitOfWork.Repository<Notification, int>().Delete(notification);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
    }
