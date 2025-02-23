using BookingAppointment.Application.DTOS.OrderItems;
using BookingAppointment.Application.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingAppointment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly IOrderItemService _orderItemService;

        public OrderItemsController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderItemDetailDto>> GetById(int id)
        {
            var orderItem = await _orderItemService.GetByIdAsync(id);
            if (orderItem == null)
                return NotFound();
            return Ok(orderItem);
        }

        [HttpGet("order/{orderId}")]
        public async Task<ActionResult<IEnumerable<OrderItemDetailDto>>> GetByOrderId(int orderId)
        {
            var orderItems = await _orderItemService.GetByOrderIdAsync(orderId);
            return Ok(orderItems);
        }

        [HttpPost]
        public async Task<ActionResult<OrderItemDetailDto>> Create(CreateOrderItemDto createDto)
        {
            try
            {
                var orderItem = await _orderItemService.CreateAsync(createDto);
                return CreatedAtAction(nameof(GetById), new { id = orderItem.Id }, orderItem);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
           
        }

        [HttpPut("{id}/quantity")]
        public async Task<ActionResult<OrderItemDetailDto>> UpdateQuantity(int id, UpdateOrderItemDto updateDto)
        {
            try
            {
                var orderItem = await _orderItemService.UpdateQuantityAsync(id, updateDto);
                return Ok(orderItem);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
           
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await _orderItemService.DeleteAsync(id);
                if (!result)
                    return NotFound();
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
