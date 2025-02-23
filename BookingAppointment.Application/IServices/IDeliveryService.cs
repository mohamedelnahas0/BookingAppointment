using BookingAppointment.Application.DTOS.DeliveryInfos;
using BookingAppointment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAppointment.Application.IServices
{
    public interface IDeliveryService
    {
        Task<DeliveryInfoDto> GetByIdAsync(int id);
        Task<DeliveryInfoDto> UpdateStatusAsync(int id, DeliveryStatus status);
    }
}
