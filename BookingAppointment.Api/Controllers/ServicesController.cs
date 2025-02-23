using BookingAppointment.Api.Erros;
using BookingAppointment.Application.DTOS.Service;
using BookingAppointment.Application.DTOS.ServiceProvider;
using BookingAppointment.Application.IServices;
using BookingAppointment.Domain.Parameters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingAppointment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServicesController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceDetailDto>> CreateService([FromBody] CreateServiceDto createServiceDto)
        {
            try
            {
                var service = await _serviceService.CreateServiceAsync(createServiceDto);
                return CreatedAtAction(
                    nameof(GetService),
                    new { id = service.Id },

                    service);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiException(400, ex.Message));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceDetailDto>> GetService(int id)
        {
            try
            {
                var service = await _serviceService.GetServiceByIdAsync(id);
                return Ok(service);
            }
            catch (Exception ex)
            {
                return NotFound(new ApiException(404, ex.Message));
            }
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ServiceDto>>> GetServices([FromQuery] ServiceSpecParams specParams)
        {
            try
            {
                var services = await _serviceService.GetServicesAsync(specParams);
                return Ok(services);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiException(400, ex.Message));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceDetailDto>> UpdateService(
            int id,
            [FromBody] UpdateServiceDto updateServiceDto)
        {
            try
            {
                var service = await _serviceService.UpdateServiceAsync(id, updateServiceDto);
                return Ok(service);
            }
            catch (Exception ex)
            {
                return NotFound(new ApiException(404, ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            try
            {
                await _serviceService.DeleteServiceAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new ApiException(404, ex.Message));
            }
        }
    }
}
