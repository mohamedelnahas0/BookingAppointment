using AutoMapper;
using BookingAppointment.Api.Erros;
using BookingAppointment.Application.DTOS.Category;
using BookingAppointment.Domain.Entities;
using BookingAppointment.Domain.Repositories;
using BookingAppointment.Domain.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingAppointment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatrgoryController : ControllerBase
    {
        private readonly IRepository<Category, int> _categoryRepo;
        private readonly IMapper _mapper;

        public CatrgoryController(IRepository<Category, int> CategoryRepo, IMapper mapper )
        {
            _categoryRepo = CategoryRepo;
            _mapper = mapper;
        }

        [HttpGet("Categories")]
        public async Task<ActionResult<IReadOnlyList<Category>>> GetCategories()
        {
           
            var Categories = await _categoryRepo.GetAllAsync();
            var mappedCategory = _mapper.Map<IReadOnlyList<Category>, IReadOnlyList<CategoryToReturnDto>>(Categories);
            return Ok(mappedCategory);
        }





        [HttpGet("{id}")]
        public async Task<ActionResult> GetCategoriesById(int id)
        {
           
            var Categories = await _categoryRepo.GetByIdAsync(id);
            if (Categories is null)
            {
                return NotFound(new ApiResponse(404));
            }
            var mappedCategory = _mapper.Map<Category, CategoryToReturnDto>(Categories);
            return Ok(mappedCategory);
        }
    }


}

