using BookingAppointment.Application.DTOS.DeliveryInfos;
using BookingAppointment.Application.IServices;
using BookingAppointment.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingAppointment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        private readonly IDeliveryService _deliveryService;

        public DeliveryController(IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DeliveryInfoDto>> GetById(int id)
        {
            var delivery = await _deliveryService.GetByIdAsync(id);
            if (delivery == null) return NotFound();
            return Ok(delivery);
        }

        [HttpPut("{id}/status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DeliveryInfoDto>> UpdateStatus(
            int id,
            [FromBody] DeliveryStatus status)
        {
            var delivery = await _deliveryService.UpdateStatusAsync(id, status);
            return Ok(delivery);
        }
    }
  
}
