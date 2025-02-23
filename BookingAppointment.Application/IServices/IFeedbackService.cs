using BookingAppointment.Application.DTOS.Feedback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAppointment.Application.IServices
{
    public interface IFeedbackService
    {
        Task<FeedbackDto> CreateFeedbackAsync(int appointmentId, CreateFeedbackDto createFeedbackDto);
        Task<FeedbackDetailDto> GetFeedbackByIdAsync(int id);
        Task<FeedbackDto> GetFeedbackByAppointmentIdAsync(int appointmentId);
        Task<bool> DeleteFeedbackAsync(int id);
    }
}
