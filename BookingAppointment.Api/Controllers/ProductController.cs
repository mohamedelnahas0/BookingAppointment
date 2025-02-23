using AutoMapper;
using BookingAppointment.Api.Erros;
using BookingAppointment.Application.DTOS.Product;
using BookingAppointment.Domain.Entities;
using BookingAppointment.Domain.Repositories;
using BookingAppointment.Domain.Specifications;
using BookingAppointment.Infrastructure.Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingAppointment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IRepository<Product , int> _productRepo;
        private readonly IMapper _mapper;

        public ProductController(IRepository<Product , int> ProductRepo , IMapper mapper)
        {
            _productRepo = ProductRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var spec = new ProductsWithCategorySpecification();
            var products = await _productRepo.ListAsync(spec);
            var mappedproduct = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDto>>(products);
            return Ok(mappedproduct);
        }


      


        [HttpGet("{id}")]
        public async Task<ActionResult> GetProductsById(int id)
        {
            var spec = new ProductsWithCategorySpecification(id); 
            var products = await _productRepo.GetBySpecAsync(spec);
            if (products is null)
            {
                return NotFound(new ApiResponse(404));
            }
            var mappedproduct = _mapper.Map<Product,ProductToReturnDto>(products);
            return Ok(mappedproduct);
        }
    }
}
