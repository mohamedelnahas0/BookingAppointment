using BookingAppointment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAppointment.Application.DTOS.DeliveryInfos
{
    public class DeliveryInfoDto
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string TrackingNumber { get; set; }
        public DeliveryStatus Status { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }
    }
}
