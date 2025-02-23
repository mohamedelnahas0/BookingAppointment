

using AutoMapper;
using BookingAppointment.Application.DTOS.Schedule;
using BookingAppointment.Application.IServices;
using BookingAppointment.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingAppointment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulesController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;
        private readonly IMapper _mapper;
        private readonly ILogger<SchedulesController> _logger;

        public SchedulesController(
            IScheduleService scheduleService,
            IMapper mapper,
            ILogger<SchedulesController> logger)
        {
            _scheduleService = scheduleService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("provider/{serviceProviderId}")]
        public async Task<ActionResult<IEnumerable<ScheduleDto>>> GetProviderSchedule(int serviceProviderId)
        {
            var schedules = await _scheduleService.GetServiceProviderScheduleAsync(serviceProviderId);
            return Ok(_mapper.Map<IEnumerable<ScheduleDto>>(schedules));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ScheduleDto>> GetSchedule(int id)
        {
            var schedule = await _scheduleService.GetScheduleByIdAsync(id);
            if (schedule == null)
                return NotFound();

            return Ok(_mapper.Map<ScheduleDto>(schedule));
        }

        [HttpPost]
        public async Task<ActionResult<ScheduleDto>> CreateSchedule(ScheduleCreateDto scheduleDto)
        {

            var schedule = _mapper.Map<Schedule>(scheduleDto);
            await _scheduleService.CreateScheduleAsync(schedule);

            return CreatedAtAction(
             nameof(GetSchedule),
               new { id = schedule.Id },
             _mapper.Map<ScheduleDto>(schedule));
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSchedule(int id, UpdateScheduleDto scheduleDto)
        {
            var schedule = await _scheduleService.GetScheduleByIdAsync(id);
            if (schedule == null)
                return NotFound();

            _mapper.Map(scheduleDto, schedule);
            await _scheduleService.UpdateScheduleAsync(schedule);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchedule(int id)
        {
            var schedule = await _scheduleService.GetScheduleByIdAsync(id);
            if (schedule == null)
                return NotFound();

            await _scheduleService.DeleteScheduleAsync(id);
            return NoContent();
        }

        [HttpGet("availability/{serviceProviderId}")]
        public async Task<ActionResult<IEnumerable<DateTime>>> GetAvailableTimeSlots(
            int serviceProviderId,
            [FromQuery] DateTime date)
        {
            var slots = await _scheduleService.GetAvailableTimeSlotsAsync(serviceProviderId, date);
            return Ok(slots);
        }

        [HttpGet("check-availability/{serviceProviderId}")]
        public async Task<ActionResult<bool>> CheckTimeSlotAvailability(
            int serviceProviderId,
            [FromQuery] DateTime dateTime)
        {
            var isAvailable = await _scheduleService.IsTimeSlotAvailableAsync(serviceProviderId, dateTime);
            return Ok(isAvailable);
        }
    }
}
