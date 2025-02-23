using BookingAppointment.Api.Services;
using BookingAppointment.Application.DTOS.Customer;
using BookingAppointment.Application.DTOS.Customers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingAppointment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetById(int id)
        {
            var customer = await _customerService.GetByIdAsync(id);
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        [HttpGet("email/{email}")]
        public async Task<ActionResult<CustomerDto>> GetByEmail(string email)
        {
            var customer = await _customerService.GetByEmailAsync(email);
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDto>> Create(CreateCustomerDto customerDto)
        {
            var customer = await _customerService.CreateAsync(customerDto);
            return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CustomerDto>> Update(int id, UpdateCustomerDto customerDto)
        {
            try
            {
                var customer = await _customerService.UpdateAsync(id, customerDto);
                return Ok(customer);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _customerService.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpPut("{id}/notification-preferences")]
        public async Task<ActionResult> UpdateNotificationPreferences(
            int id,
            [FromQuery] bool enableEmail,
            [FromQuery] bool enableSms)
        {
            var result = await _customerService.UpdateNotificationPreferencesAsync(id, enableEmail, enableSms);
            if (!result) return NotFound();
            return NoContent();
        }
    }

}
