using BookingAppointment.Application.DTOS.Appointment;
using BookingAppointment.Application.Parameters;
using BookingAppointment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAppointment.Application.IServices
{
    public interface IAppointmentService
    {
        Task<AppointmentDto> CreateAppointmentAsync(CreateAppointmentDto createAppointmentDto);
        Task<AppointmentDto> GetAppointmentByIdAsync(int id);
        Task<IReadOnlyList<AppointmentListDto>> GetAppointmentsAsync(AppointmentSpecParams specParams);
        Task<AppointmentDto> UpdateAppointmentAsync(int id, UpdateAppointmentDto updateAppointmentDto);
        Task<AppointmentDto> UpdateAppointmentStatusAsync(int id, AppointmentStatus status);
        Task<bool> DeleteAppointmentAsync(int id);
    }

}
