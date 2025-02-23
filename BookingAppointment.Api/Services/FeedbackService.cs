using AutoMapper;
using BookingAppointment.Api.Erros;
using BookingAppointment.Application.DTOS.Feedback;
using BookingAppointment.Application.IServices;
using BookingAppointment.Domain.Entities;
using BookingAppointment.Domain.Specifications.Feedbacks;
using BookingAppointment.Domain.Unitofwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAppointment.Application.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FeedbackService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<FeedbackDto> CreateFeedbackAsync(int appointmentId, CreateFeedbackDto createFeedbackDto)
        {
            var appointment = await _unitOfWork.Repository<Appointment, int>()
                .GetByIdAsync(appointmentId);

            if (appointment == null)
                throw new Exception($"Resource found, it was not. Appointment ID {appointmentId}");

            var existingFeedback = await _unitOfWork.Repository<Feedback, int>()
                .AnyAsync(f => f.AppointmentId == appointmentId);

            if (existingFeedback)
                throw new Exception("A bad request, you have made. Feedback already exists");

            var feedback = _mapper.Map<Feedback>(createFeedbackDto);
            feedback.AppointmentId = appointmentId;

            await _unitOfWork.Repository<Feedback, int>().AddAsync(feedback);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<FeedbackDto>(feedback);
        }

        public async Task<FeedbackDetailDto> GetFeedbackByIdAsync(int id)
        {
            var spec = new FeedbackWithAppointmentSpec(id);
            var feedback = await _unitOfWork.Repository<Feedback, int>()
                .GetBySpecAsync(spec);

            if (feedback == null)
                throw new Exception($"Resource found, it was not. Feedback ID {id}");

            return _mapper.Map<FeedbackDetailDto>(feedback);
        }

        public async Task<FeedbackDto> GetFeedbackByAppointmentIdAsync(int appointmentId)
        {
            var spec = new FeedbackByAppointmentIdSpec(appointmentId);
            var feedback = await _unitOfWork.Repository<Feedback, int>()
                .GetBySpecAsync(spec);

            if (feedback == null)
                throw new Exception($"Resource found, it was not. Feedback for appointment {appointmentId}");

            return _mapper.Map<FeedbackDto>(feedback);
        }

        public async Task<bool> DeleteFeedbackAsync(int id)
        {
            var feedback = await _unitOfWork.Repository<Feedback, int>()
                .GetByIdAsync(id);

            if (feedback == null)
                throw new Exception($"Resource found, it was not. Feedback ID {id}");

            _unitOfWork.Repository<Feedback, int>().Delete(feedback);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
