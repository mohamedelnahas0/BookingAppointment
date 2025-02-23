using BookingAppointment.Api.Erros;
using BookingAppointment.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingAppointment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : ControllerBase
    {
        private readonly AppointmentContext _dbContext;

        public BuggyController(AppointmentContext dbContext )
        {
            _dbContext = dbContext;
        }
        [HttpGet("Notfound")]
        public ActionResult GetNotFoundRequest()
        {
            var product = _dbContext.Products.Find(42);
           if(product is null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(product);
        }
        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            var product = _dbContext.Products.Find(42);
            var productToReturn = product.ToString();
            return Ok(productToReturn);
        }
        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }
        [HttpGet("badrequest/{id}")]
        public ActionResult GetBadRequest(int id)
        {
            return Ok();
        }
    }
}
