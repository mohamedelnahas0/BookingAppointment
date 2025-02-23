using BookingAppointment.Api.Erros;
using BookingAppointment.Application.DTOS.Feedback;
using BookingAppointment.Application.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingAppointment.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpPost("appointments/{appointmentId}/feedback")]
        public async Task<ActionResult<FeedbackDto>> CreateFeedback(
            int appointmentId,
            [FromBody] CreateFeedbackDto createFeedbackDto)
        {
            try
            {
                var feedback = await _feedbackService.CreateFeedbackAsync(appointmentId, createFeedbackDto);
                return CreatedAtAction(
                    nameof(GetFeedbackById),
                    new { id = feedback.Id },
                    feedback);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiException(400, ex.Message));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FeedbackDetailDto>> GetFeedbackById(int id)
        {
            try
            {
                var feedback = await _feedbackService.GetFeedbackByIdAsync(id);
                return Ok(feedback);
            }
            catch (Exception ex)
            {
                return NotFound(new ApiException(404, ex.Message));
            }
        }

        [HttpGet("appointments/{appointmentId}")]
        public async Task<ActionResult<FeedbackDto>> GetFeedbackByAppointmentId(int appointmentId)
        {
            try
            {
                var feedback = await _feedbackService.GetFeedbackByAppointmentIdAsync(appointmentId);
                return Ok(feedback);
            }
            catch (Exception ex)
            {
                return NotFound(new ApiException(404, ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedback(int id)
        {
            try
            {
                await _feedbackService.DeleteFeedbackAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new ApiException(404, ex.Message));
            }
        }
    }
}
