using AutoMapper;
using AutoMapper.Execution;
using BookingAppointment.Domain.Entities;
using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using BookingAppointment.Application.DTOS.Product;

namespace BookingAppointment.Application.Helpers
{
    public class ProductPictureUrlResolver(IConfiguration configuration) : IValueResolver<Product,ProductToReturnDto,string>
    {
        private readonly IConfiguration _configuration = configuration;

        public string Resolve(Product source,ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ImageUrl))
            
                return $"{_configuration["ApiBaseUrl"]}{source.ImageUrl}";
                return string.Empty;
        }
    }
     
}
