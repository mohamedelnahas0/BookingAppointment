using BookingAppointment.Api.Erros;
using BookingAppointment.Application.Helpers;
using BookingAppointment.Application.IServices;
using BookingAppointment.Application.Services;
using BookingAppointment.Domain.Repositories;
using BookingAppointment.Infrastructure.Data.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BookingAppointment.Api.Extensions
{
    public static  class ApplicationServiceExtension
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            Services.AddAutoMapper(typeof(MappingProfiles));
            Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState.Values.SelectMany(x => x.Errors.Select(p => p.ErrorMessage));
                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(errorResponse);
                };
            });
            Services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });
        

            return Services;

        }
    }
}
