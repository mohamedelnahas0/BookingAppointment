using BookingAppointment.Api.Erros;
using BookingAppointment.Application.DTOS.ServiceProvider;
using BookingAppointment.Domain.Parameters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using BookingAppointment.Application.IServices;
using BookingAppointment.Application.DTOS.Schedule;

namespace BookingAppointment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceProvidersController : ControllerBase
    {
        private readonly IServiceProviderService _serviceProviderService;

        public ServiceProvidersController(IServiceProviderService serviceProviderService)
        {
            _serviceProviderService = serviceProviderService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ServiceProviderDetailDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ServiceProviderDetailDto>> CreateServiceProvider(
                 [FromBody] CreateServiceProviderDto createServiceProviderDto)
        {
            var result = await _serviceProviderService.CreateServiceProviderAsync(createServiceProviderDto);
            return CreatedAtAction(nameof(GetServiceProvider), new { id = result.Id }, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceProviderDetailDto>> GetServiceProvider(int id)
        {
            try
            {
                var serviceProvider = await _serviceProviderService.GetServiceProviderByIdAsync(id);
                return Ok(serviceProvider);
            }
            catch (Exception ex)
            {
                return NotFound(new ApiException(404, ex.Message));
            }
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ServiceProviderDto>>> GetServiceProviders(
            [FromQuery] ServiceProviderSpecParams specParams)
        {
            try
            {
                var serviceProviders = await _serviceProviderService.GetServiceProvidersAsync(specParams);
                return Ok(serviceProviders);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiException(400, ex.Message));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceProviderDetailDto>> UpdateServiceProvider(
            int id,
            [FromBody] UpdateServiceProviderDto updateServiceProviderDto)
        {
            try
            {
                var serviceProvider = await _serviceProviderService.UpdateServiceProviderAsync(id, updateServiceProviderDto);
                return Ok(serviceProvider);
            }
            catch (Exception ex)
            {
                return NotFound(new ApiException(404, ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiceProvider(int id)
        {
            try
            {
                await _serviceProviderService.DeleteServiceProviderAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new ApiException(404, ex.Message));
            }
        }

        [HttpGet("{id}/schedule")]
        public async Task<ActionResult<ScheduleDto>> GetServiceProviderSchedule(int id)
        {
            try
            {
                var schedule = await _serviceProviderService.GetServiceProviderScheduleAsync(id);
                return Ok(schedule);
            }
            catch (Exception ex)
            {
                return NotFound(new ApiException(404, ex.Message));
            }
        }

        [HttpPut("{id}/schedule")]
        public async Task<ActionResult<ScheduleDto>> UpdateServiceProviderSchedule(
            int id,
            [FromBody] ScheduleDto scheduleDto)
        {
            try
            {
                var schedule = await _serviceProviderService.UpdateServiceProviderScheduleAsync(id, scheduleDto);
                return Ok(schedule);
            }
            catch (Exception ex)
            {
                return NotFound(new ApiException(404, ex.Message));
            }
        }
    }
}
