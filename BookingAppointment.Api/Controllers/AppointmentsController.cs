using BookingAppointment.Api.Erros;
using BookingAppointment.Application.DTOS.Appointment;
using BookingAppointment.Application.IServices;
using BookingAppointment.Application.Parameters;
using BookingAppointment.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingAppointment.Api.Controllers
{
   
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpPost]
        public async Task<ActionResult<AppointmentDto>> CreateAppointment([FromBody] CreateAppointmentDto createAppointmentDto)
        {
            try
            {
                var appointment = await _appointmentService.CreateAppointmentAsync(createAppointmentDto);
                return CreatedAtAction(
                    nameof(GetAppointment),
                    new { id = appointment.Id },
                    appointment);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiException(400, ex.Message));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentDto>> GetAppointment(int id)
        {
            try
            {
                var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
                return Ok(appointment);
            }
            catch (Exception ex)
            {
                return NotFound(new ApiException(404, ex.Message));
            }
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<AppointmentListDto>>> GetAppointments([FromQuery] AppointmentSpecParams specParams)
        {
            try
            {
                var appointments = await _appointmentService.GetAppointmentsAsync(specParams);
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiException(400, ex.Message));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AppointmentDto>> UpdateAppointment(
            int id,
            [FromBody] UpdateAppointmentDto updateAppointmentDto)
        {
            try
            {
                var appointment = await _appointmentService.UpdateAppointmentAsync(id, updateAppointmentDto);
                return Ok(appointment);
            }
            catch (Exception ex)
            {
                return NotFound(new ApiException(404, ex.Message));
            }
        }

        [HttpPatch("{id}/status")]
        public async Task<ActionResult<AppointmentDto>> UpdateAppointmentStatus(
            int id,
            [FromBody] AppointmentStatus status)
        {
            try
            {
                var appointment = await _appointmentService.UpdateAppointmentStatusAsync(id, status);
                return Ok(appointment);
            }
            catch (Exception ex)
            {
                return NotFound(new ApiException(404, ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            try
            {
                await _appointmentService.DeleteAppointmentAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new ApiException(404, ex.Message));
            }
        }
    }
}
